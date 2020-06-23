using System.Collections;
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

    
}
