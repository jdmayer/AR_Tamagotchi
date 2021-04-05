using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Vuforia;

/// <summary>
/// Author: Janine Mayer
/// </summary>
public class RandomAdventure : MonoBehaviour, ITrackableEventHandler
{
    public GameObject Player;
    public float MinDistance = 0.3f;
    public float RechargeTime = 60f;
    public float PossibilityOfGem = 30;
    public Text TimerTextPrefab;
    public Canvas Canvas;

    private float _timeLeft = 0f;
    private bool _hasTimeOut = false;
    private Text _countDown;

    private GameObject _exclamationMark;
    private GameObject _questionMark;
    private GameObject _vegetation;
    private GameObject _adventureObject;
    private AudioSource _audioSource;

    private bool _isActive;

    private IList<TrackableBehaviour.Status> _trackableStatus = 
        new List<TrackableBehaviour.Status>() 
        { 
            TrackableBehaviour.Status.DETECTED,
            TrackableBehaviour.Status.EXTENDED_TRACKED,
            TrackableBehaviour.Status.TRACKED 
        };

    void Start()
    {
        GetComponent<TrackableBehaviour>().RegisterTrackableEventHandler(this);
        _audioSource = GetComponent<AudioSource>();

        _exclamationMark = transform.Find(Prefabs.ExclamationMark).gameObject;
        _exclamationMark.SetActive(false);
        _questionMark = transform.Find(Prefabs.QuestionMark).gameObject;
        _questionMark.SetActive(false);
        _vegetation = transform.Find(Prefabs.Vegetation).gameObject;

        _isActive = false;
    }

    void Update()
    {
        if (_hasTimeOut)
        {
            _timeLeft -= Time.deltaTime;
            _countDown.text = $"{_timeLeft:00} seconds";
            _countDown.transform.position = Camera.main.WorldToScreenPoint(_vegetation.transform.position);

            if (_timeLeft < 0)
            {
                StopTimer();
            }

            return;
        }
        else if (!_isActive)
        {
            SetAttentionMark();
        }

        // TODO Remove for PROD
        if (Input.GetKeyUp("t") && !_isActive)
        {
            Debug.Log("Discover adventure");
            DiscoverAdventure();
        }

        if (Input.GetKeyUp("r") && _isActive)
        {
            Debug.Log("Reset adventure");
            ResetAdventure();
        }
    }

    private void SetAttentionMark()
    {
        float actualDistance = Vector3.Distance(Player.transform.position, transform.position);
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
        if (_isActive)
        {
            return;
        }

        _isActive = true;

        _questionMark.SetActive(false);
        _vegetation.SetActive(false);

        if (Random.Range(0, 100) <= PossibilityOfGem)
        {
            var gemPrefab = GetRandomPrefab(Prefabs.GemPrefabs, Prefabs.GemDirectory);
            _adventureObject = Instantiate(gemPrefab, _vegetation.transform.position, _vegetation.transform.rotation, gameObject.transform);
            _adventureObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            var dragonPrefab = Resources.Load(Prefabs.DragonDirectory + Prefabs.DragonNeutral) as GameObject;
            _adventureObject = Instantiate(dragonPrefab, _vegetation.transform.position, _vegetation.transform.rotation, gameObject.transform);
            _adventureObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private GameObject GetRandomPrefab(string[] objects, string directory)
    {
        var randomIndex = Random.Range(0, objects.Length);
        var randomPrefabName = $"{directory}{objects[randomIndex]}";
        return Resources.Load(randomPrefabName) as GameObject;
    }

    public void ResetAdventure()
    {
        Destroy(_adventureObject);

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

        _isActive = false;
        StartTimer();
    }

    private void StartTimer()
    {
        if (_countDown == null)
        {
            var countdownPos = Camera.main.WorldToScreenPoint(_vegetation.transform.position);
            _countDown = Instantiate(TimerTextPrefab, Canvas.transform);
            _countDown.transform.position = countdownPos;
        }

        _countDown.enabled = true;
        _timeLeft = RechargeTime;
        _hasTimeOut = true;
    }

    private void StopTimer()
    {
        _countDown.enabled = false;
        _hasTimeOut = false;
    }

    //move to other class - to detect help or item
    //as long as nothing is shown - DO NOT PLAY SOUND
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (_trackableStatus.Contains(newStatus))
        {
            _audioSource.mute = false;
        }
    }
}
