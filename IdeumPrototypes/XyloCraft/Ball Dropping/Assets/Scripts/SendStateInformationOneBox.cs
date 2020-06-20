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
            Chord.gameObject.SetActive(true);
            Rhythym.gameObject.SetActive(false);
        }
        else if (Mode.value == 0) 
        {
            Chord.gameObject.SetActive(false);
            Rhythym.gameObject.SetActive(true);
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
