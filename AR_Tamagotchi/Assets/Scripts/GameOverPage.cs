using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Autorin: Helena Wilde
/// </summary>

public class GameOverPage : MonoBehaviour
{
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game closed");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("AR_Main_Start");
    }
}
