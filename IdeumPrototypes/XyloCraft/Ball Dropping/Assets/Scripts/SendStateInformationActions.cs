using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SendStateInformationActions : MonoBehaviour
{
    public SelectedElement SelectedBall;
    public SelectedElement SelectedLine;
    public SelectedElement ChangeLineInstrument;
    public Slider VolumeSlider;
    public SelectedElement DestroyLine;
    public SelectedElementRepeat RepeatState;

    private string DropdownState = "Instruments";

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
    public string GetLineColor()
    {
        return SelectedLine.GetCurrentColor();
    }

    public void SetDropdownState(string state)
    {
        DropdownState = state;
    }

    public string GetDropdownState() 
    {
        return DropdownState;
    }

    public string GetChangeLineInstrumentColor()
    {
        return ChangeLineInstrument.GetCurrentColor();
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

    public float GetVolumeState()
    {
        return VolumeSlider.value;
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
}
