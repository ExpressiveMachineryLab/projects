using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SendStateInformationOneBox : MonoBehaviour
{
    public SelectedElement SelectedBall;
    public SelectedElement SelectedLine;
    public SelectedElementChord Chord;
    public SelectedElementRhythym Rhythym;
    public Dropdown Mode;

    public SelectedElementRepeat RepeatState;

    public GameObject ChordOptions;
    public GameObject RhythymOptions;
    public GameObject VisualOptions;
    public GameObject ModeOptions;
    public GameObject SpeedOptions;

    //public Image flashBorder;
    private Color thisColor;

    // Start is called before the first frame update
    void Start()
    {
        thisColor = this.gameObject.GetComponent<Image>().color;
    }

    public string GetBallColor()
    {
        return SelectedBall.GetCurrentColor();
    }
    public string GetLineColor()
    {
        return SelectedLine.GetCurrentColor();
    }

    public void UpdatePanel() 
    {
        if (Mode.value == 0)
        {
            ChordOptions.gameObject.SetActive(true);
            RhythymOptions.gameObject.SetActive(false);
            VisualOptions.gameObject.SetActive(false);
            ModeOptions.gameObject.SetActive(false);
            SpeedOptions.gameObject.SetActive(false);
        }
        else if (Mode.value == 1)
        {
            ChordOptions.gameObject.SetActive(false);
            RhythymOptions.gameObject.SetActive(true);
            VisualOptions.gameObject.SetActive(false);
            ModeOptions.gameObject.SetActive(false);
            SpeedOptions.gameObject.SetActive(false);
        }
        else if (Mode.value == 2)
        {
            ChordOptions.gameObject.SetActive(false);
            RhythymOptions.gameObject.SetActive(false);
            VisualOptions.gameObject.SetActive(true);
            ModeOptions.gameObject.SetActive(false);
            SpeedOptions.gameObject.SetActive(false);
        }
        else if (Mode.value == 3)
        {
            ChordOptions.gameObject.SetActive(false);
            RhythymOptions.gameObject.SetActive(false);
            VisualOptions.gameObject.SetActive(false);
            ModeOptions.gameObject.SetActive(true);
            SpeedOptions.gameObject.SetActive(false);
        }
        else if (Mode.value == 4) 
        {
            ChordOptions.gameObject.SetActive(false);
            RhythymOptions.gameObject.SetActive(false);
            VisualOptions.gameObject.SetActive(false);
            ModeOptions.gameObject.SetActive(false);
            SpeedOptions.gameObject.SetActive(true);
        }
    }

    public string GetSelectedChord()
    {
        return Chord.GetCurrentChord();
    }
    public int GetSelectedRhythym()
    {
        return Rhythym.GetCurrentRhythym();
    }

    public string GetRepeatState()
    {
        return RepeatState.GetCurrentRhythym();
    }
    public void SetRepeatStateNone()
    {
        RepeatState.SetModeToNone();
        RepeatState.SetNoneToggle();
    }
}
