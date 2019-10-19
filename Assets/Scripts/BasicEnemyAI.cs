using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    public float speed;

    void Update()
    {
        Vector2 moveAmount = new Vector2(speed, 0);
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
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}