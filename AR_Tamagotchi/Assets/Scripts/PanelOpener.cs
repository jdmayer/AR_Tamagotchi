using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Autor: Helena Wilde
/// </summary>
public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;

    public void OpenPanel()
    {
        if(Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
                }
    }
}
