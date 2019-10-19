using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public static GameOverHandler instance;

    void Start()
    {
        instance = this;
    }

    public void ActivateGameOver()
    {
        Time.timeScale = 0;
        GetComponent<Canvas>().enabled = true;
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}