using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/// <summary>
/// Autorin: Helena Wilde
/// </summary>

public class RecognizeTeddy : MonoBehaviour, ITrackableEventHandler
{
    public GameObject canvas;
    public GameObject teddy;

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        var tamaScript = canvas.GetComponent<Tamagotchi>();
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED || newStatus == TrackableBehaviour.Status.TRACKED)
        {
            teddy.SetActive(true);
            tamaScript.SocializingTheFino();
        }
        else
        {
            teddy.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var trackableHandler = GetComponent<TrackableBehaviour>();
        trackableHandler.RegisterTrackableEventHandler(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}