using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}