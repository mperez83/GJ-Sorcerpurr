using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float spawnTimer;
    public float spawnTimerLength;
    public LayerMask interactionMask;

    public GameObject enemyPrefab;



    void Start()
    {
        spawnTimer = spawnTimerLength;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = spawnTimerLength;

            float randomAngle = Random.Range(20f, 160f);
            Vector2 direction = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)).normalized; 
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 100f, interactionMask);
            
            Debug.DrawRay(transform.position, direction * hit.distance, Color.red, 1f);
            if (hit)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, hit.point, Quaternion.identity);
                Vector3 dir = transform.position - newEnemy.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                newEnemy.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
}