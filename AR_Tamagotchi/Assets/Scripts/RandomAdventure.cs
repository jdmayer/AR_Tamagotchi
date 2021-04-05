using System.Collections.Generic;
using UnityEngine;
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

    private float _nextRound = 0f;
    private bool _hasTimeOut = false;


    private GameObject _exclamationMark;
    private GameObject _questionMark;
    private GameObject _vegetation;
    private Transform _vegetationTransform;

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

        _exclamationMark = transform.Find(Constants.ExclamationMark).gameObject;
        _exclamationMark.SetActive(false);
        _questionMark = transform.Find(Constants.QuestionMark).gameObject;
        _questionMark.SetActive(false);
        _vegetation = transform.Find(Constants.Vegetation).gameObject;
        _vegetationTransform = _vegetation.transform;
    }

    void Update()
    {

        if (_hasTimeOut && Time.time > _nextRound)
        {            
            _hasTimeOut = false;
        }

        if (!_hasTimeOut)
        {
            SetAttentionMark();
        }


        // only for testing
        if (Input.GetKeyUp("d"))
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
            Debug.Log("SURPRISE!!");
            _questionMark.SetActive(true);
        }
        else
        {
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
        var randomVegetation = $"{Constants.VegetationDirectoy}{vegetationPrefabs[randomIndex]}";
        
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

        StartTimeOut();
    }

    private void StartTimeOut()
    {
        _hasTimeOut = true;
        _nextRound = Time.time + RechargeTime;
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (_trackableStatus.Contains(newStatus))
        {
            Debug.Log("detected something!");
        }
    }
}
