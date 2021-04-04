using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace Monobehaviours
{
    public class Mark : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {
            if (gameObject.activeSelf)
            {
                transform.Rotate(0, 1, 0);
            }
        }
    }
}
