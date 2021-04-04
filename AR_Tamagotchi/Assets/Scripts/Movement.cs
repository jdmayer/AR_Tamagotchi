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
        //[SerializeField] private AnimatorOverrideController[] overrideControllers;
        //[SerializeField] private AnimatorOverrider overrider;

        public AudioSource audioSource;
        public AudioClip jumpSound;

        private Vector3 originalPosition;
        public float speed = 0.05f;

        public Text test;


        void Start()
        {
            //originalPosition = transform.position; // new Vector3(Input.GetAxis(Constants.Horizontal), 0f, Input.GetAxis(Constants.Vertical));
            audioSource = GetComponent<AudioSource>();
        }

        private bool isMovingBack = false;

        void Update()
        {
            var hasDoubleTapped = Input.touches.Length == 0 ? false : Input.GetTouch(0).tapCount == 2;
            if (Input.GetButtonUp(Constants.Jump) || hasDoubleTapped)
            {
                this.GetComponent<Animator>().SetTrigger(Constants.Loop);
                audioSource.PlayOneShot(jumpSound, 1);
            }

            //if (Input.GetKeyUp("t"))
            //{
            //    Debug.Log(originalPosition);

            //    Debug.Log("move back to target! teleport!!");
            //    //transform.position = originalPosition;
            //    //Debug.Log(originalPosition);
            //    //var test = (transform.position - originalPosition).normalized;
            //    //Debug.Log(test);

            //    //transform.position +=  test * 0.1f * Time.deltaTime;
            //    isMovingBack = true;


            //}

            //if (Input.GetKeyUp("c"))
            //{
            //    transform.position += (transform.position - originalPosition).normalized * 1f * Time.deltaTime;
            //    //test.text = originalPosition.ToString() + " " + transform.position.ToString();
            //    //isMovingBack = false;

            //}

            // behave according to current state:
            //if (isMovingBack)
            //{
            //    test.text = transform.position.ToString();

            //    transform.position += (transform.position - originalPosition).normalized * 1f * Time.deltaTime;
            //}

            var xDirection = Input.GetAxis(Constants.Horizontal);
            var zDirection = Input.GetAxis(Constants.Vertical);
            Vector3 moveDirection = new Vector3(xDirection, 0.0f, zDirection);

            transform.position += moveDirection * speed;
            
            if (moveDirection != Vector3.zero)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else
            {
                audioSource.Stop();
            }
        }

        //do not let move too far from marker
    }
}