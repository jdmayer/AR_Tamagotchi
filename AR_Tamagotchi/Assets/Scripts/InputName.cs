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
    public GameObject StartButton;

    public void StoreName()
    {
        theName = inputField.GetComponent<Text>().text;
        
        if(theName == "") 
        {
            textDisplay.GetComponent<Text>().text = "The name is empty";
        }
        else 
        {
            StartButton.SetActive(true);
            textDisplay.GetComponent<Text>().text = "The name of your Fino is " + theName;
            NameTransfer.InputName = theName; 
        }
    }
}
