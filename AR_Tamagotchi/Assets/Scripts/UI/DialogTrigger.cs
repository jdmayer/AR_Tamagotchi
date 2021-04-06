using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace UI
{
    public class DialogTrigger : MonoBehaviour
    {
        public Dialog dialog;
        
        public void TriggerDialog()
        {
            dialog = new Dialog { Name = "Janine", Sentences = new string[] { "hallo", "nein danke", "na dann" } };
            FindObjectOfType<DialogManager>().StartDialog(dialog);
        }
    }
}

//when starting the scene - set dialog
//welcome back!
//green stones - health
//blue - experience
//lila - energy
//enemies - beat them with jokes! we are stronger!