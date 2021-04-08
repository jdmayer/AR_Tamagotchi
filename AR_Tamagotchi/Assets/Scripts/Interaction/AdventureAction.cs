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
            switch(RandomAdventure.State)
            {
                case AdventureState.IsTickling:
                    StartCoroutine(Constants.TickleEnemy);
                    break;
                case AdventureState.HasGem:
                    RandomAdventure.UseGemStone();
                    break;
            }
        }

        public void OnButtonReleased(VirtualButtonBehaviour vb)
        {
            switch (RandomAdventure.State)
            {
                case AdventureState.IsTickling:
                    StopCoroutine(Constants.TickleEnemy);
                    break;
                case AdventureState.HasGem:
                    RandomAdventure.UsedGemStone();
                    break;
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

        public void StopLaughing()
        {
            //RandomAdventure.GetActiveEnemy().GetComponent<Animator>().SetTrigger(Constants.Laugh);

        }
    }
}
