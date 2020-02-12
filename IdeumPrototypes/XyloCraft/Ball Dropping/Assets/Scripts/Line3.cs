using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Line3 : MonoBehaviour
{
    public Sprite originalSprite;
    public Sprite hitSprite;

    //private CodeStateInformation codeInfo;
    private SendStateInformation3 blueBall;
    private SendStateInformation3 greenBall;
    private SendStateInformation3 redBall;
    private SendStateInformation3 yellowBall;
    private SoundManager soundMan;
    private LineArray lineArray;

    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;

    private int pitchLevel = 0;

    BoxCollider2D thisCollider;
    AudioSource playClip;

    public AudioClip collisionSound;
    
    void Start()
    {
        thisCollider = GetComponent<BoxCollider2D>();
        playClip = GetComponent<AudioSource>();
        //codeInfo = GameObject.Find("GameManager").GetComponent<CodeStateInformation>();
        blueBall = GameObject.Find("CodePanel - 3").GetComponent<SendStateInformation3>();
        greenBall = GameObject.Find("CodePanel - 3 (3)").GetComponent<SendStateInformation3>();
        redBall = GameObject.Find("CodePanel - 3 (1)").GetComponent<SendStateInformation3>();
        yellowBall = GameObject.Find("CodePanel - 3 (2)").GetComponent<SendStateInformation3>();
        soundMan = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        lineArray = GameObject.Find("GameManager").GetComponent<LineArray>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, 
                mousePos.y - startPosY, 0);
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
        //Debug.Log(codeInfo.getCodeState());
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

    public void MakeSound()
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
    private IEnumerator ChangePitch(float seconds, AudioClip audioClip)
    {
        playClip.PlayOneShot(audioClip);
        MakeSound();
        yield return new WaitForSeconds(seconds);
        playClip.pitch = 1.0f;
    }

    private IEnumerator ChangeColor(float seconds, GameObject oldColor, GameObject newColor, SendStateInformation3 ballColor)
    {
        newColor.transform.GetChild(0).gameObject.SetActive(false);
        playClip.PlayOneShot(soundMan.GetAudio(ballColor.getBallNumber(), pitchLevel));

        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }

    private void PerformCodeBehvaior(Collision2D collision)
    {
        //// None
        //if (codeInfo.getCodeState() == 0)
        //{
        //    MakeSound();
        //}
        Debug.Log(collision.gameObject.tag);
        Debug.Log(this.gameObject.tag);
        if (collision.gameObject.tag == "Ball0" && this.gameObject.tag == "Line" + blueBall.getLineState())
        {
            PerformCode(blueBall);
        }
        else if (collision.gameObject.tag == "Ball1" && this.gameObject.tag == "Line" + greenBall.getLineState())
        {
            PerformCode(greenBall);
        }
        else if (collision.gameObject.tag == "Ball2" && this.gameObject.tag == "Line" + redBall.getLineState())
        {
            PerformCode(redBall);
        }
        else if (collision.gameObject.tag == "Ball3" && this.gameObject.tag == "Line" + yellowBall.getLineState())
        {
            PerformCode(yellowBall);
        }

        else
        {
            MakeSound();
        }
    }

    //private bool CheckCorrectCollision(Collision2D collision)
    //{
    //    return collision.gameObject.tag == "Ball" + codeInfo.getBallState()
    //        && this.gameObject.tag == "Line" + codeInfo.getLineState();
    //}

    private void PerformCode(SendStateInformation3 ballColor)
    {
        // None
        if (ballColor.getCodeState() == 0)
        {
            MakeSound();
        }

        // Destroy
        if (ballColor.getCodeState() == 1)
        {
            StartCoroutine(DestroyObject(0.2f));
        }

        //  Loop
        if (ballColor.getCodeState() == 2)
        {
            StartCoroutine(LoopSound(1f, ballColor.getLoopState() + 2));
        }
        //  Increase Pitch + transform width!!
        if (ballColor.getCodeState() == 3)
        {
            // ++
            if (ballColor.getPitchState() == 0)
            {
                if (pitchLevel < 4)
                {
                    pitchLevel++;
                    this.gameObject.transform.localScale += new Vector3(0, 0.2f, 0);
                    this.gameObject.transform.GetChild(0).localScale -= new Vector3(0, 0.1f, 0);
                }
            }

            // --
            else if (ballColor.getPitchState() == 1)
            {
                if (pitchLevel > 0)
                {
                    pitchLevel--;
                    this.gameObject.transform.localScale -= new Vector3(0, 0.2f, 0);
                    this.gameObject.transform.GetChild(0).localScale += new Vector3(0, 0.1f, 0);
                }
            }
            //Debug.Log(codeInfo.getLineState());
            playClip.PlayOneShot(soundMan.GetAudio(ballColor.getLineState(), pitchLevel));

        }

        //  Change Color
        if (ballColor.getCodeState() == 4)
        {
            // switch gameobject -> destroy + instantiate


            //Destroy(this.gameObject);
            GameObject newColor = Instantiate(lineArray.GetObject(ballColor.getColorState()),
                this.gameObject.transform.position, this.gameObject.transform.rotation);
            StartCoroutine(ChangeColor(1f, this.gameObject, newColor, ballColor));
            //MakeSound();
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    //public AudioClip
}
