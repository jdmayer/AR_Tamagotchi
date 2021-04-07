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
            FindObjectOfType<DialogManager>().StartDialog(dialog);
        }
    }
}