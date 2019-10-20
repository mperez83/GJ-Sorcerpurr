using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType { AttackSpeed, Catpaw };
    public PowerupType powerupType;

    float mainDeg;
    float sinVal;



    void Start()
    {
        LeanTween.delayedCall(gameObject, 5f, () =>
        {
            LeanTween.scale(gameObject, Vector3.zero, 5).setOnComplete(() =>
            {
                Destroy(gameObject);
            });
        });
    }

    void Update()
    {
        //Make it sway
        mainDeg += (360 * (1f / 2f)) * Time.deltaTime;
        while (mainDeg > 360) mainDeg = mainDeg - 360;
        sinVal = 5 * Mathf.Sin(mainDeg * Mathf.Deg2Rad);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, sinVal);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ActivatePowerup(powerupType);
            LeanTween.cancel(gameObject);
            Destroy(gameObject);
        }
    }
}