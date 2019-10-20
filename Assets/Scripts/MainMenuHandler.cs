using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuHandler : MonoBehaviour
{
    public TextMeshProUGUI highscoreText;

    void Start()
    {
        highscoreText.text = "Highscore: " + GameMaster.instance.highscore.ToString();
    }

    public void PlayButton()
    {
        GameMaster.instance.PlayGameMusic();
        SceneManager.LoadScene("Main");
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }
}