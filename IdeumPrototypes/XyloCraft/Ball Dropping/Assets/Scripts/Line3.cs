using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Line3 : MonoBehaviour
{
    public string lineColor;
    public Sprite originalSprite;
    public Sprite hitSprite;
    public float speed = 5f;

    //private CodeStateInformation codeInfo;
    private SendStateInformation InstrumentPanel1;
    private SendStateInformationChord ChordPanel1;
    private SendStateInformationRhythym RhythymPanel1;
    private SendStateInformation InstrumentPanel2;
    private SendStateInformationChord ChordPanel2;
    private SendStateInformationRhythym RhythymPanel2;

    private SoundManager soundMan;
    private LineInformation lineArray;

    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;
    private bool isBeingRotated = false;

    private int pitchLevel = 0;
    private bool pitchPositive = true;

    BoxCollider2D thisCollider;
    AudioSource playClip;

    private GameManager gameManger;

    public AudioClip collisionSound;
    
    void Start()
    {
        thisCollider = GetComponent<BoxCollider2D>();
        playClip = GetComponent<AudioSource>();

        InstrumentPanel1 = GameObject.Find("InstrumentPanel1").GetComponent<SendStateInformation>();
        ChordPanel1 = GameObject.Find("ChordPanel1").GetComponent<SendStateInformationChord>();
        RhythymPanel1 = GameObject.Find("RhythymPanel1").GetComponent<SendStateInformationRhythym>();

        InstrumentPanel2 = GameObject.Find("InstrumentPanel2").GetComponent<SendStateInformation>();
        ChordPanel2 = GameObject.Find("ChordPanel2").GetComponent<SendStateInformationChord>();
        RhythymPanel2 = GameObject.Find("RhythymPanel2").GetComponent<SendStateInformationRhythym>();

        soundMan = GameObject.Find("GameManager").GetComponent<SoundManager>();
        lineArray = GameObject.Find("GameManager").GetComponent<LineInformation>();
        gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();

        //this.GetComponent<AudioSource>().volume = soundMan.GetVolume(lineColor);
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
        soundMan.GetAudio(playClip, this.gameObject.tag, pitchLevel, gameManger.GetSoundState());
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
    
    private void PerformCodeBehvaior(Collision2D collision)
    {
        //  Instrument Panel
        if ((InstrumentPanel1.GetBallColor() == "All" && this.gameObject.tag == InstrumentPanel1.GetLineColor() + "Line" ) ||
            (InstrumentPanel1.GetLineColor() == "All" && collision.gameObject.tag == InstrumentPanel1.GetBallColor() + "Ball" ) ||
            collision.gameObject.tag == InstrumentPanel1.GetBallColor() +"Ball" 
            && this.gameObject.tag == InstrumentPanel1.GetLineColor() + "Line" 
            && InstrumentPanel1.GetRepeatState() != "None")
        {
            this.lineColor = InstrumentPanel1.GetChangeLineColor();
            this.gameObject.tag = lineColor + "Line";
            this.originalSprite = lineArray.GetSprite(lineColor);
            this.hitSprite = lineArray.GetHitSprite(lineColor);
            MakeSound();
            if (InstrumentPanel1.GetRepeatState() == "Once") 
            {
                InstrumentPanel1.SetRepeatStateNone();
            }
        }

        else if ((InstrumentPanel2.GetBallColor() == "All" && this.gameObject.tag == InstrumentPanel2.GetLineColor() + "Line") ||
            (InstrumentPanel2.GetLineColor() == "All" && collision.gameObject.tag == InstrumentPanel2.GetBallColor() + "Ball") ||
            collision.gameObject.tag == InstrumentPanel2.GetBallColor() + "Ball"
            && this.gameObject.tag == InstrumentPanel2.GetLineColor() + "Line" 
            && InstrumentPanel2.GetRepeatState() != "None")
        {
            this.lineColor = InstrumentPanel2.GetChangeLineColor();
            this.gameObject.tag = lineColor + "Line";
            this.originalSprite = lineArray.GetSprite(lineColor);
            this.hitSprite = lineArray.GetHitSprite(lineColor);
            MakeSound();
            if (InstrumentPanel2.GetRepeatState() == "Once")
            {
                InstrumentPanel2.SetRepeatStateNone();
            }
        }

        // Chord + Note Panel
        if ((ChordPanel1.GetBallColor() == "All" && this.gameObject.tag == ChordPanel1.GetLineColor() + "Line") ||
            (ChordPanel1.GetLineColor() == "All" && collision.gameObject.tag == ChordPanel1.GetBallColor() + "Ball") ||
            collision.gameObject.tag == ChordPanel1.GetBallColor() + "Ball"
            && this.gameObject.tag == ChordPanel1.GetLineColor() + "Line"
            && ChordPanel1.GetRepeatState() != "None")
        {
            // ++
            if (ChordPanel1.GetSelectedChord() == "Plus") 
            {
                if (pitchLevel < 4)
                {
                    pitchLevel++;
                }
                else 
                {
                    pitchLevel = 0;
                }
            }
            else if (ChordPanel1.GetSelectedChord() == "Minus")
            {
                if (pitchLevel > 0)
                {
                    pitchLevel--;
                }
                else 
                {
                    pitchLevel = 4;
                }
            }
            else if (ChordPanel1.GetSelectedChord() == "PlusMinus")
            {
                if (pitchLevel == 4) 
                {
                    pitchPositive = false;
                } else if (pitchLevel == 0)
                {
                    pitchPositive = true;
                }

                if (pitchPositive)
                {
                    pitchLevel++;
                }
                else {
                    pitchLevel--;
                }
            }
            soundMan.GetAudio(playClip, ChordPanel1.GetLineColor(), pitchLevel, gameManger.GetSoundState());
            if (ChordPanel1.GetRepeatState() == "Once")
            {
                ChordPanel1.SetRepeatStateNone();
            }
        }

        if ((ChordPanel2.GetBallColor() == "All" && this.gameObject.tag == ChordPanel2.GetLineColor() + "Line") ||
            (ChordPanel2.GetLineColor() == "All" && collision.gameObject.tag == ChordPanel2.GetBallColor() + "Ball") || 
            collision.gameObject.tag == ChordPanel2.GetBallColor() + "Ball"
            && this.gameObject.tag == ChordPanel2.GetLineColor() + "Line"
            && ChordPanel2.GetRepeatState() != "None")
        {
            // ++
            if (ChordPanel2.GetSelectedChord() == "Plus")
            {
                if (pitchLevel < 4)
                {
                    pitchLevel++;
                }
                else
                {
                    pitchLevel = 0;
                }
            }
            else if (ChordPanel2.GetSelectedChord() == "Minus")
            {
                if (pitchLevel > 0)
                {
                    pitchLevel--;
                }
                else
                {
                    pitchLevel = 4;
                }
            }
            else if (ChordPanel2.GetSelectedChord() == "PlusMinus")
            {
                if (pitchLevel == 4)
                {
                    pitchPositive = false;
                }
                else if (pitchLevel == 0)
                {
                    pitchPositive = true;
                }

                if (pitchPositive)
                {
                    pitchLevel++;
                }
                else
                {
                    pitchLevel--;
                }
            }
            soundMan.GetAudio(playClip, ChordPanel2.GetLineColor(), pitchLevel, gameManger.GetSoundState());

            if (ChordPanel2.GetRepeatState() == "Once")
            {
                ChordPanel2.SetRepeatStateNone();
            }
        }

        // Rhythym Panel
        if ((RhythymPanel1.GetBallColor() == "All" && this.gameObject.tag == RhythymPanel1.GetLineColor() + "Line") ||
            (RhythymPanel1.GetLineColor() == "All" && collision.gameObject.tag == RhythymPanel1.GetBallColor() + "Ball") || 
            collision.gameObject.tag == RhythymPanel1.GetBallColor() + "Ball"
            && this.gameObject.tag == RhythymPanel1.GetLineColor() + "Line"
            && RhythymPanel1.GetRepeatState() != "None")
        {
            StartCoroutine(LoopSound(0.2f, RhythymPanel1.GetSelectedRhythym()));
            if (RhythymPanel1.GetRepeatState() == "Once")
            {
                RhythymPanel1.SetRepeatStateNone();
            }
        }


        if ((RhythymPanel2.GetBallColor() == "All" && this.gameObject.tag == RhythymPanel2.GetLineColor() + "Line") ||
            (RhythymPanel2.GetLineColor() == "All" && collision.gameObject.tag == RhythymPanel2.GetBallColor() + "Ball") || 
            collision.gameObject.tag == RhythymPanel2.GetBallColor() + "Ball"
            && this.gameObject.tag == RhythymPanel2.GetLineColor() + "Line"
            && RhythymPanel2.GetRepeatState() != "None")
        {
            StartCoroutine(LoopSound(0.2f, RhythymPanel2.GetSelectedRhythym()));
            if (RhythymPanel2.GetRepeatState() == "Once")
            {
                RhythymPanel2.SetRepeatStateNone();
            }
        }

        else
        {
            MakeSound(); 
        }
    }

    //private void PerformCode(SendStateInformation3 ballColor)
    //{
    //    // None
    //    if (ballColor.GetCodeState() == 0)
    //    {
    //        MakeSound();
    //    }

    //    // Destroy
    //    if (ballColor.GetCodeState() == 4)
    //    {
    //        StartCoroutine(DestroyObject(0.2f));
    //    }

    //    //  Loop
    //    if (ballColor.GetCodeState() == 5)
    //    {
    //        StartCoroutine(LoopSound(0.2f, ballColor.GetLoopState() + 2));
    //    }
    //    //  Increase Pitch + transform width
    //    if (ballColor.GetCodeState() == 1)
    //    {
    //        Debug.Log("Change Pitch");
    //        if (ballColor.GetPitchState() < 5)
    //        {
    //            pitchLevel = ballColor.GetPitchState() + 1;
    //        }

    //        // ++
    //        if (ballColor.GetPitchState() == 5)
    //        {
    //            if (pitchLevel < 5)
    //            {
    //                pitchLevel++;
    //            }
    //        }

    //        // --
    //        else if (ballColor.GetPitchState() == 6)
    //        {
    //            if (pitchLevel > 1)
    //            {
    //                pitchLevel--;
    //            }
    //        }
    //        this.TransformLine(pitchLevel);
    //        playClip.PlayOneShot(soundMan.GetAudio(ChordPanel1.GetLineColor(), (pitchLevel - 1) + gameManger.GetSoundState()));

    //    }

    //    //  Change Color
    //    if (ballColor.GetCodeState() == 3)
    //    {
    //        //this.lineColor = ballColor.GetColorState();
    //        this.gameObject.tag = "Line" + lineColor;
    //        //this.originalSprite = lineArray.GetSprite(lineColor);
    //        //this.hitSprite = lineArray.GetHitSprite(lineColor);
    //        MakeSound();
    //    }

    //    // Change Volume
    //    if (ballColor.GetCodeState() == 2)
    //    {
    //        this.GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, ballColor.GetVolumeState());
    //        this.GetComponent<AudioSource>().volume = ballColor.GetVolumeState();
    //        MakeSound();
    //    }
    //}

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
