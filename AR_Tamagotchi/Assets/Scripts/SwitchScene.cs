using Character;
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
        public CharacterBasic Character;

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
                    Character.SetPlayerPrefs();
                    SceneManager.LoadScene(Constants.SceneMain);
                    break;
                case Constants.SceneMain:
                    Character.SetPlayerPrefs();
                    SceneManager.LoadScene(Constants.SceneAdventure);
                    break;
            }
        }
    }
}