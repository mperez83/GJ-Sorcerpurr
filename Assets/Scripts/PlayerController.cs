using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    float attackInterval;
    public float attackIntervalLength;

    bool attackSpeedPowerupActive = false;
    bool catPawPowerupActive = false;

    public GameObject fireballPrefab;
    public GameObject catPawPrefab;

    AudioSource audioSource;
    public AudioClip fireballShoot;
    public AudioClip catPawShoot;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        attackInterval = attackIntervalLength;
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            //Point the player towards the mouse position
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

            //Move the player
            Vector2 moveAmount = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            transform.Translate(moveAmount * moveSpeed * Time.deltaTime, Space.World);

            //Constrain the player to within the screen
            if (transform.position.y > GameMaster.instance.screenTopEdge) transform.position = new Vector2(transform.position.x, GameMaster.instance.screenTopEdge);
            if (transform.position.y < GameMaster.instance.screenBottomEdge + 1) transform.position = new Vector2(transform.position.x, GameMaster.instance.screenBottomEdge + 1);
            if (transform.position.x < GameMaster.instance.screenLeftEdge) transform.position = new Vector2(GameMaster.instance.screenLeftEdge, transform.position.y);
            if (transform.position.x > GameMaster.instance.screenRightEdge) transform.position = new Vector2(GameMaster.instance.screenRightEdge, transform.position.y);

            //Attack
            attackInterval -= Time.deltaTime;
            if (attackSpeedPowerupActive == true) attackInterval = 0;

            //Left click
            if (Input.GetMouseButton(0) && attackInterval <= 0)
            {
                //Ultra super hyper attack??
                if (catPawPowerupActive && attackSpeedPowerupActive)
                {
                    GameObject newFireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
                    newFireball.transform.rotation = transform.rotation;

                    GameObject newCatpaw = Instantiate(catPawPrefab, transform.position, Quaternion.identity);
                    newCatpaw.transform.rotation = transform.rotation;

                    audioSource.clip = catPawShoot;
                    audioSource.Play();
                }

                //Cat paw attack
                else if (catPawPowerupActive)
                {
                    GameObject newCatpaw = Instantiate(catPawPrefab, transform.position, Quaternion.identity);
                    newCatpaw.transform.rotation = transform.rotation;
                    attackInterval = 0.5f;

                    audioSource.clip = catPawShoot;
                    audioSource.Play();
                }

                //Attack speed active
                else if (attackSpeedPowerupActive)
                {
                    GameObject newFireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
                    newFireball.transform.rotation = transform.rotation;

                    audioSource.clip = fireballShoot;
                    audioSource.Play();
                }

                //Normal fireball attack
                else
                {
                    GameObject newFireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
                    newFireball.transform.rotation = transform.rotation;
                    attackInterval = attackIntervalLength;

                    audioSource.clip = fireballShoot;
                    audioSource.Play();
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameplayUI.instance.UseBlank();
            }
        }
    }



    public void ActivatePowerup(Powerup.PowerupType powerupType)
    {
        switch (powerupType)
        {
            case Powerup.PowerupType.AttackSpeed:
                attackSpeedPowerupActive = true;
                LeanTween.delayedCall(gameObject, 8f, () => { attackSpeedPowerupActive = false; });
                break;

            case Powerup.PowerupType.Catpaw:
                catPawPowerupActive = true;
                LeanTween.delayedCall(gameObject, 8f, () => { catPawPowerupActive = false; });
                break;
        }
    }
}