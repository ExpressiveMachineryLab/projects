using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
	public ElemColor color;
    Vector2 forceVector;
    public float speed = 20f;
    public Rigidbody2D rb;
    public Sprite originalSprite;
    public Sprite hitSprite;
    private GameManager gameManager;
	
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb.velocity = transform.up * speed;
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

}
