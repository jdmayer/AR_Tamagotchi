using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffScreenPointer : MonoBehaviour
{
    public GameObject Pointer; //points
    public GameObject Target; //canvas
    //transform - lays on fino

    public Text test;

    void Start()
    {
        
    }

    void Update()
    {
        var screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        //works more precisely than GetComponent<Renderer>().isVisible
        if (screenPosition.z > 0 &&
            screenPosition.x > 0 && screenPosition.x < Screen.width &&
            screenPosition.y > 0 && screenPosition.y < Screen.height)
        {
            if (Pointer.activeSelf)
            {
                Pointer.SetActive(false);
            }
        }
        else
        {
            if (!Pointer.activeSelf)
            {
                Pointer.SetActive(true);
            }

            var targetPos = Camera.main.WorldToViewportPoint(Target.transform.position); 
            var transPos = Camera.main.WorldToViewportPoint(transform.position); 
            Vector2 direction = Target.transform.position - transform.position;
            //RaycastHit2D ray = Physics2D.Raycast(transform.position, direction);
            RaycastHit2D ray = Physics2D.Raycast(targetPos, transPos);


            //var direction = Target.transform.position - transform.position;
            //Vector2 t1 = new Vector2(transform.position.x, transform.position.y);
            //Vector2 t2 = new Vector2(direction.x, direction.y);
            //RaycastHit2D ray = Physics2D.Raycast(t1, t2);

            if (ray.collider != null && ray.point != null)
            {
                test.text = ray.point.ToString();

                Pointer.transform.position = new Vector3(ray.point.x, ray.point.y, 0);
                Debug.Log("collision?");
            }
            else
            {
                Debug.Log("no?");
            }
        }
    }
}
