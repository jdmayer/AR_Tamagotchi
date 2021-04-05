using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Autorin: Helena Wilde
/// </summary>

public class StartPage : MonoBehaviour
{
    public GameObject StartButton;

    public void Start()
    {
        StartButton.SetActive(false);
    }
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }
    public void StartGame()
    {
            SceneManager.LoadScene("AR_Main_FinoMarker");
    }
}
