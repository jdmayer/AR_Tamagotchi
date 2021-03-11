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

        // Start is called before the first frame update
        void Start()
        {
            adventureButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        }

        // Update is called once per frame
        public void OnButtonPressed(VirtualButtonBehaviour behaviour) 
        {
            Debug.Log("adventure button pressed");
            //SceneManager.LoadScene(Constants.SceneAdventure);
        }

        public void OnButtonReleased(VirtualButtonBehaviour behaviour)
        {
            Debug.Log("adventure button released");
            SceneManager.LoadScene(Constants.SceneAdventure);
        }
    }
}