using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    AudioSource audioSource;
    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;

    [HideInInspector]
    public float highscore = 0;

    [HideInInspector]
    public float screenTopEdge;
    [HideInInspector]
    public float screenBottomEdge;
    [HideInInspector]
    public float screenLeftEdge;
    [HideInInspector]
    public float screenRightEdge;



    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);

        audioSource = GetComponent<AudioSource>();
        PlayMainMenuMusic();

        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        screenTopEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, -(Camera.main.transform.position.z))).y;
        screenBottomEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -(Camera.main.transform.position.z))).y;
        screenLeftEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -(Camera.main.transform.position.z))).x;
        screenRightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, -(Camera.main.transform.position.z))).x;
    }



    public void PlayMainMenuMusic()
    {
        audioSource.clip = mainMenuMusic;
        audioSource.Play();
    }

    public void PlayGameMusic()
    {
        audioSource.clip = gameMusic;
        audioSource.Play();
    }

    public bool GetIfOutOfBounds(Vector2 pos)
    {
        if (pos.y > screenTopEdge || pos.y < screenBottomEdge || pos.x < screenLeftEdge || pos.x > screenRightEdge)
            return true;
        else
            return false;
    }
}