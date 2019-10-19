using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    float attackInterval;
    public float attackIntervalLength;

    public GameObject fireballPrefab;



    void Start()
    {
        attackInterval = attackIntervalLength;
    }

    void Update()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector2 moveAmount = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(moveAmount * moveSpeed * Time.deltaTime, Space.World);

        //Attack
        attackInterval -= Time.deltaTime;

        if (Input.GetMouseButton(0) && attackInterval <= 0)
        {
            attackInterval = attackIntervalLength;

            GameObject newFireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            newFireball.transform.rotation = transform.rotation;
        }
    }
}