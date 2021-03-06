﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed;

    float mainDeg;
    float sinVal;
    public float sinSpeed;
    public float sinIntensity;



    void Start()
    {
        mainDeg = Random.Range(45f, 135f);
        if (Random.Range(0, 2) == 0) mainDeg += 180;
    }

    void Update()
    {
        mainDeg += (360 * (sinSpeed / 1f)) * Time.deltaTime;
        while (mainDeg > 360) mainDeg = mainDeg - 360;
        sinVal = sinIntensity * Mathf.Sin(mainDeg * Mathf.Deg2Rad);

        transform.Translate(new Vector2(sinVal, -speed) * Time.deltaTime);

        if (GameMaster.instance.GetIfOutOfBounds(transform.position)) Destroy(gameObject);
    }
}