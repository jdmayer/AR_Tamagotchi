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
    public Text TimerTextPrefab;
    public Canvas Canvas;

    private float _timeLeft = 0f;
    private bool _hasTimeOut = false;
    private Text _countDown;

    private GameObject _exclamationMark;
    private GameObject _questionMark;
    private GameObject _vegetation;
    private AudioSource _audioSource;

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

        _exclamationMark = transform.Find(Constants.ExclamationMark).gameObject;
        _exclamationMark.SetActive(false);
        _questionMark = transform.Find(Constants.QuestionMark).gameObject;
        _questionMark.SetActive(false);
        _vegetation = transform.Find(Constants.Vegetation).gameObject;
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
        else
        {
            SetAttentionMark();
        }


        // TODO Remove for PROD
        if (Input.GetKeyUp("t"))
        {
            Debug.Log("Discover adventure");
            DiscoverAdventure();
        }

        if (Input.GetKeyUp("r"))
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
                Debug.Log("SURPRISE!!");
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
        _questionMark.SetActive(false);
        _vegetation.SetActive(false);

        //set random adventure with prefabs - either monster or power up
    }

    public void ResetAdventure()
    {
        var vegetationPrefabs = Constants.VegetationPrefabs;
        var randomIndex = Random.Range(0, vegetationPrefabs.Length);
        var randomVegetation = $"{Constants.VegetationDirectory}{vegetationPrefabs[randomIndex]}";
        
        var vegetation = Resources.Load(randomVegetation) as GameObject;

        if (vegetation == null)
        {
            _vegetation.SetActive(true);
        }
        else
        {
            GameObject newVegetation = Instantiate(vegetation, _vegetation.transform.position, Random.rotation, gameObject.transform);
            newVegetation.transform.localScale = _vegetation.transform.localScale;

            Destroy(_vegetation);
            _vegetation = newVegetation;
        }

        StartTimer();
    }

    //make text move then!
    private void StartTimer()
    {
        if (_countDown == null)
        {
            var countdownPos = Camera.main.WorldToScreenPoint(_vegetation.transform.position);
            _countDown = Instantiate(TimerTextPrefab, Canvas.transform);
            _countDown.transform.position = countdownPos;
        }

        _countDown.enabled = true;
        _hasTimeOut = true;
        _timeLeft = RechargeTime;
    }

    private void StopTimer()
    {
        _countDown.enabled = false;
        _hasTimeOut = false;
    }

    //move to other class - to detect help or item
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (_trackableStatus.Contains(newStatus))
        {
            Debug.Log("detected something!");
        }
    }
}
