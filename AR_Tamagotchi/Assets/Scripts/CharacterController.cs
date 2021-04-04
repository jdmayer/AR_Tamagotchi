using System.Collections;
using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Monobehaviours
{
    public class CharacterController : MonoBehaviour
    {
        private bool isSoundPlayed = false;

        void Update()
        {
            if (!isSoundPlayed && transform.localPosition.y < .05f)
            {
                isSoundPlayed = true;
                StartCoroutine(DelayPlaySound());
            }
        }

        private IEnumerator DelayPlaySound()
        {
            yield return new WaitForSeconds(.2f);
            GetComponent<AudioSource>().Play();
        }

        public void MoveCharacter()
        {
            transform.localPosition += new Vector3(0, 10, 0);
            transform.eulerAngles += new Vector3(5, 20, 5);
            isSoundPlayed = false;
        }
    }
}