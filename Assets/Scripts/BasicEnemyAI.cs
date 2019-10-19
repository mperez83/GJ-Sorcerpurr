using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    public float speed;
    [Range(0, 100)]
    public int powerupDropPercentageChance;
    public GameObject[] powerupPrefabs;



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
            ScoreUI.instance.AddScore(1);

            if (powerupPrefabs.Length != 0 && Random.Range(0f, 1f) <= (powerupDropPercentageChance / 100f))
            {
                Instantiate(powerupPrefabs[Random.Range(0, powerupPrefabs.Length)], transform.position, Quaternion.identity);
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}