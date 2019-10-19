using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    public float speed;

    void Update()
    {
        Vector2 moveAmount = new Vector2(0, -speed);
        transform.Translate(moveAmount * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            GameOverHandler.instance.ActivateGameOver();
        }
        else if (other.CompareTag("Fireball"))
        {
            ScoreUI.instance.AddScore(1);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}