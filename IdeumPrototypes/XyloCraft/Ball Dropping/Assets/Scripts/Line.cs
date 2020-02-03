﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Line : MonoBehaviour
{
    public Sprite originalSprite;
    public Sprite hitSprite;

    private CodeStateInformation codeInfo;
    private SoundManager soundMan;

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
        codeInfo = GameObject.Find("GameManager").GetComponent<CodeStateInformation>();
        soundMan = GameObject.Find("SoundManager").GetComponent<SoundManager>();
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
        Debug.Log(codeInfo.getCodeState());
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
    private IEnumerator ChangePitch(float seconds, AudioClip audioClip)
    {
        playClip.PlayOneShot(audioClip);
        MakeSound();
        yield return new WaitForSeconds(seconds);
        playClip.pitch = 1.0f;
    }

    private void PerformCodeBehvaior(Collision2D collision)
    {
        // None
        if (codeInfo.getCodeState() == 0)
        {
            MakeSound();
        }

        if (CheckCorrectCollision(collision))
        {
            //  Destroy
            if (codeInfo.getCodeState() == 1)
            {
                StartCoroutine(DestroyObject(0.2f));
            }

            //  Loop
            if (codeInfo.getCodeState() == 2)
            {
                Debug.Log(codeInfo.getCodeExtraState());
                StartCoroutine(LoopSound(1f, codeInfo.getCodeExtraState() + 2));
            }
            //  Increase Pitch
            if (codeInfo.getCodeState() == 3)
            {
                playClip.PlayOneShot(soundMan.GetAudio(codeInfo.getLineState(), codeInfo.getCodeExtraState() + 1));
                //StartCoroutine(ChangePitch(0.2f, 
                //    soundMan.GetAudio(codeInfo.getLineState(), codeInfo.getCodeExtraState())));
            }

            //  Decrease Pitch
            if (codeInfo.getCodeState() == 4)
            {
                playClip.PlayOneShot(soundMan.GetAudio(codeInfo.getLineState(), codeInfo.getCodeExtraState() + 1));
                //StartCoroutine(ChangePitch(0.2f,
                //    soundMan.GetAudio(codeInfo.getLineState(), codeInfo.getCodeExtraState())));
            }

            //  Change Color
            if (codeInfo.getCodeState() == 5)
            {
            }
        }
        else
        {
            MakeSound();
        }
    }

    private bool CheckCorrectCollision(Collision2D collision)
    {
        return collision.gameObject.tag == "Ball" + codeInfo.getBallState()
            && this.gameObject.tag == "Line" + codeInfo.getLineState();
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
