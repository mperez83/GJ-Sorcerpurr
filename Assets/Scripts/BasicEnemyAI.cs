using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    public int health;
    public float speed;
    public int scoreValue;

    float damageTintStrength;

    [Range(0, 100)]
    public int powerupDropPercentageChance;
    public GameObject[] powerupPrefabs;

    bool obliterated = false;
    float obliteratedAngle;
    float obliteratedForce;
    float obliteratedRotateSpeed;

    SpriteRenderer sr;



    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!obliterated)
        {
            Vector2 moveAmount = new Vector2(speed, 0);
            transform.Translate(moveAmount * Time.deltaTime);
        }
        else
        {
            Vector2 dir = Quaternion.Euler(0, 0, obliteratedAngle) * Vector2.right;
            Vector2 moveAmount = dir * obliteratedForce;
            transform.Translate(moveAmount * Time.deltaTime, Space.World);
            transform.Rotate(new Vector3(0, 0, obliteratedRotateSpeed * 10) * Time.deltaTime);
            if (GameMaster.instance.GetIfOutOfBounds(transform.position)) Destroy(gameObject);
        }

        //Handle damage tinting
        damageTintStrength -= Time.deltaTime * 5;
        if (damageTintStrength < 0) damageTintStrength = 0;
        sr.color = new Color(1, 1 - damageTintStrength, 1 - damageTintStrength);
    }



    public void GiveKillRewards()
    {
        ScoreUI.instance.AddScore(scoreValue);
        if (powerupPrefabs.Length != 0 && Random.Range(0f, 1f) <= (powerupDropPercentageChance / 100f))
            Instantiate(powerupPrefabs[Random.Range(0, powerupPrefabs.Length)], transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            GameOverHandler.instance.ActivateGameOver();
        }
        else if (other.CompareTag("Fireball"))
        {
            health -= 1;
            damageTintStrength = 0.8f;
            Destroy(other.gameObject);

            if (health <= 0)
            {
                GiveKillRewards();
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("Catpaw"))
        {
            health -= 10;
            damageTintStrength = 1f;

            if (health <= 0)
            {
                GiveKillRewards();

                obliterated = true;
                obliteratedAngle = other.transform.localEulerAngles.z - 90 + Random.Range(-45f, 45f);
                obliteratedForce = Random.Range(20f, 25f);
                obliteratedRotateSpeed = (Random.Range(0, 2) == 0) ? Random.Range(-180f, -90f) : Random.Range(90f, 180f);

                CameraShakeHandler.instance.SetIntensity(0.4f);
                Destroy(GetComponent<Rigidbody2D>());
            }
        }
    }
}