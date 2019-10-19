using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    float attackInterval;
    public float attackIntervalLength;
    bool fireballPowerupActive = false;
    float fireballPowerupTimer;

    public GameObject fireballPrefab;



    void Start()
    {
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
            if (fireballPowerupActive == true)
            {
                attackInterval = 0;
                fireballPowerupTimer -= Time.deltaTime;
                if (fireballPowerupTimer <= 0)
                {
                    fireballPowerupActive = false;
                    fireballPowerupTimer = 0;
                }
            }

            if (Input.GetMouseButton(0) && attackInterval <= 0)
            {
                attackInterval = attackIntervalLength;

                GameObject newFireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
                newFireball.transform.rotation = transform.rotation;
            }
        }
    }

    public void ActivatePowerup(Powerup.PowerupType powerupType)
    {
        switch (powerupType)
        {
            case Powerup.PowerupType.Fireball:
                fireballPowerupActive = true;
                fireballPowerupTimer = 8f;
                break;

            case Powerup.PowerupType.Catpaw:
                break;
        }
    }
}