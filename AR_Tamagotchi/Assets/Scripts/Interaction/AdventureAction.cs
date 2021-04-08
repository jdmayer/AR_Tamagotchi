using System.Collections;
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
                if (RandomAdventure.IsFighting)
                {
                    StartCoroutine(Constants.TickleEnemy);
                }
                else
                {
                    RandomAdventure.UseGemStone();
                }
            }
        }

        public void OnButtonReleased(VirtualButtonBehaviour vb)
        {
            if (RandomAdventure.IsActive)
            {
                StopCoroutine(Constants.TickleEnemy);
            }
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
