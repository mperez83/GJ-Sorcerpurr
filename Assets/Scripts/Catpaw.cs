using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catpaw : MonoBehaviour
{
    void Start()
    {
        transform.Translate(new Vector2(0, -1));
        LeanTween.alpha(gameObject, 0, 0.5f).setEase(LeanTweenType.easeInCubic).setOnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    void Update()
    {
        transform.Translate(new Vector2(0, -8) * Time.deltaTime);
    }
}