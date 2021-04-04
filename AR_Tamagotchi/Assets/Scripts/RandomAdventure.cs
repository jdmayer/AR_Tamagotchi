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

    private GameObject _exclamationMark;

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
    }

    void Update()
    {
        float actualDistance = Vector3.Distance(Player.transform.position, transform.position);
        if (actualDistance <= MinDistance)
        {
            Debug.Log("SURPRISE!!");
            _exclamationMark.SetActive(true);
        }
        else
        {
            _exclamationMark.SetActive(false);
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (_trackableStatus.Contains(newStatus))
        {
            Debug.Log("detected something!");
        }
    }
}
