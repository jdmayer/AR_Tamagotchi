using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Autorin: Helena Wilde
/// </summary>

public class InputName : MonoBehaviour
{
    public static string theName;
    public GameObject inputField;
    public GameObject textDisplay;

    public void StoreName()
    {
        theName = inputField.GetComponent<Text>().text;
        textDisplay.GetComponent<Text>().text = "The name of your Fino is " + theName;
        NameTransfer.InputName = theName; 
    }
}
