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
        [SerializeField] private AnimatorOverrideController[] overrideControllers;
        [SerializeField] private AnimatorOverrider overrider;

        private Vector3 originalPosition;
        public float speed = 0.1f;

        public Text test;


        void Start()
        {
            //originalPosition = transform.position; // new Vector3(Input.GetAxis(Constants.Horizontal), 0f, Input.GetAxis(Constants.Vertical));
        }

        private bool isMovingBack = false;

        void Update()
        {

            if (originalPosition == null)
            {
                Debug.Log("is null");
                originalPosition = transform.position; // new Vector3(Input.GetAxis(Constants.Horizontal), 0f, Input.GetAxis(Constants.Vertical));
                Debug.Log(originalPosition);

            }

            if (Input.GetButtonUp(Constants.Jump))
            {
                overrider.SetTrigger();
                //overrider.SetAnimations(overrideControllers[1]);
            }

            if (Input.GetKeyUp("t"))
            {
                Debug.Log(originalPosition);

                Debug.Log("move back to target! teleport!!");
                //transform.position = originalPosition;
                //Debug.Log(originalPosition);
                //var test = (transform.position - originalPosition).normalized;
                //Debug.Log(test);

                //transform.position +=  test * 0.1f * Time.deltaTime;
                isMovingBack = true;


            }

            if (Input.GetKeyUp("c"))
            {
                test.text = transform.position.ToString();
                isMovingBack = false;
            }

            // behave according to current state:
            if (isMovingBack)
            {
                test.text = transform.position.ToString();

                transform.position += (transform.position - originalPosition).normalized * 1f * Time.deltaTime;
            }

            if (!isMovingBack)
            {
                var xDirection = Input.GetAxis(Constants.Horizontal);
                var zDirection = Input.GetAxis(Constants.Vertical);
                Vector3 moveDirection = new Vector3(xDirection, 0.0f, zDirection);
            
                transform.position += moveDirection * speed;
            }
            
        }

        //do not let move too far from marker
    }
}