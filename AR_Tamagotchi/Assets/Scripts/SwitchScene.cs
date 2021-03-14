using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Assets.Scripts
{
    public class SwitchScene : MonoBehaviour, IVirtualButtonEventHandler
    {
        public GameObject adventureButton;

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