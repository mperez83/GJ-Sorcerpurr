using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverHandler : MonoBehaviour
{
    public static GameOverHandler instance;

    public TextMeshProUGUI newHighscoreText;
    public GameObject leftSecret;
    public GameObject rightSecret;



    void Start()
    {
        instance = this;
        if (Random.Range(0, 100) == 0) leftSecret.SetActive(true);
        if (Random.Range(0, 100) == 0) rightSecret.SetActive(true);
    }

    public void ActivateGameOver()
    {
        Time.timeScale = 0;

        if (ScoreUI.instance.GetScore() > GameMaster.instance.highscore)
        {
            GameMaster.instance.highscore = ScoreUI.instance.GetScore();
            ScoreUI.instance.highscoreText.text = "Highscore: " + GameMaster.instance.highscore.ToString();
            newHighscoreText.gameObject.SetActive(true);
            newHighscoreText.text = "New highscore!\n(" + GameMaster.instance.highscore.ToString() + ")";
        }

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