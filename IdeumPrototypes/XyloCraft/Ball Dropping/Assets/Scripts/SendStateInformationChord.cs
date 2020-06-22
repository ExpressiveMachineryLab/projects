using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SendStateInformationChord : MonoBehaviour
{
    public SelectedElement SelectedBall;
    public SelectedElement SelectedLine;
    public SelectedElementChord Chord;
    public SelectedElementRepeat RepeatState;

    public Image flashBorder;
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

    public string GetLineColor()
    {
        return SelectedLine.GetCurrentColor();
    }

    public string GetSelectedChord()
    {
        return Chord.GetCurrentChord();
    }

    public void SetSelectedChord(string chord) 
    {
        Chord.SetCurrentChord(chord);
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


    public void FlashBox(int color)
    {
        StartCoroutine(Flash(color));
    }

    private IEnumerator Flash(int color)
    {
        flashBorder.color += new Color(0, 0, 0, 0.5f);
        if (color == 0)
        {
            this.gameObject.GetComponent<Image>().color = new Color(0, 0, 1, 0.1f);
        }
        if (color == 1)
        {
            this.gameObject.GetComponent<Image>().color = new Color(0, 1, 0, 0.3f);
        }
        if (color == 2)
        {
            //this.gameObject.GetComponent<Image>().color = new Color(1, 0, 0, 0.3f);
        }
        if (color == 3)
        {
            this.gameObject.GetComponent<Image>().color = new Color(1, 0.92f, 0.016f, 0.3f);
        }

        yield return new WaitForSeconds(0.3f);
        flashBorder.color -= new Color(0, 0, 0, 0.5f);
        this.gameObject.GetComponent<Image>().color = thisColor;
        yield return new WaitForSeconds(0.1f);
    }
}
