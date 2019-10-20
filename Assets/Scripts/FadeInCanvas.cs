using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInCanvas : MonoBehaviour
{
    public Image fadeImage;

    void Start()
    {
        fadeImage.CrossFadeAlpha(0, 1, false);
        LeanTween.delayedCall(gameObject, 1, () =>
        {
            Destroy(gameObject);
        });
    }
}