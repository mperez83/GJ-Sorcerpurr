using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float spawnTimer;
    float spawnTimerLength;
    public float initialSpawnTimerLength;

    float ttzstlTimer;
    public float timeToZeroSpawnTimerLength;

    public LayerMask interactionMask;

    public GameObject[] enemyPrefabs;
    public GameObject homunculusPrefab;



    void Start()
    {
        spawnTimerLength = initialSpawnTimerLength;
        spawnTimer = spawnTimerLength;
        ttzstlTimer = timeToZeroSpawnTimerLength;
    }

    void Update()
    {
        ttzstlTimer -= Time.deltaTime;
        if (ttzstlTimer < 0) ttzstlTimer = 0;

        spawnTimerLength = initialSpawnTimerLength * (ttzstlTimer / timeToZeroSpawnTimerLength);

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = spawnTimerLength;

            float randomAngle = Random.Range(20f, 160f);
            Vector2 direction = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)).normalized; 
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 100f, interactionMask);
            
            //Debug.DrawRay(transform.position, direction * hit.distance, Color.red, 1f);
            if (hit)
            {
                GameObject newEnemy;
                if (Random.Range(0, 100) == 0)
                    newEnemy = Instantiate(homunculusPrefab, hit.point, Quaternion.identity);
                else
                    newEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], hit.point, Quaternion.identity);

                Vector3 dir = transform.position - newEnemy.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                newEnemy.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                if (angle < -90) newEnemy.GetComponent<SpriteRenderer>().flipY = true;
            }
        }
    }
}