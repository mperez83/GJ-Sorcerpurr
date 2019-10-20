using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI instance;

    int score;
    int nextMeowScore = 100;

    public PlayerController player;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;



    void Start()
    {
        instance = this;
        highscoreText.text = "Highscore: " + GameMaster.instance.highscore.ToString();
    }

    void Update()
    {
        
    }



    public int GetScore()
    {
        return score;
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score.ToString();

        if (score >= nextMeowScore)
        {
            nextMeowScore += 100;
            player.GetComponent<AudioSource>().Play();
        }
    }
}