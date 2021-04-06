using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace UI
{
    public class DialogManager : MonoBehaviour
    {
        public Text NameText;
        public Text DialogText;
        public Animator Animator;

        private Queue<string> _sentences;
        void Start()
        {
            _sentences = new Queue<string>();
        }

        public void StartDialog(Dialog dialog)
        {
            Animator.SetBool(Constants.IsOpen, true);
            _sentences.Clear();

            NameText.text = dialog.Name;
            foreach (var sentence in dialog.Sentences)
            {
                _sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (_sentences.Count == 0)
            {
                QuitDialog();
                return;
            }

            StopAllCoroutines();
            StartCoroutine(TypeSentence(_sentences.Dequeue()));
        }

        public void QuitDialog()
        {
            Animator.SetBool(Constants.IsOpen, false);
        }

        IEnumerator TypeSentence(string sentence)
        {
            DialogText.text = string.Empty;
            foreach (var letter in sentence.ToCharArray())
            {
                DialogText.text += letter;
                yield return null;
            }
        }
    }
}