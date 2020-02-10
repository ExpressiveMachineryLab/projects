using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LineCorners : MonoBehaviour
{
    public Sprite originalSprite;
    public Sprite hitSprite;

    private CodeStateInformationCorners codeInfo;
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
        codeInfo = GameObject.Find("GameManager").GetComponent<CodeStateInformationCorners>();
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

    private IEnumerator ChangeColor(float seconds, GameObject oldColor, GameObject newColor)
    {
        //playClip.PlayOneShot(audioClip);
        //MakeSound();

        //Debug.Log(newColor.GetComponent<Line>());
        //newColor.GetComponent<Line>().MakeSound();
        //(Line) newColor.MakeSound();
        newColor.transform.GetChild(0).gameObject.SetActive(false);
        //Debug.Log(soundMan.GetAudio(codeInfo.getColorState(), pitchLevel));
        playClip.PlayOneShot(soundMan.GetAudio(codeInfo.getColorState(), pitchLevel));


        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);

        //playClip.pitch = 1.0f;
    }

    private void PerformCodeBehvaior(Collision2D collision)
    {
        //  Destroy
        if (codeInfo.getDestroyActive())
        {
            //blue ball 
            if (collision.gameObject.tag == "Ball0"
            && this.gameObject.tag == "Line" + codeInfo.getLineDestroyState())
            {
                StartCoroutine(DestroyObject(0.2f));
            }
            
        }

        //  Loop
        if (codeInfo.getLoopActive())
        {
            // red ball
            if (collision.gameObject.tag == "Ball2"
            && this.gameObject.tag == "Line" + codeInfo.getLineLoopState())
            {
                StartCoroutine(LoopSound(1f, codeInfo.getLoopState() + 2));
            }
        }
        //  Increase Pitch + transform width!!
        if (codeInfo.getPitchActive())
        {
            if (collision.gameObject.tag == "Ball1"
            && this.gameObject.tag == "Line" + codeInfo.getLinePitchState())
            {
                // ++
                if (codeInfo.getPitchState() == 0)
                {
                    if (pitchLevel < 4)
                    {
                        pitchLevel++;
                        this.gameObject.transform.localScale += new Vector3(0, 0.2f, 0);
                        this.gameObject.transform.GetChild(0).localScale -= new Vector3(0, 0.1f, 0);
                    }
                }

                // --
                else if (codeInfo.getPitchState() == 1)
                {
                    if (pitchLevel > 0)
                    {
                        pitchLevel--;
                        this.gameObject.transform.localScale -= new Vector3(0, 0.2f, 0);
                        this.gameObject.transform.GetChild(0).localScale += new Vector3(0, 0.1f, 0);
                    }
                }
                //Debug.Log(codeInfo.getLineState());
                playClip.PlayOneShot(soundMan.GetAudio(codeInfo.getLinePitchState(), pitchLevel));
            }
        }

        //  Change Color
        if (codeInfo.getColorActive())
        {
            if (collision.gameObject.tag == "Ball3"
               && this.gameObject.tag == "Line" + codeInfo.getLineColorState())
            {
                GameObject newColor = Instantiate(lineArray.GetObject(codeInfo.getColorState()),
                this.gameObject.transform.position, this.gameObject.transform.rotation);
                StartCoroutine(ChangeColor(1f, this.gameObject, newColor));
                //MakeSound();
            }
        }
        // None
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

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    //public AudioClip
}
