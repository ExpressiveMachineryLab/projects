using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Line3 : MonoBehaviour
{
    public string lineColor;

    public Sprite[] chordSprites; //should always be of length 5
    public Sprite[] chordHitSprites;

    public float speed = 5f;

    public Animator effects;

    public SpriteRenderer lineSprite;

    private int ChordPanelCount;
    private int RhythymPanelCount;
    private int EffectPanelCount;
    private int ActionsPanelCount;

    private ArrayList ChordPanels = new ArrayList();
    private ArrayList RhythymPanels = new ArrayList();
    private ArrayList EffectPanels = new ArrayList();
    private ArrayList ActionsPanels = new ArrayList();

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
        soundMan = GameObject.Find("GameManager").GetComponent<SoundManager>();
        lineArray = GameObject.Find("GameManager").GetComponent<LineInformation>();
        gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();

        ChordPanelCount = 4;
        RhythymPanelCount = 4;
        EffectPanelCount = 4;
        ActionsPanelCount = 4;

        //if (gameManger.OneBox)
        //{
        //    ChordPanelCount = 0;
        //    RhythymPanelCount = 0;
        //    EffectPanelCount = 0;
        //    ActionsPanelCount = 0;
        //}
        //else
        //{
        //    ChordPanelCount = 4;
        //    RhythymPanelCount = 4;
        //    EffectPanelCount = 4;
        //    ActionsPanelCount = 4;
        //}
        for (int i = 1; i <= ChordPanelCount; i++)
        {
            if (GameObject.Find("ChordPanel" + i) != null)
            {
                ChordPanels.Add(GameObject.Find("ChordPanel" + i).GetComponent<SendStateInformationChord>());
            }

        }

        for (int i = 1; i <= RhythymPanelCount; i++)
        {
            if (GameObject.Find("RhythymPanel" + i) != null)
            {
                RhythymPanels.Add(GameObject.Find("RhythymPanel" + i).GetComponent<SendStateInformationRhythym>());
            }

        }

        for (int i = 1; i <= EffectPanelCount; i++)
        {
            if (GameObject.Find("EffectsPanel" + i) != null)
            {
                EffectPanels.Add(GameObject.Find("EffectsPanel" + i).GetComponent<SendStateInformation>());
            }

        }

        for (int i = 1; i <= ActionsPanelCount; i++)
        {
            if (GameObject.Find("ActionsPanel" + i) != null)
            {
                ActionsPanels.Add(GameObject.Find("ActionsPanel" + i).GetComponent<SendStateInformationActions>());
            }

        }
    }

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

        //check and update panel count
        if (gameManger.OneBox)
        {
            ChordPanelCount = gameManger.GetChordCount();
            RhythymPanelCount = gameManger.GetRhythymCount();
            EffectPanelCount = gameManger.GetEffectCount();
        }
        if (gameManger.OneBox || gameManger.FourBox)
        {
            ChordPanels = new ArrayList();
            RhythymPanels = new ArrayList();
            EffectPanels = new ArrayList();

            for (int i = 1; i <= ChordPanelCount; i++)
            {

                if (GameObject.Find("ChordPanel" + i) != null)
                {
                    ChordPanels.Add(GameObject.Find("ChordPanel" + i).GetComponent<SendStateInformationChord>());
                }

            }

            for (int i = 1; i <= RhythymPanelCount; i++)
            {
                if (GameObject.Find("RhythymPanel" + i) != null)
                {
                    RhythymPanels.Add(GameObject.Find("RhythymPanel" + i).GetComponent<SendStateInformationRhythym>());
                }

            }

            for (int i = 1; i <= EffectPanelCount; i++)
            {
                Debug.Log("Iteration");
                if (GameObject.Find("EffectsPanel" + i) != null)
                {
                    EffectPanels.Add(GameObject.Find("EffectsPanel" + i).GetComponent<SendStateInformation>());
                    Debug.Log("Found");
                }

            }
        }

        PerformCodeBehvaior(collision);

    }

    private IEnumerator ChangeSprite(float seconds, Collision2D collision)
    {

        Sprite ballOriginalObject = collision.gameObject.GetComponent<Ball>().originalSprite;
        Sprite ballHitObject = collision.gameObject.GetComponent<Ball>().hitSprite;
        SpriteRenderer collidedObject = collision.gameObject.GetComponent<SpriteRenderer>();
       // SpriteRenderer thisLineObject = this.gameObject.GetComponent<SpriteRenderer>();

        collidedObject.sprite = ballHitObject;


        lineSprite.sprite = chordHitSprites[pitchLevel];

        yield return new WaitForSeconds(seconds);
        if (collidedObject != null)
        {
            collidedObject.sprite = ballOriginalObject;
        }

        lineSprite.sprite = chordSprites[pitchLevel];

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

        // Chord + Note Panel
        foreach (SendStateInformationChord panel in ChordPanels)
        {

            if (panel != null)
            {

                Debug.Log("hello");
                if (((panel.GetBallColor() == "All" && this.gameObject.tag == panel.GetLineColor() + "Line") ||
                (panel.GetLineColor() == "All" && collision.gameObject.tag == panel.GetBallColor() + "Ball") ||
                (collision.gameObject.tag == panel.GetBallColor() + "Ball"
                && this.gameObject.tag == panel.GetLineColor() + "Line"))
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
                    //SpriteRenderer thisLineObject = this.gameObject.GetComponent<SpriteRenderer>();
                    //thisLineObject.sprite = chordSprites[pitchLevel];


                    soundMan.GetAudio(playClip, panel.GetLineColor(), pitchLevel, gameManger.GetSoundState());
                    if (panel.GetRepeatState() == "Once")
                    {
                        panel.SetRepeatStateNone();
                    }
                }
                else
                {

                }
            }

        }

        // Rhythym Panel
        foreach (SendStateInformationRhythym panel in RhythymPanels)
        {
            if (panel != null)
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
                   // MakeSound();
                }
            }

        }

        //  Effects Panel
        foreach (SendStateInformation panel in EffectPanels)
        {
            Debug.Log(panel);
            Debug.Log("you");
            Debug.Log(panel.GetRepeatState());
            if (((panel.GetBallColor() == "All" && this.gameObject.tag == panel.GetLineColor() + "Line") ||
                (panel.GetLineColor() == "All" && collision.gameObject.tag == panel.GetBallColor() + "Ball") ||
                (collision.gameObject.tag == panel.GetBallColor() + "Ball"
                && this.gameObject.tag == panel.GetLineColor() + "Line"))
                && panel.GetRepeatState() != "None")
            {
                Debug.Log("there");
                effects.SetTrigger("Play");
                Debug.Log(panel.GetBallColor() + " " + panel.GetLineColor());
              //  MakeSound();
                if (panel.GetRepeatState() == "Once")
                {
                    panel.SetRepeatStateNone();
                }
            }
            else
            {
              //  MakeSound();
            }

        }

        //  Actions Panel
        foreach (SendStateInformationActions panel in ActionsPanels)
        {
            if ((panel.GetBallColor() == "All" && this.gameObject.tag == panel.GetLineColor() + "Line") ||
                (panel.GetLineColor() == "All" && collision.gameObject.tag == panel.GetBallColor() + "Ball") ||
                collision.gameObject.tag == panel.GetBallColor() + "Ball"
                && this.gameObject.tag == panel.GetLineColor() + "Line"
                && panel.GetRepeatState() != "None")
            {
                if (gameManger.ThreeBox)
                {
                    this.lineColor = panel.GetChangeLineInstrumentColor();
                    this.gameObject.tag = lineColor + "Line";
                  //  this.chordSprites = new Sprite[] { lineArray.GetSprite(lineColor) };
                 //   this.chordHitSprites = new Sprite[]lineArray.GetHitSprite(lineColor);
                 //   MakeSound();
                }
                else
                {
                    if (panel.GetDropdownState() == "Instruments")
                    {
                        this.lineColor = panel.GetChangeLineInstrumentColor();
                        this.gameObject.tag = lineColor + "Line";
                       // this.originalSprite = lineArray.GetSprite(lineColor);
                       // this.hitSprite = lineArray.GetHitSprite(lineColor);
                      //  MakeSound();

                    }
                    else if (panel.GetDropdownState() == "Volume")
                    {
                        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, panel.GetVolumeState());
                        this.GetComponent<AudioSource>().volume = panel.GetVolumeState();
                      //  MakeSound();
                    }
                    else if (panel.GetDropdownState() == "Destroy")
                    {
                        StartCoroutine(DestroyObject(0.2f));
                    }

                    if (panel.GetRepeatState() == "Once")
                    {
                        panel.SetRepeatStateNone();
                    }

                }

            }
            else
            {
               // MakeSound();
            }
        }


        StartCoroutine(ChangeSprite(0.15f, collision));
        MakeSound();

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
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}
