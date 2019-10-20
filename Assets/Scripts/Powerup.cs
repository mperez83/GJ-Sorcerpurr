using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType { AttackSpeed, Catpaw };
    public PowerupType powerupType;

    float mainDeg;
    float sinVal;

    AudioSource audioSource;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();

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
            audioSource.Play();

            LeanTween.cancel(gameObject);
            LeanTween.scale(gameObject, transform.localScale * 1.25f, 0.5f).setEase(LeanTweenType.easeOutCubic);
            LeanTween.alpha(gameObject, 0, 0.5f).setOnComplete(() =>
            {
                LeanTween.cancel(gameObject);
                Destroy(gameObject);
            });
        }
    }
}