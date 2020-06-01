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

    public Animator effects;

    private int InstrumentPanelCount = 3;
    private int ChordPanelCount = 2;
    private int RhythymPanelCount = 2;
    private int EffectPanelCount = 2;

    //private ArrayList InstrumentPanels = new ArrayList();
    private ArrayList ChordPanels = new ArrayList();
    private ArrayList RhythymPanels = new ArrayList();
    private ArrayList EffectPanels = new ArrayList();

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

        //for (int i = 1; i <= InstrumentPanelCount; i++) 
        //{
        //    InstrumentPanels.Add(GameObject.Find("InstrumentPanel" + i).GetComponent<SendStateInformation>());
        //}

        for (int i = 1; i <= ChordPanelCount; i++)
        {
            ChordPanels.Add(GameObject.Find("ChordPanel" + i).GetComponent<SendStateInformationChord>());
        }

        for (int i = 1; i <= RhythymPanelCount; i++)
        {
            RhythymPanels.Add(GameObject.Find("RhythymPanel" + i).GetComponent<SendStateInformationRhythym>());
        }

        for (int i = 1; i <= EffectPanelCount; i++)
        {
            EffectPanels.Add(GameObject.Find("EffectsPanel" + i).GetComponent<SendStateInformation>());
        }

        //InstrumentPanel1 = GameObject.Find("InstrumentPanel1").GetComponent<SendStateInformation>();
        //ChordPanel1 = GameObject.Find("ChordPanel1").GetComponent<SendStateInformationChord>();
        //RhythymPanel1 = GameObject.Find("RhythymPanel1").GetComponent<SendStateInformationRhythym>();

        //InstrumentPanel2 = GameObject.Find("InstrumentPanel2").GetComponent<SendStateInformation>();
        //ChordPanel2 = GameObject.Find("ChordPanel2").GetComponent<SendStateInformationChord>();
        //RhythymPanel2 = GameObject.Find("RhythymPanel2").GetComponent<SendStateInformationRhythym>();

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
        //this.GetComponentInChildren<ParticleSystem>().Play();
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
        //foreach (SendStateInformation panel in InstrumentPanels) 
        //{
        //    if ((panel.GetBallColor() == "All" && this.gameObject.tag == panel.GetLineColor() + "Line") ||
        //    (panel.GetLineColor() == "All" && collision.gameObject.tag == panel.GetBallColor() + "Ball") ||
        //    collision.gameObject.tag == panel.GetBallColor() + "Ball"
        //    && this.gameObject.tag == panel.GetLineColor() + "Line"
        //    && panel.GetRepeatState() != "None")
        //    {
        //        this.lineColor = panel.GetChangeLineColor();
        //        this.gameObject.tag = lineColor + "Line";
        //        this.originalSprite = lineArray.GetSprite(lineColor);
        //        this.hitSprite = lineArray.GetHitSprite(lineColor);
        //        MakeSound();
        //        if (panel.GetRepeatState() == "Once")
        //        {
        //            panel.SetRepeatStateNone();
        //        }
        //    }
        //    else
        //    {
        //        MakeSound();
        //    }
        //}

        // Chord + Note Panel
        foreach (SendStateInformationChord panel in ChordPanels)
        {
            if ((panel.GetBallColor() == "All" && this.gameObject.tag == panel.GetLineColor() + "Line") ||
            (panel.GetLineColor() == "All" && collision.gameObject.tag == panel.GetBallColor() + "Ball") ||
            collision.gameObject.tag ==panel.GetBallColor() + "Ball"
            && this.gameObject.tag == panel.GetLineColor() + "Line"
            && panel.GetRepeatState() != "None")
            {
                // ++
                if (panel.GetSelectedChord() == "Plus")
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
                else if (panel.GetSelectedChord() == "Minus")
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
                else if (panel.GetSelectedChord() == "PlusMinus")
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
                soundMan.GetAudio(playClip, panel.GetLineColor(), pitchLevel, gameManger.GetSoundState());
                if (panel.GetRepeatState() == "Once")
                {
                    panel.SetRepeatStateNone();
                }
            }
            else
            {
                MakeSound();
            }
        }

        // Rhythym Panel
        foreach (SendStateInformationRhythym panel in RhythymPanels)
        {
            if ((panel.GetBallColor() == "All" && this.gameObject.tag == panel.GetLineColor() + "Line") ||
            (panel.GetLineColor() == "All" && collision.gameObject.tag == panel.GetBallColor() + "Ball") ||
            collision.gameObject.tag == panel.GetBallColor() + "Ball"
            && this.gameObject.tag == panel.GetLineColor() + "Line"
            && panel.GetRepeatState() != "None")
            {
                StartCoroutine(LoopSound(0.2f, panel.GetSelectedRhythym()));
                if (panel.GetRepeatState() == "Once")
                {
                    panel.SetRepeatStateNone();
                }
            }
            else
            {
                MakeSound();
            }
        }

        //  Effects Panel
        foreach (SendStateInformation panel in EffectPanels)
        {
            if ((panel.GetBallColor() == "All" && this.gameObject.tag == panel.GetLineColor() + "Line") ||
            (panel.GetLineColor() == "All" && collision.gameObject.tag == panel.GetBallColor() + "Ball") ||
            collision.gameObject.tag == panel.GetBallColor() + "Ball"
            && this.gameObject.tag == panel.GetLineColor() + "Line"
            && panel.GetRepeatState() != "None")
            {
                effects.SetTrigger("Play");
                //this.lineColor = panel.GetChangeLineColor();
                //this.gameObject.tag = lineColor + "Line";
                //this.originalSprite = lineArray.GetSprite(lineColor);
                //this.hitSprite = lineArray.GetHitSprite(lineColor);
                
                MakeSound();
                if (panel.GetRepeatState() == "Once")
                {
                    panel.SetRepeatStateNone();
                }
            }
            else
            {
                MakeSound();
            }
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
