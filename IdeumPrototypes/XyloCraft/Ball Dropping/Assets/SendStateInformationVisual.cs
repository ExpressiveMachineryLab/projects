using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SendStateInformationVisual : MonoBehaviour
{
    public SelectedElement SelectedBall;
    public SelectedElement SelectedLine;
    public SelectedElementVisual Visual;
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

    public string GetSelectedVisual()
    {
        return Visual.GetCurrentVisual();
    }

    public void SetSelectedVisual(string visual)
    {
        Visual.SetCurrentVisual(visual);
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


    public void FlashBox()
    {
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        flashBorder.color -= new Color(0, 0, 0, 0.5f);


        yield return new WaitForSeconds(0.3f);
        flashBorder.color += new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.1f);
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
            RepeatState.Repeat.toggled();
        }
    }
}
