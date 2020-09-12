using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
	public SelectedElementType type = SelectedElementType.Ball;
	public string id = "";
	public ElemColor color;
    public float speed = 20f;
    public Sprite originalSprite;
    public Sprite hitSprite;

	private Rigidbody2D rb;
	private GameManager gameManager;

	private bool markForDestruction = false;
	
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed * gameManager.GetSpeedMultiplier();

		if (id == "")
		{
			id = "0" + (int)color;
			RandomString randomstring = new RandomString();
			id += randomstring.CreateRandomString(8);
		}
		else if (!id[0].Equals("0".ToCharArray()[0]))
		{
			id = "0" + id;
		}
	}

    void Update()
    {
        rb.velocity = (rb.velocity.normalized) * speed * gameManager.GetSpeedMultiplier();
        if (markForDestruction && rb.velocity.Equals(Vector2.zero))
        {
            rb.velocity = (rb.velocity.normalized) * speed * gameManager.GetSpeedMultiplier();
            Destroy(this.gameObject);
        }
		if (rb.velocity.Equals(Vector2.zero))
		{
			markForDestruction = true;
		}
		else
		{
			markForDestruction = false;
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
		string SOstring = id;
		SOstring += "," + speed;
		SOstring += "," + transform.position.x + "," + transform.position.y;
		SOstring += "," + transform.rotation.eulerAngles.z;

		return SOstring;
	}

	public void BallFromSO(string SOball)
	{
		string[] SOstring = SOball.Split(new[] { "," }, StringSplitOptions.None);

		id = SOstring[0];
		speed = float.Parse(SOstring[1]);
		transform.position = new Vector3(float.Parse(SOstring[2]), float.Parse(SOstring[3]), 0);
		transform.eulerAngles = new Vector3(0, 0, float.Parse(SOstring[4]));
	}
}
