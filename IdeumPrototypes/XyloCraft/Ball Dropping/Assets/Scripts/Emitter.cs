using System.Collections;
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
    public Animator emitterAnimator;
    public int color;

    private Slider ballSpeedSlider;
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;
    private bool isBeingRotated = false;
    private float clickTimer;
    private bool clickTimerOn;

    void Start()
    {
        ballSpeedSlider = GameObject.Find("Slider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clickTimerOn) 
        {
            clickTimer += Time.deltaTime;
        }

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
            if (hit.collider != null && hit.collider.CompareTag("Rotator") && hit.collider == gameObject.transform.GetChild(1).GetComponent<Collider2D>())
            {
                Debug.Log("yes");
                isBeingRotated = true;
                
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isBeingRotated = false;
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

        clickTimer = 0;
        clickTimerOn = true;
        isBeingHeld = true;
    }

    private void OnMouseUp()
    {
        clickTimerOn = false;
        if (clickTimer < 0.15) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider == gameObject.GetComponent<Collider2D>())
            {
                ShootBall();
                emitterAnimator.SetTrigger("Shoot");

            }
        }
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

    public int GetColor() 
    {
        return color;
    }
}
