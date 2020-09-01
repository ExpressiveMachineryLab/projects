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

	public Vector3 position;
	public Quaternion rotation;

	private Rigidbody2D rb;
	private GameManager gameManager;
	
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;

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

	public string BallToJSON()
	{
		position = transform.position;
		rotation = transform.rotation;

		return JsonUtility.ToJson(this);
	}

	public void BallFromJSON(string json)
	{
		JsonUtility.FromJsonOverwrite(json, this);

		transform.position = position;
		transform.rotation = rotation;
	}
}
