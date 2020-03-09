using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour 
{
    public Transform firePoint;
    public GameObject ballPrefab;
    private Slider ballSpeedSlider;

    void Start()
    {
        ballSpeedSlider = GameObject.Find("Slider").GetComponent<Slider>();
    }
    
    void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicked");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider == gameObject.GetComponent<Collider2D>())
            {
                ShootBall();
            }
        }
        
    }

    void ShootBall()
    {
        Instantiate(ballPrefab.GetComponent<Ball>().SetSpeed(ballSpeedSlider.value), firePoint.position, firePoint.rotation);
    }
}
