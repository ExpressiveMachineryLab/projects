﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Emitter Class:
// Allows emitter to be dragged

public class Emitter : MonoBehaviour
{
    public Transform firePoint;
    public GameObject ballPrefab;
    public float speed = 5f;

    private Slider ballSpeedSlider;
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;
    private bool isBeingRotated = false;

    void Start()
    {
        ballSpeedSlider = GameObject.Find("Slider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }

        if (isBeingRotated)
        {
            Rotate();
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            Debug.Log(gameObject.transform.GetChild(1).GetComponent<Collider2D>()) ;
            if (hit.collider != null && hit.collider.tag == "Rotator" && hit.collider == gameObject.transform.GetChild(1).GetComponent<Collider2D>())
            {
                isBeingRotated = true;
                
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isBeingRotated = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider == gameObject.GetComponent<Collider2D>())
            {
                ShootBall();
            }
        }
    }

    private void OnMouseDown()
    {
        // Drag with left click
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        startPosX = mousePos.x - this.transform.localPosition.x;
        startPosY = mousePos.y - this.transform.localPosition.y;

        isBeingHeld = true;
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;

    }
    void ShootBall()
    {
        Instantiate(ballPrefab.GetComponent<Ball>().SetSpeed(ballSpeedSlider.value), firePoint.position, firePoint.rotation);
    }

    void Rotate()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rotation *= Quaternion.Euler(0, 0, -90);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime) ;
    }
}
