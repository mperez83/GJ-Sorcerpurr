using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI instance;

    int blankCount = 3;
    public Image blank1;
    public Image blank2;
    public Image blank3;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    public void UseBlank()
    {
        if (blankCount > 0)
        {
            blankCount--;
            GetComponent<AudioSource>().Play();

            switch (blankCount)
            {
                case 2:
                    //blank3.enabled = false;
                    LeanTween.scale(blank3.rectTransform, blank3.rectTransform.localScale * 1.5f, 0.5f).setEase(LeanTweenType.easeOutCubic);
                    LeanTween.value(gameObject, 1, 0, 0.5f).setOnUpdate((float val) =>
                    {
                        Color c = blank3.color;
                        c.a = val;
                        blank3.color = c;
                    });
                    break;

                case 1:
                    //blank2.enabled = false;
                    LeanTween.scale(blank2.rectTransform, blank2.rectTransform.localScale * 1.5f, 0.5f).setEase(LeanTweenType.easeOutCubic);
                    LeanTween.value(gameObject, 1, 0, 0.5f).setOnUpdate((float val) =>
                    {
                        Color c = blank2.color;
                        c.a = val;
                        blank2.color = c;
                    });
                    break;

                case 0:
                    //blank1.enabled = false;
                    LeanTween.scale(blank1.rectTransform, blank1.rectTransform.localScale * 1.5f, 0.5f).setEase(LeanTweenType.easeOutCubic);
                    LeanTween.value(gameObject, 1, 0, 0.5f).setOnUpdate((float val) =>
                    {
                        Color c = blank1.color;
                        c.a = val;
                        blank1.color = c;
                    });
                    break;
            }

            BasicEnemyAI[] foundObjects = FindObjectsOfType<BasicEnemyAI>();
            for (int i = 0; i < foundObjects.Length; i++)
            {
                foundObjects[i].GiveKillRewards();
                CameraShakeHandler.instance.SetIntensity(0.6f);
                Destroy(foundObjects[i].gameObject);
            }
        }
    }
}