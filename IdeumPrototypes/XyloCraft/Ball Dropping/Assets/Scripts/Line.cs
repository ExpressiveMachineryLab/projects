using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Dropdown codeStateDropdown;
    public Dropdown ballStateDropdown;
    public Dropdown lineStateDropdown;

    private int codeState;
    private int ballState;
    private int lineState;
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

        codeStateDropdown = GameObject.Find("StateDropdown").GetComponent<Dropdown>();
        codeState = codeStateDropdown.value;

        ballStateDropdown = GameObject.Find("IfDropdown").GetComponent<Dropdown>();
        ballState = ballStateDropdown.value;

        lineStateDropdown = GameObject.Find("HitsDropdown").GetComponent<Dropdown>();
        lineState = lineStateDropdown.value;
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
        if (ballStateDropdown.value != ballState)
        {
            ballState = ballStateDropdown.value;
            Debug.Log(ballStateDropdown.value);
        }
        if (lineStateDropdown.value != lineState)
        {
            lineState = lineStateDropdown.value;
            Debug.Log(lineStateDropdown.value);
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
        StartCoroutine(ChangeSprite(0.15f, collision));

        PerformCodeBehvaior(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        //playClip.Stop();
    }

    private IEnumerator ChangeSprite(float seconds, Collision2D collision)
    {
        Sprite originalObject = collision.gameObject.GetComponent<Ball>().originalSprite;
        Sprite hitObject = collision.gameObject.GetComponent<Ball>().hitSprite;
        SpriteRenderer collidedObject = collision.gameObject.GetComponent<SpriteRenderer>();
        collidedObject.sprite = hitObject;
        yield return new WaitForSeconds(seconds);
        collidedObject.sprite = originalObject;
    }

    private void PerformCodeBehvaior(Collision2D collision) {
        //  Destroy
        if (codeStateDropdown.value == 1)
        {
            //Debug.Log("destroy ready " + this);
            // if ball is correct ball and hit correct line
            Destroy(this.gameObject);
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

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
