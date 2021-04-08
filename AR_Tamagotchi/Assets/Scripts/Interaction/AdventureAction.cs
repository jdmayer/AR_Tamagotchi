using UnityEngine;
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

        void Update()
        {

        }
        
        public void OnButtonPressed(VirtualButtonBehaviour vb)
        {
            Debug.Log("PRESSSS!!");
            //call some method of random Adventure!
            //allow tickling! - on pressed - start gíggle animation
            //on released stop it!
        }

        public void OnButtonReleased(VirtualButtonBehaviour vb)
        {
            Debug.Log("Release the kraken!!");
        }
    }
}
