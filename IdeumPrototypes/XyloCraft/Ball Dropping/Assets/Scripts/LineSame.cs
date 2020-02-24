using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LineSame : MonoBehaviour
{
    public int lineColor;

    public Sprite originalSprite;
    public Sprite hitSprite;

    private SendStateInformationSame blueBall;
    private SendStateInformationSame greenBall;
    private SendStateInformationSame redBall;
    private SendStateInformationSame yellowBall;
    private SoundManagerSame soundMan;
    private LineArray lineArray;

    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;
    private float grid;
   
    BoxCollider2D thisCollider;
    AudioSource playClip;

    public AudioClip collisionSound;
    
    void Start()
    {
        thisCollider = GetComponent<BoxCollider2D>();
        playClip = GetComponent<AudioSource>();
        //codeInfo = GameObject.Find("GameManager").GetComponent<CodeStateInformation>();
        blueBall = GameObject.Find("CodePanelBlue").GetComponent<SendStateInformationSame>();
        greenBall = GameObject.Find("CodePanelGreen").GetComponent<SendStateInformationSame>();
        redBall = GameObject.Find("CodePanelRed").GetComponent<SendStateInformationSame>();
        yellowBall = GameObject.Find("CodePanelYellow").GetComponent<SendStateInformationSame>();
        soundMan = GameObject.Find("SoundManager").GetComponent<SoundManagerSame>();
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

            this.gameObject.transform.localPosition = new Vector3(mousePos.x, 
                mousePos.y, 0);
        }
    }

    private void OnMouseDown()
    {
        isBeingHeld = true;

        if (Input.GetMouseButtonDown(0))
        {
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
        playClip.PlayOneShot(soundMan.GetAudio(lineColor));
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

    private IEnumerator ChangeColor(float seconds, GameObject oldColor, GameObject newColor, SendStateInformationSame ballColor)
    {
        // selection background to false
        newColor.transform.GetChild(0).gameObject.SetActive(false);
        newColor.GetComponent<AudioSource>().PlayOneShot(soundMan.GetAudio(ballColor.GetBallNumber()));

        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }

    private void PerformCodeBehvaior(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        Debug.Log(this.gameObject.tag);
        Debug.Log(blueBall.GetLineState());
        if (collision.gameObject.tag == "Ball0" && this.gameObject.tag == "Line" + blueBall.GetLineState())
        {
            blueBall.FlashBox(0);
            PerformCode(blueBall);
        }
        else if (collision.gameObject.tag == "Ball1" && this.gameObject.tag == "Line" + greenBall.GetLineState())
        {
            greenBall.FlashBox(1);
            PerformCode(greenBall);
        }
        else if (collision.gameObject.tag == "Ball2" && this.gameObject.tag == "Line" + redBall.GetLineState())
        {
            redBall.FlashBox(2);
            PerformCode(redBall);
        }
        else if (collision.gameObject.tag == "Ball3" && this.gameObject.tag == "Line" + yellowBall.GetLineState())
        {
            yellowBall.FlashBox(3);
            PerformCode(yellowBall);
        }

        else
        {
            MakeSound();
        }
    }

    private void PerformCode(SendStateInformationSame ballColor)
    {
        // None
        if (ballColor.GetCodeState() == 0)
        {
            MakeSound();
        }

        // Destroy
        if (ballColor.GetCodeState() == 1)
        {
            StartCoroutine(DestroyObject(0.2f));
        }

        //  Loop
        if (ballColor.GetCodeState() == 2)
        {
            StartCoroutine(LoopSound(0.2f, ballColor.GetLoopState() + 2));
        }

        //  Change Color
        if (ballColor.GetCodeState() == 3)
        {
            GameObject newColor = Instantiate(lineArray.GetObject(ballColor.GetColorState()),
                this.gameObject.transform.position, this.gameObject.transform.rotation);

            Destroy(this.gameObject);
            newColor.transform.GetChild(0).gameObject.SetActive(false);
            newColor.GetComponent<AudioSource>().PlayOneShot(soundMan.GetAudio(ballColor.GetColorState()));
        }
    }
    
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
