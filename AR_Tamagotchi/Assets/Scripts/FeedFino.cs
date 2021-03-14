using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Autorin: Helena Wilde
/// </summary>

public class FeedFino : MonoBehaviour
{
    public GameObject bananeObject;
    public GameObject burgerObject;
    public GameObject icecreamObject;

    int banane = 1;
    int burger = 2;
    int icecream = 3;

    // Start is called before the first frame update
    void Start()
    {
        int randomZahl = 0;
        randomZahl = Random.Range(0, 2);

        if (banane == randomZahl)
        {
            Debug.Log ("Fino schmeckt das");
        }
        else
        {
            Debug.Log ("Fino schmeckt das nicht");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
        
}
