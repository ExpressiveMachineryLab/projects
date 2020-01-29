using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Sprite originalSprite;
    public Sprite hitSprite;

    private Dropdown codeStateDropdown;
    private Dropdown ballStateDropdown;
    private Dropdown lineStateDropdown;

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
        StartCoroutine(ChangeSprite(0.15f, collision));
        PerformCodeBehvaior(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        //playClip.Stop();
    }

    private IEnumerator ChangeSprite(float seconds, Collision2D collision)
    {
        Sprite ballOriginalObject = collision.gameObject.GetComponent<Ball>().originalSprite;
        Sprite ballHitObject = collision.gameObject.GetComponent<Ball>().hitSprite;
        SpriteRenderer collidedObject = collision.gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer thisLineObject = this.gameObject.GetComponent<SpriteRenderer>();

        collidedObject.sprite = ballHitObject;
        thisLineObject.sprite = hitSprite;
        yield return new WaitForSeconds(seconds);
        collidedObject.sprite = ballOriginalObject;
        thisLineObject.sprite = originalSprite;
    }

    private void MakeSound()
    {
        playClip.clip = collisionSound;
        playClip.Play();
    }

    private IEnumerator DestroyObject(float seconds)
    {
        MakeSound();
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }

    private IEnumerator LoopSound(float seconds, int numLoops)
    {
        for (int i = 0; i < numLoops; i++)
        {
            MakeSound();
            yield return new WaitForSeconds(seconds);
        }
    }
    private IEnumerator ChangePitch(float seconds, float pitchLevel)
    {
        playClip.pitch = pitchLevel;
        MakeSound();
        yield return new WaitForSeconds(seconds);
        playClip.pitch = 1.0f;
    }

    private void PerformCodeBehvaior(Collision2D collision) {
        // None
        if (codeStateDropdown.value == 0)
        {
            MakeSound();
        }

        if (CorrectCollision(collision))
        {
            //  Destroy
            if (codeStateDropdown.value == 1)
            {
                StartCoroutine(DestroyObject(0.2f));
            }

            //  Loop
            if (codeStateDropdown.value == 2)
            {
                // pass in how many times to loop
                StartCoroutine(LoopSound(1f, 5));
            }
            //  Increase Pitch
            if (codeStateDropdown.value == 3)
            {
                StartCoroutine(ChangePitch(0.2f, 2.0f));
            }

            //  Decrease Pitch
            if (codeStateDropdown.value == 4)
            {
                StartCoroutine(ChangePitch(0.2f, 0.75f));
            }

            //  Change Color
            if (codeStateDropdown.value == 5)
            {
            }
        }
    }

    private bool CorrectCollision(Collision2D collision) {
        return collision.gameObject.tag == "Ball" + ballState && this.gameObject.tag == "Line" + lineState;
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
