using UnityEngine;
using UnityEngine.UI;
using Utils;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Monobehaviours
{
    public class Movement : MonoBehaviour
    {
        private AudioSource _audioSource;
        public AudioClip jumpSound;

        public float speed = 0.05f;

        public Text test;


        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private bool isMovingBack = false;

        void Update()
        {
            var hasDoubleTapped = Input.touches.Length == 0 ? false : Input.GetTouch(0).tapCount == 2;
            if (Input.GetButtonUp(Constants.Jump) || hasDoubleTapped)
            {
                this.GetComponent<Animator>().SetTrigger(Constants.Loop);
                _audioSource.PlayOneShot(jumpSound, 1);
            }

            var xDirection = Input.GetAxis(Constants.Horizontal);
            var zDirection = Input.GetAxis(Constants.Vertical);
            Vector3 moveDirection = new Vector3(xDirection, 0.0f, zDirection);

            transform.position += moveDirection * speed;
            
            if (moveDirection != Vector3.zero)
            {
                if (!_audioSource.isPlaying)
                {
                    _audioSource.Play();
                }
            }
            else
            {
                _audioSource.Stop();
            }
        }
    }
}