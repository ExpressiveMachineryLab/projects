﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
	public SelectedElementType type = SelectedElementType.Ball;
	public ElemColor color;
    public float speed = 20f;
    public Sprite originalSprite;
    public Sprite hitSprite;

	private Rigidbody2D rb;
	private GameManager gameManager;
	
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed * gameManager.GetSpeedMultiplier();

		//Debug.Log(JsonUtility.ToJson(this));
	}

    void Update()
    {
        rb.velocity = (rb.velocity.normalized) * speed * gameManager.GetSpeedMultiplier();
        if (rb.velocity.Equals(Vector2.zero))
        {
            rb.velocity = (rb.velocity.normalized) * speed * gameManager.GetSpeedMultiplier();
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    public GameObject SetSpeed(float sliderSpeed)
    {
        speed = sliderSpeed;
        return this.gameObject;
    }

	public string BallToSO()
	{
		string SOstring = "0";
		SOstring += (int)color;
		SOstring += "i";
		SOstring += "," + speed;
		SOstring += "," + transform.position.x + "," + transform.position.y + "," + transform.position.z;
		SOstring += "," + transform.rotation.w + "," + transform.rotation.x + "," + transform.rotation.y + "," + transform.rotation.z;

		return SOstring;
	}

	public void BallFromSO(string SOball)
	{
		string[] SOstring = SOball.Split(new[] { "," }, System.StringSplitOptions.None);

		speed = float.Parse(SOstring[1]);
		Vector3 position = new Vector3
		{
			x = float.Parse(SOstring[2]),
			y = float.Parse(SOstring[3]),
			z = float.Parse(SOstring[4])
		};
		transform.position = position;
		Quaternion rotation = new Quaternion
		{
			w = float.Parse(SOstring[5]),
			x = float.Parse(SOstring[6]),
			y = float.Parse(SOstring[7]),
			z = float.Parse(SOstring[8])
		};
		transform.rotation = rotation;
	}
}
