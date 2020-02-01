﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGravity : MonoBehaviour
{
    private float gravity = 9.8f;

    public Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity += Time.deltaTime * Vector3.down * gravity;
        transform.position += velocity * Time.deltaTime;
    }
}
