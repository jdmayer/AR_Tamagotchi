﻿using Character;
using Item;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Vuforia;
using static UI.DialogManager;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Interaction
{
    public class RandomAdventure : MonoBehaviour, ITrackableEventHandler
    {
        public Character.Character Player;
        public float MinDistance = 0.3f;
        public float RechargeTime = 60f;
        public float PossibilityOfGem = 30;

        public Button TickleButton;
        public Button JokeButton;

        public Text TimerTextPrefab;
        public Canvas Canvas;
        private Timer _timer;

        private GameObject _fino;
        private GameObject _exclamationMark;
        private GameObject _questionMark;
        private GameObject _vegetation;
        private GameObject _adventureObject;
        private AudioSource _audioSource;
        private DialogManager _dialogManager;

        private GemStone _gem;
        private Enemy _enemy;

        public AdventureState State;
        public bool IsActive;

        void Start()
        {
            GetComponent<TrackableBehaviour>().RegisterTrackableEventHandler(this);
            _audioSource = GetComponent<AudioSource>();
            _dialogManager = FindObjectOfType<DialogManager>();

            _fino = GameObject.Find("Fino/micro_dragon_fino_happy");

            _exclamationMark = _fino.transform.Find(Prefabs.ExclamationMark).gameObject;
            _exclamationMark.SetActive(false);

            _questionMark = transform.Find(Prefabs.QuestionMark).gameObject;
            _questionMark.SetActive(false);
            _vegetation = transform.Find(Prefabs.Vegetation).gameObject;

            IsActive = false;
            State = AdventureState.IsInactive;
            _timer = new Timer(Canvas, TimerTextPrefab, RechargeTime);

            HideFightOptions();
        }

        void Update()
        {
            if (_timer.HasTimeOut)
            {
                _timer.UpdateTimer(_vegetation.transform);
                return;
            }
            else if (!IsActive)
            {
                SetAttentionMark();
            }

            // TODO Remove for PROD
            if (Input.GetKeyUp("t") && !IsActive)
            {
                Debug.Log("Discover adventure");
                DiscoverAdventure();
            }

            if (Input.GetKeyUp("r") && IsActive)
            {
                Debug.Log("Reset adventure");
                ResetAdventure();
            }
        }

        public GemStone GetActiveGem()
        {
            return _gem;
        }

        public Enemy GetActiveEnemy()
        {
            return _enemy;
        }

        private void SetAttentionMark()
        {
            float actualDistance = Vector3.Distance(Player.gameObject.transform.position, transform.position);
            if (actualDistance <= MinDistance)
            {
                if (!_questionMark.activeSelf)
                {
                    _audioSource.Play();
                    _questionMark.SetActive(true);
                }
            }
            else
            {
                _audioSource.Stop();
                _questionMark.SetActive(false);
            }
        }

        public void DiscoverAdventure()
        {
            if (State != AdventureState.IsInactive)
            {
                return;
            }

            State = AdventureState.IsActive;

            _exclamationMark.SetActive(true);
            _questionMark.SetActive(false);
            _vegetation.SetActive(false);

            if (Random.Range(0, 100) <= PossibilityOfGem)
            {
                State = AdventureState.HasGem;
                CreateRandomGem();
            }
            else
            {
                State = AdventureState.IsFighting;
                CreateRandomEnemy();
            }
        }

        private void CreateRandomGem()
        {
            var gemPrefab = GetRandomPrefab(Prefabs.GemPrefabs, Prefabs.GemDirectory);
            _adventureObject = Instantiate(gemPrefab, _vegetation.transform.position, _vegetation.transform.rotation, gameObject.transform);
            _adventureObject.transform.localScale = new Vector3(1f, 1f, 1f);
            _gem = new GemStone(GemUtil.GetGemTypeByName(gemPrefab.name));
            _dialogManager.StartDialog(_gem.InformationDialog, HideExclamationMark);
        }

        private void CreateRandomEnemy()
        {
            var statusBarPrefab = Resources.Load(Prefabs.StatusBar) as GameObject;
            var statusBarPosition = Camera.main.WorldToScreenPoint(_vegetation.transform.position);
            GameObject enemyStats = Instantiate(statusBarPrefab, statusBarPosition, Quaternion.Euler(0, 0, 0), Canvas.transform);
            StatusBar bar = enemyStats.transform.GetChild(0).GetComponent<StatusBar>();

            var dragonPrefab = Resources.Load(Prefabs.DragonDirectory + Prefabs.DragonNeutral) as GameObject;
            var newRotation = Quaternion.LookRotation(_fino.transform.position) * Quaternion.AngleAxis(90, transform.up);
            _adventureObject = Instantiate(dragonPrefab, _vegetation.transform.position, newRotation, gameObject.transform);
            _adventureObject.transform.localScale = new Vector3(1f, 1f, 1f);
            _adventureObject.AddComponent<Animator>();
            Handheld.Vibrate();

            _enemy = new Enemy(Player.Level, bar);
            StartCoroutine("UpdateEnemyStatusBarPosition");
            _dialogManager.StartDialog(_enemy.StartDialog, DisplayFightOptions);
        }

        private GameObject GetRandomPrefab(string[] objects, string directory)
        {
            var randomIndex = Random.Range(0, objects.Length);
            var randomPrefabName = $"{directory}{objects[randomIndex]}";
            return Resources.Load(randomPrefabName) as GameObject;
        }

        public void DisplayFightOptions()
        {
            HideExclamationMark();
            TickleButton.gameObject.SetActive(true);
            JokeButton.gameObject.SetActive(true);
        }
        
        public void HideFightOptions()
        {
            TickleButton.gameObject.SetActive(false);
            JokeButton.gameObject.SetActive(false);       
        }

        public void HideExclamationMark()
        {
            _exclamationMark.SetActive(false);
        }

        public void TellAJoke()
        {
            State = AdventureState.IsJoking;
            HideFightOptions();
            var joke = new Dialog(Player.Name, Jokes.GetRandomJoke());
            _dialogManager.StartDialog(joke, EnemyReaction);
        }

        public void TickleEnemy()
        {
            State = AdventureState.IsTickling;
        }

        public void EnemyReaction()
        {
            Debug.Log("laugh!");
            State = AdventureState.IsBeingAttacked; // TODO depend on strength!
        }

        public void WinFight()
        {
            TriggerDialog(_enemy.LoseDialog);
            StopCoroutine("UpdateEnemyStatusBarPosition");
            //TODO on continue:
            //IsFighting = false;
            //Destroy(_enemy.StatusBar.gameObject);
            //Destroy(_adventureObject);

            //CreateRandomGem();
            //reset everything!
            State = AdventureState.IsDone;
        }

        public void LoseFight()
        {
            TriggerDialog(_enemy.WinDialog);
            StopCoroutine("UpdateEnemyStatusBarPosition");
            //TODO - Add Dialog and go back to 
            State = AdventureState.IsDone;
        }

        public void UseGemStone()
        {
            _gem.UseGemStone(Player);
        }

        public void UsedGemStone()
        {
            State = AdventureState.IsDone;
            _dialogManager.StartDialog(_gem.UsedDialog, ResetAdventure);
        }

        //should only be able when fight is lost/won and no gem is there anymore
        public void ResetAdventure()
        {
            if (State != AdventureState.IsDone)
            {
                return;
            }

            //Destroy(_adventureObject);

            var randomVegetation = GetRandomPrefab(Prefabs.VegetationPrefabs, Prefabs.VegetationDirectory);
            if (randomVegetation == null)
            {
                _vegetation.SetActive(true);
            }
            else
            {
                GameObject newVegetation = Instantiate(randomVegetation,
                    _vegetation.transform.position, _vegetation.transform.rotation, gameObject.transform);
                newVegetation.transform.localScale = _vegetation.transform.localScale;

                Destroy(_vegetation);
                _vegetation = newVegetation;
            }

            IsActive = false;
            _timer.StartTimer(_vegetation.transform);
        }

        IEnumerator UpdateEnemyStatusBarPosition()
        {
            while (true)
            {
                _enemy.UpdateStatPosition(this.transform);
                yield return null;
            }
        }

        public void TriggerDialog(Dialog dialog)
        {
            FindObjectOfType<DialogManager>().StartDialog(dialog);
        }

        public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
        {
            IList<TrackableBehaviour.Status> _trackableStatus =
            new List<TrackableBehaviour.Status>()
            {
            TrackableBehaviour.Status.DETECTED,
            TrackableBehaviour.Status.EXTENDED_TRACKED,
            TrackableBehaviour.Status.TRACKED
            };

            if (_trackableStatus.Contains(newStatus))
            {
                _audioSource.mute = false;
            }
        }
    }
}