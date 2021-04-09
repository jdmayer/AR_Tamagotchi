using UnityEngine;

/// <summary>
/// Author: Janine Mayer
/// </summary>
namespace UI
{
    public class MarkRotation : MonoBehaviour
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