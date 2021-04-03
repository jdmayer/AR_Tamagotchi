using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Vuforia;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Monobehaviours
{
    public class SwitchScene : MonoBehaviour, IVirtualButtonEventHandler
    {
        public GameObject adventureButton;

        // TODO
        // Set new Fino attributes
        // not same as those used in Helenas Part
        void Start()
        {
            adventureButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        }

        public void OnButtonPressed(VirtualButtonBehaviour behaviour) 
        {
        }

        public void OnButtonReleased(VirtualButtonBehaviour behaviour)
        {
            switch(SceneManager.GetActiveScene().name)
            {
                case Constants.SceneAdventure:
                    SceneManager.LoadScene(Constants.SceneMain);
                    break;
                case Constants.SceneMain:
                    SceneManager.LoadScene(Constants.SceneAdventure);
                    break;
            }
        }
    }
}