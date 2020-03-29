﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Line3 : MonoBehaviour
{
    public int lineColor;
    public Sprite originalSprite;
    public Sprite hitSprite;
    public float speed = 5f;

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
    private bool isBeingRotated = false;

    private int pitchLevel = 1;

    BoxCollider2D thisCollider;
    AudioSource playClip;

    public AudioClip collisionSound;
    
    void Start()
    {
        thisCollider = GetComponent<BoxCollider2D>();
        playClip = GetComponent<AudioSource>();
        blueBall = GameObject.Find("CodePanelBlue").GetComponent<SendStateInformation3>();
        greenBall = GameObject.Find("CodePanelGreen").GetComponent<SendStateInformation3>();
        redBall = GameObject.Find("CodePanelRed").GetComponent<SendStateInformation3>();
        yellowBall = GameObject.Find("CodePanelYellow").GetComponent<SendStateInformation3>();
        soundMan = GameObject.Find("GameManager").GetComponent<SoundManager>();
        lineArray = GameObject.Find("GameManager").GetComponent<LineArray>();

        this.GetComponent<AudioSource>().volume = soundMan.GetVolume(lineColor);
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

        if (isBeingRotated)
        {
            Rotate();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            Debug.Log(gameObject.transform.GetChild(1).GetComponent<Collider2D>());
            if (hit.collider != null && hit.collider.tag == "Rotator" && hit.collider == gameObject.transform.GetChild(1).GetComponent<Collider2D>())
            {
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
    
    private IEnumerator ChangeSprite(float seconds, Collision2D collision)
    {
        Sprite ballOriginalObject = collision.gameObject.GetComponent<Ball>().originalSprite;
        Sprite ballHitObject = collision.gameObject.GetComponent<Ball>().hitSprite;
        SpriteRenderer collidedObject = collision.gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer thisLineObject = this.gameObject.GetComponent<SpriteRenderer>();

        collidedObject.sprite = ballHitObject;
        thisLineObject.sprite = hitSprite;
        yield return new WaitForSeconds(seconds);
        if (collidedObject != null)
        {
            collidedObject.sprite = ballOriginalObject;
        }
        
        thisLineObject.sprite = originalSprite;
    }

    public void MakeSound()
    {
        playClip.PlayOneShot(soundMan.GetAudio(lineColor, pitchLevel - 1));
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
    
    private void PerformCodeBehvaior(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        Debug.Log(this.gameObject.tag);
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

    private void PerformCode(SendStateInformation3 ballColor)
    {
        // None
        if (ballColor.GetCodeState() == 0)
        {
            MakeSound();
        }

        // Destroy
        if (ballColor.GetCodeState() == 4)
        {
            StartCoroutine(DestroyObject(0.2f));
        }

        //  Loop
        if (ballColor.GetCodeState() == 5)
        {
            StartCoroutine(LoopSound(0.2f, ballColor.GetLoopState() + 2));
        }
        //  Increase Pitch + transform width!!
        if (ballColor.GetCodeState() == 1)
        {
            Debug.Log("Change Pitch");
            if (ballColor.GetPitchState() < 5)
            {
                pitchLevel = ballColor.GetPitchState() + 1;
            }

            // ++
            if (ballColor.GetPitchState() == 5)
            {
                if (pitchLevel < 5)
                {
                    pitchLevel++;
                }
            }

            // --
            else if (ballColor.GetPitchState() == 6)
            {
                if (pitchLevel > 1)
                {
                    pitchLevel--;
                }
            }
            this.TransformLine(pitchLevel);
            playClip.PlayOneShot(soundMan.GetAudio(ballColor.GetLineState(), pitchLevel - 1));

        }

        //  Change Color
        if (ballColor.GetCodeState() == 3)
        {
            this.lineColor = ballColor.GetColorState();
            this.gameObject.tag = "Line" + lineColor;
            this.originalSprite = lineArray.GetSprite(lineColor);
            this.hitSprite = lineArray.GetHitSprite(lineColor);
            MakeSound();
        }

        // Change Volume
        if (ballColor.GetCodeState() == 2)
        {
            this.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, ballColor.GetVolumeState());
            this.GetComponent<AudioSource>().volume = ballColor.GetVolumeState();
            MakeSound();
        }
    }

    private void TransformLine(int pitch)
    {
        // 1
        if (pitchLevel == 1)
        {
            this.gameObject.transform.localScale = new Vector3(0.1f, 0.15f, 0);
            this.gameObject.transform.GetChild(0).localScale = new Vector3(7.5f, 0.5f, 0);
        }

        // 2
        if (pitchLevel == 2)
        {
            this.gameObject.transform.localScale = new Vector3(0.1f, 0.35f, 0);
            this.gameObject.transform.GetChild(0).localScale = new Vector3(7.5f, 0.35f, 0);
        }

        // 3
        if (pitchLevel == 3)
        {
            this.gameObject.transform.localScale = new Vector3(0.1f, 0.55f, 0);
            this.gameObject.transform.GetChild(0).localScale = new Vector3(8f, 0.2f, 0);
        }

        // 4
        if (pitchLevel == 4)
        {
            this.gameObject.transform.localScale = new Vector3(0.1f, 0.75f, 0);
            this.gameObject.transform.GetChild(0).localScale = new Vector3(8f, 0.2f, 0);
        }

        // 5
        if (pitchLevel == 5)
        {
            this.gameObject.transform.localScale = new Vector3(0.1f, 0.95f, 0);
            this.gameObject.transform.GetChild(0).localScale = new Vector3(8.5f, 0.2f, 0);
        }
    }

    public int GetPitchLevel()
    {
        return pitchLevel;
    }

    public void SetPitchLevel(int newPitch)
    {
        pitchLevel = newPitch;
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    void Rotate()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //rotation *= Quaternion.Euler(0, 0, -90);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}
