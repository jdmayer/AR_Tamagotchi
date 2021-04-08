using Character;
using Item;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        private UI.Timer _timer;

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

            State = AdventureState.IsInactive;
            _timer = new UI.Timer(Canvas, TimerTextPrefab, RechargeTime);

            HideFightOptions();
        }

        void Update()
        {
            if (_timer.HasTimeOut)
            {
                _timer.UpdateTimer(_vegetation.transform);
                return;
            }
            else if (State == AdventureState.IsInactive)
            {
                SetAttentionMark();

                if (Input.GetKeyUp("t"))
                {
                    Debug.Log("Discover adventure");
                    DiscoverAdventure();
                }
            }

            if (Input.GetKeyUp("l"))
            {
            _adventureObject.GetComponent<Animator>().SetTrigger(Constants.Loop);
            }
            if (Input.GetKeyUp("k"))
            {
            _adventureObject.GetComponent<Animator>().SetTrigger(Constants.Laugh);
            }
        }

        public GameObject GetEnemyGameObject()
        {
            return _adventureObject;
        }

        private void SetAttentionMark()
        {
            float actualDistance = Vector3.Distance(_fino.transform.position, transform.position);
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
                CreateRandomGem();
            }
            else
            {
                CreateRandomEnemy();
            }
        }

        private void CreateRandomGem()
        {
            State = AdventureState.HasGem;

            var gemPrefab = GetRandomPrefab(Prefabs.GemPrefabs, Prefabs.GemDirectory);
            _adventureObject = Instantiate(gemPrefab, _vegetation.transform.position, _vegetation.transform.rotation, gameObject.transform);
            _adventureObject.transform.localScale = new Vector3(1f, 1f, 1f);
            _gem = new GemStone(GemUtil.GetGemTypeByName(gemPrefab.name));
            _dialogManager.StartDialog(_gem.InformationDialog, HideExclamationMark);
        }

        private void CreateRandomEnemy()
        {
            State = AdventureState.IsFighting;

            var statusBarPrefab = Resources.Load(Prefabs.StatusBar) as GameObject;
            var statusBarPosition = Camera.main.WorldToScreenPoint(_vegetation.transform.position);
            GameObject enemyStats = Instantiate(statusBarPrefab, statusBarPosition, Quaternion.Euler(0, 0, 0), Canvas.transform);
            StatusBar bar = enemyStats.transform.GetChild(0).GetComponent<StatusBar>();

            var dragonPrefab = Resources.Load(Prefabs.DragonDirectory + Prefabs.DragonNeutral) as GameObject;
            var newRotation = Quaternion.LookRotation(_fino.transform.position) * Quaternion.AngleAxis(90, transform.up);
            _adventureObject = Instantiate(dragonPrefab, _vegetation.transform.position, newRotation, gameObject.transform);
            _adventureObject.transform.localScale = new Vector3(1f, 1f, 1f);

            _adventureObject.AddComponent<Animator>();
            var entityController = Resources.Load(Constants.EntityControllerPath) as RuntimeAnimatorController;
            _adventureObject.GetComponent<Animator>().runtimeAnimatorController = entityController;

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
            HideFightOptions();
        }

        public void EnemyReaction()
        {
            _dialogManager.StartDialog(_enemy.LaughDialog, EnemyAttack);
            _adventureObject.gameObject.GetComponent<Animator>().SetTrigger(Constants.Laugh);
            _enemy.TakeDamage();

            if (_enemy.Anger == 0)
            {
                WinFight();
            }
        }
                

        public void EnemyAttack()
        {
            State = AdventureState.IsBeingAttacked;

            _adventureObject.GetComponent<Animator>().SetTrigger(Constants.Attack);
            _fino.GetComponent<Animator>().SetTrigger(Constants.GetHit);

            Player.Health = Player.Health - _enemy.DealDamage();

            if (Player.Health == 0)
            {
                LoseFight();
            }
            else
            {
                _dialogManager.StartDialog(new Dialog(Player.Name, "You've been attacked! Ouch ..."), EndEnemyAttack);
            }
        }

        public void EndEnemyAttack()
        {
            StopCoroutine("DisplayAttackAnimation");
            DisplayFightOptions();
        }

        public void WinFight()
        {
            State = AdventureState.IsDone;
            StopCoroutine("UpdateEnemyStatusBarPosition");
            _dialogManager.StartDialog(_enemy.LoseDialog, CreateRandomGem);
            Destroy(_enemy.StatusBar.gameObject);
            Destroy(_adventureObject);
        }

        public void LoseFight()
        {
            State = AdventureState.IsDone;
            StopCoroutine("UpdateEnemyStatusBarPosition");
            _dialogManager.StartDialog(_enemy.WinDialog, ReturnToMainScene);
            Destroy(_enemy.StatusBar.gameObject);
            Destroy(_adventureObject);
        }

        private void ReturnToMainScene()
        {
            ResetAdventure();
            Player.SetPlayerPrefs();
            Debug.Log("You kinda lost.");
            SceneManager.LoadScene(Constants.SceneMain);
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

        public void ResetAdventure()
        {
            Debug.Log("reset!");
            Debug.Log(State == AdventureState.IsDone);
            if (State != AdventureState.IsDone)
            {
                return;
            }

            if (_adventureObject != null)
            {
                Destroy(_adventureObject);
            }

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

            _timer.StartTimer(_vegetation.transform);
            State = AdventureState.IsInactive;
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