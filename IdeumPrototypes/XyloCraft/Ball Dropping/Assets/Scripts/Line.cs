using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Dropdown codeStateDropdown;

    private int codeState;
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;

    BoxCollider2D thisCollider;
    AudioSource playClip;

    public AudioClip collisionSound;

    void Start()
    {
        thisCollider = GetComponent<BoxCollider2D>();
        playClip = GetComponent<AudioSource>();

        codeState = codeStateDropdown.value;
        // thisCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }
        if (codeStateDropdown.value != codeState)
        {
            codeState = codeStateDropdown.value;
            Debug.Log(codeStateDropdown.value);

            
        }
    }

    private void OnMouseDown()
    {
        isBeingHeld = true;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isBeingHeld = true;
        }
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playClip.clip = collisionSound;
        playClip.Play();
        //SpriteRenderer collidedObject = collision.gameObject.transform.GetComponent<SpriteRenderer>();
        //collidedObject.sprite = (Sprite)Resources.Load<Sprite>(collidedObject.sprite + "_hit");
        //collision.gameObject.transform.GetComponent<SpriteRenderer>().sprite = collision.gameObject.ToString() + "_hit";

        //  Destroy
        if (codeStateDropdown.value == 1)
        {
            Debug.Log("destroy ready");
        }

        //  Loop
        if (codeStateDropdown.value == 2)
        {
            Debug.Log("loop ready");
        }

        //  Increase Pitch
        if (codeStateDropdown.value == 3)
        {
            Debug.Log("increase pitch ready");
        }

        //  Decrease Pitch
        if (codeStateDropdown.value == 4)
        {
            Debug.Log("decrease pitch ready");
        }

        //  Change Color
        if (codeStateDropdown.value == 5)
        {
            Debug.Log("change color ready");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        //playClip.Stop();
    }
}
