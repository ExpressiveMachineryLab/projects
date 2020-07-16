﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SendStateInformationOneBox : MonoBehaviour
{
    public SelectedElement SelectedBall;
    public SelectedElement SelectedLine;
    public SelectedElementChord Chord;
    public GameObject ChordPlusButton;
    public GameObject ChordMinusButton;
    public SelectedElementRhythym Rhythym;
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public SelectedElementVisual Visual;
    public GameObject VisualPlusButton;
    public GameObject VisualMinusButton;

    public Text textSummary;
    public Text ifStatement;
    public ToggleSelected textToggle;

    public int AssignedNumber;

    public Dropdown Mode;

    public SelectedElementRepeat RepeatState;

    public GameObject ChordOptions;
    public GameObject RhythymOptions;
    public GameObject VisualOptions;
    public GameObject ModeOptions;
    public GameObject SpeedOptions;

    public GameObject Conditionals;

    //public Image flashBorder;
    private Color thisColor;

    private GameManager GA; 
    private string LastSelectedCodeButton;

    // Start is called before the first frame update
    void Start()
    {
        thisColor = this.gameObject.GetComponent<Image>().color;
        GA = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public string GetBallColor()
    {
        return SelectedBall.GetCurrentColor();
    }
    public string GetLineColor()
    {
        return SelectedLine.GetCurrentColor();
    }

    public void SetBallColor(string color)
    {
        if (color == "Blue")
        {
            SelectedBall.SetBlue();
        }
        else if (color == "Red")
        {
            SelectedBall.SetRed();
        }
        else if (color == "Green")
        {
            SelectedBall.SetGreen();
        }
        else if (color == "Yellow")
        {
            SelectedBall.SetYellow();
        }
    }

    public void SetLineColor(string color)
    {
        if (color == "Blue")
        {
            SelectedLine.SetBlue();
        }
        else if (color == "Red")
        {
            SelectedLine.SetRed();
        }
        else if (color == "Green")
        {
            SelectedLine.SetGreen();
        }
        else if (color == "Yellow")
        {
            SelectedLine.SetYellow();
        }
    }
    public void SetSelectedChord(string chord)
    {
        
        if (chord == "Plus")
        {
            if (Chord.GetCurrentChord() == "Minus" || Chord.GetCurrentChord() == "PlusMinus")
            {
                ChordMinusButton.GetComponent<ToggleSelected>().toggled();
            } else if (Chord.GetCurrentChord() != "Plus")
            {
                ChordPlusButton.GetComponent<ToggleSelected>().toggled();
            }
        }
        else if (chord == "Minus")
        {
            if (Chord.GetCurrentChord() != "Minus")
            {
                ChordMinusButton.GetComponent<ToggleSelected>().toggled();
            }
            else if (Chord.GetCurrentChord() == "Plus" || Chord.GetCurrentChord() == "PlusMinus")
            {
                ChordPlusButton.GetComponent<ToggleSelected>().toggled();
            }
        }
        else if (chord == "PlusMinus")
        {
            if (Chord.GetCurrentChord() != "PlusMinus")
            {
                ChordPlusButton.GetComponent<ToggleSelected>().toggled();
                ChordMinusButton.GetComponent<ToggleSelected>().toggled();
            }
            else if (Chord.GetCurrentChord() == "Plus")
            {
                ChordMinusButton.GetComponent<ToggleSelected>().toggled();
            }
            else if (Chord.GetCurrentChord() == "Minus")
            {
                ChordPlusButton.GetComponent<ToggleSelected>().toggled();
            }
        }
        else if (chord == "None") 
        {
            if (Chord.GetCurrentChord() == "PlusMinus")
            {
                ChordPlusButton.GetComponent<ToggleSelected>().toggled();
                ChordMinusButton.GetComponent<ToggleSelected>().toggled();
            }
            else if (Chord.GetCurrentChord() == "Plus")
            {
                ChordPlusButton.GetComponent<ToggleSelected>().toggled();
            }
            else if (Chord.GetCurrentChord() == "Minus")
            {
                ChordMinusButton.GetComponent<ToggleSelected>().toggled();
            }
        }
        Chord.SetCurrentChord(chord);
    }

    public void SetSelectedRhythym(int optional) 
    {
        if (optional == 1 && Rhythym.GetCurrentRhythym() != 1)
        {
            one.GetComponent<ToggleSelected>().toggled();
        }
        else if (optional == 2 && Rhythym.GetCurrentRhythym() != 2) 
        { 
            two.GetComponent<ToggleSelected>().toggled(); 
        }
        else if (optional == 3 && Rhythym.GetCurrentRhythym() != 3)
        {
            three.GetComponent<ToggleSelected>().toggled();
        }
        else if (optional == 4 && Rhythym.GetCurrentRhythym() != 4)
        {
            four.GetComponent<ToggleSelected>().toggled();
        }
        else if (optional == 5 && Rhythym.GetCurrentRhythym() != 5)
        {
            five.GetComponent<ToggleSelected>().toggled();
        }

        if (Rhythym.GetCurrentRhythym() == 1)
        {
            one.GetComponent<ToggleSelected>().toggled();
        }
        else if (Rhythym.GetCurrentRhythym() == 2)
        {
            two.GetComponent<ToggleSelected>().toggled();
        }
        else if (Rhythym.GetCurrentRhythym() == 3)
        {
            three.GetComponent<ToggleSelected>().toggled();
        }
        else if (Rhythym.GetCurrentRhythym() == 4)
        {
            four.GetComponent<ToggleSelected>().toggled();
        }
        else if (Rhythym.GetCurrentRhythym() == 5)
        {
            five.GetComponent<ToggleSelected>().toggled();
        }

        Rhythym.SetCurrentRhythym(optional);
    }

    public void SetSelectedVisual(string visual)
    {

        if (visual == "Plus")
        {
            if (Visual.GetCurrentVisual() == "Minus" || Visual.GetCurrentVisual() == "PlusMinus")
            {
                VisualMinusButton.GetComponent<ToggleSelected>().toggled();
            }
            else if (Visual.GetCurrentVisual() != "Plus")
            {
                VisualPlusButton.GetComponent<ToggleSelected>().toggled();
            }
        }
        else if (visual == "Minus")
        {
            if (Visual.GetCurrentVisual() != "Minus")
            {
                VisualMinusButton.GetComponent<ToggleSelected>().toggled();
            }
            else if (Visual.GetCurrentVisual() == "Plus" || Visual.GetCurrentVisual() == "PlusMinus")
            {
                VisualPlusButton.GetComponent<ToggleSelected>().toggled();
            }
        }
        else if (visual == "PlusMinus")
        {
            if (Visual.GetCurrentVisual() != "PlusMinus")
            {
                VisualPlusButton.GetComponent<ToggleSelected>().toggled();
                VisualMinusButton.GetComponent<ToggleSelected>().toggled();
            }
            else if (Visual.GetCurrentVisual() == "Plus")
            {
                VisualMinusButton.GetComponent<ToggleSelected>().toggled();
            }
            else if (Visual.GetCurrentVisual() == "Minus")
            {
                VisualPlusButton.GetComponent<ToggleSelected>().toggled();
            }
        }
        else if (visual == "None")
        {
            if (Visual.GetCurrentVisual() == "PlusMinus")
            {
                VisualPlusButton.GetComponent<ToggleSelected>().toggled();
                VisualMinusButton.GetComponent<ToggleSelected>().toggled();
            }
            else if (Visual.GetCurrentVisual() == "Plus")
            {
                VisualPlusButton.GetComponent<ToggleSelected>().toggled();
            }
            else if (Visual.GetCurrentVisual() == "Minus")
            {
                VisualMinusButton.GetComponent<ToggleSelected>().toggled();
            }
        }
        Visual.SetCurrentVisual(visual);
    }

    public void SetRepeatState(string repeat)
    {
        if (repeat == "None")
        {
            RepeatState.SetModeToNone();
        }
        else if (repeat == "Once")
        {
            RepeatState.SetModeToOnce();
        }
        else if (repeat == "Repeat")
        {
            RepeatState.SetModeToRepeat();
        }
    }

    public void SetNumber(int number) 
    {
        ifStatement.text = number + ". (";
    }

    public void UpdatePanel() 
    {
        if (Mode.value == 0)
        {
            Conditionals.SetActive(true);
            ChordOptions.gameObject.SetActive(true);
            RhythymOptions.gameObject.SetActive(false);
            VisualOptions.gameObject.SetActive(false);
            ModeOptions.gameObject.SetActive(false);
            SpeedOptions.gameObject.SetActive(false);
            if (!GA.OneBox) 
            {
                this.gameObject.name = "ChordPanel" + AssignedNumber;
                GA.IncreaseChordCount();
            }
        }
        else if (Mode.value == 1)
        {
            Conditionals.SetActive(true);
            ChordOptions.gameObject.SetActive(false);
            RhythymOptions.gameObject.SetActive(true);
            VisualOptions.gameObject.SetActive(false);
            ModeOptions.gameObject.SetActive(false);
            SpeedOptions.gameObject.SetActive(false);
            if (!GA.OneBox)
            {
                this.gameObject.name = "RhythymPanel" + AssignedNumber;
                GA.IncreaseRhythymCount();
            }
        }
        else if (Mode.value == 2)
        {
            Conditionals.SetActive(true);
            ChordOptions.gameObject.SetActive(false);
            RhythymOptions.gameObject.SetActive(false);
            VisualOptions.gameObject.SetActive(true);
            ModeOptions.gameObject.SetActive(false);
            SpeedOptions.gameObject.SetActive(false);
            if (!GA.OneBox)
            {
                this.gameObject.name = "EffectsPanel" + AssignedNumber;
                GA.IncreaseEffectCount();
            }
        }
        else if (Mode.value == 3)
        {
            Conditionals.SetActive(false);
            ChordOptions.gameObject.SetActive(false);
            RhythymOptions.gameObject.SetActive(false);
            VisualOptions.gameObject.SetActive(false);
            ModeOptions.gameObject.SetActive(true);
            SpeedOptions.gameObject.SetActive(false);
        }
        else if (Mode.value == 4)
        {
            Conditionals.SetActive(false);
            ChordOptions.gameObject.SetActive(false);
            RhythymOptions.gameObject.SetActive(false);
            VisualOptions.gameObject.SetActive(false);
            ModeOptions.gameObject.SetActive(false);
            SpeedOptions.gameObject.SetActive(true);
        }
    }

    public void UpdateDropdown(int newValue) 
    {
        Mode.value = newValue;
        UpdatePanel();
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
        Debug.Log(RepeatState.GetCurrentRhythym());
        return RepeatState.GetCurrentRhythym();
    }
    public void SetRepeatStateNone()
    {
        RepeatState.SetModeToNone();
        RepeatState.SetNoneToggle();
    }

    public void UpdateText()
    {
        if (Mode.value == 0)
        {
            if (RepeatState.GetCurrentRhythym() == "Once")
            {
                textSummary.text = "If (" + GetBallColor() + " ball hits " + GetLineColor() + " line) {Chord " + GetSelectedChord() + " }";
            }
            else if (RepeatState.GetCurrentRhythym() == "Repeat")
            {
                textSummary.text = "While True: if(" + GetBallColor() + " ball hits " + GetLineColor() + " line) {Chord " + GetSelectedChord() + " }";
            }
            else 
            {
                textSummary.text = "(" + GetBallColor() + " ball hits " + GetLineColor() + " line) {Chord " + GetSelectedChord() + " }";
            }


        }
        else if (Mode.value == 1)
        {
            if (RepeatState.GetCurrentRhythym() == "Once")
            {
                textSummary.text = "If (" + GetBallColor() + " ball hits " + GetLineColor() + " line) {Repeat " + GetSelectedRhythym() + " }";
            }
            else if (RepeatState.GetCurrentRhythym() == "Repeat")
            {
                textSummary.text = "While True: if(" + GetBallColor() + " ball hits " + GetLineColor() + " line) {Repeat " + GetSelectedRhythym() + " }";
            }
            else
            {
                textSummary.text = "(" + GetBallColor() + " ball hits " + GetLineColor() + " line) {Repeat " + GetSelectedRhythym() + " }";
            }

            
        }
        else if (Mode.value == 2) 
        {
            if (RepeatState.GetCurrentRhythym() == "Once")
            {
                textSummary.text = "If (" + GetBallColor() + " ball hits " + GetLineColor() + " line) {Play Effect}";
            }
            else if (RepeatState.GetCurrentRhythym() == "Repeat")
            {
                textSummary.text = "While True: if(" + GetBallColor() + " ball hits " + GetLineColor() + " line) {Play Effect}";
            }
            else
            {
                textSummary.text = "(" + GetBallColor() + " ball hits " + GetLineColor() + " line) {Play Effect}";
            }
        }
    }

    public void ToggleText() 
    {
        textToggle.toggled();
        UpdateText();
        textSummary.enabled = !textSummary.enabled;
    }

    public void RandomElements() 
    {
        Mode.value = (int)(Random.value * 3);
        if (Mode.value == 0)
        {
            string[] chordOptions = { "Plus", "Minus", "PlusMinus" };
            SetSelectedChord(chordOptions[(int)(Random.value * 3)]);
        }
        else if (Mode.value == 1) 
        {
            SetSelectedRhythym((int)(Random.value * 5));
        }

        string[] repeatOptions = { "None", "Once", "Repeat" };
        SetRepeatState(repeatOptions[(int)(Random.value * 3)]);
    }
}
