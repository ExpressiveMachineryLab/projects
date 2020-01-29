﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    Vector2 forceVector;
    public float speed = 20f;
    public Rigidbody2D rb;
    public Sprite originalSprite;
    public Sprite hitSprite;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    void Update()
    {
        rb.velocity = (rb.velocity.normalized) * speed;
    }
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}