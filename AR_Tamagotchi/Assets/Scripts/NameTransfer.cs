using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Autorin: Helena Wilde
/// </summary>

public class NameTransfer : MonoBehaviour
{
    public static string InputName;
    public Text showName;

    private void Start()
    {
        showName.text = InputName;
    }
}
