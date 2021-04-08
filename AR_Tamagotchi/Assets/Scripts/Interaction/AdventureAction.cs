﻿using System.Collections;
using UnityEngine;
using Utils;
using Vuforia;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Interaction
{
    public class AdventureAction : MonoBehaviour, IVirtualButtonEventHandler
    {
        public RandomAdventure RandomAdventure;

        void Start()
        {
            GameObject vb = GameObject.Find("VirtualButton");
            vb.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        }
                
        public void OnButtonPressed(VirtualButtonBehaviour vb)
        {
            if (RandomAdventure.IsActive)
            {
                StartCoroutine(Constants.TickleEnemy);
            }
            Debug.Log("PRESSSS!!");
        }

        public void OnButtonReleased(VirtualButtonBehaviour vb)
        {
            if (RandomAdventure.IsActive)
            {
                StopCoroutine(Constants.TickleEnemy);
            }
            Debug.Log("Release the kraken!!");
        }

        IEnumerable TickleEnemy()
        {
            while (true)
            {
                Debug.Log("tickle!!");
                //show him laughing - decrease anger - but only for so long
                yield return null;
            }
        }
    }
}
