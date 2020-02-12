using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SendStateInformation3 : MonoBehaviour
{
    public int ballNumber;

    public Dropdown localCodeStateDropdown;
    public Dropdown localLineStateDropdown;
    public Dropdown localLoopDropdown;
    public Dropdown localPitchDropdown;
    public Dropdown localColorDropdown;

    //public CodeStateInformation3 codeInfo;

    // Start is called before the first frame update
    void Start()
    { 
        //codeInfo = GameObject.Find("GameManager").GetComponent<CodeStateInformation3>();

        localCodeStateDropdown.onValueChanged.AddListener(delegate {
            CodeStateHandler(localCodeStateDropdown);
        });

        //localLineStateDropdown.onValueChanged.AddListener(delegate {
        //    LineStateHandler(localLineStateDropdown);
        //});

        //localLoopDropdown.onValueChanged.AddListener(delegate {
        //    LoopStateHandler(localLoopDropdown);
        //});

        //localPitchDropdown.onValueChanged.AddListener(delegate {
        //    PitchStateHandler(localPitchDropdown);
        //});

        //localColorDropdown.onValueChanged.AddListener(delegate {
        //    ColorStateHandler(localColorDropdown);
        //});
    }

    // Update is called once per frame
    void Update()
    {
    }

    // if change, sets info hub
    private void CodeStateHandler(Dropdown localCodeStateDropdown)
    {
        //codeInfo.ChangeCodeState(localCodeStateDropdown.value);

        if (localCodeStateDropdown.value == 2)
        {
            localLoopDropdown.gameObject.SetActive(true);
            localPitchDropdown.gameObject.SetActive(false);
            localColorDropdown.gameObject.SetActive(false);
        }
        else if (localCodeStateDropdown.value == 3)
        {
            localLoopDropdown.gameObject.SetActive(false);
            localPitchDropdown.gameObject.SetActive(true);
            localColorDropdown.gameObject.SetActive(false);
        }
        else if (localCodeStateDropdown.value == 4)
        {
            localLoopDropdown.gameObject.SetActive(false);
            localPitchDropdown.gameObject.SetActive(false);
            localColorDropdown.gameObject.SetActive(true);
        }
        else
        {
            localLoopDropdown.gameObject.SetActive(false);
            localPitchDropdown.gameObject.SetActive(false);
            localColorDropdown.gameObject.SetActive(false);
        }
    }

    public int getBallNumber()
    {
        return ballNumber;
    }

    public int getCodeState()
    {
        return localCodeStateDropdown.value;
    }

    public int getLineState()
    {
        return localLineStateDropdown.value;
    }

    public int getLoopState()
    {
        return localLoopDropdown.value;
    }

    public int getPitchState()
    {
        return localPitchDropdown.value;
    }

    public int getColorState()
    {
        return localColorDropdown.value;
    }

    //private void BallStateHandler(Dropdown localBallStateDropdown)
    //{
    //    codeInfo.ChangeBallState(localBallStateDropdown.value);
    //}

    //private void LineStateHandler(Dropdown localLineStateDropdown)
    //{
    //    codeInfo.ChangeLineState(localLineStateDropdown.value);
    //}

    //private void LoopStateHandler(Dropdown localLoopStateDropdown)
    //{
    //    codeInfo.ChangeLoopState(localLoopStateDropdown.value);
    //}

    //private void PitchStateHandler(Dropdown localPitchStateDropdown)
    //{
    //    codeInfo.ChangePitchState(localPitchStateDropdown.value);
    //}

    //private void ColorStateHandler(Dropdown localColorStateDropdown)
    //{
    //    codeInfo.ChangeColorState(localColorStateDropdown.value);
    //}
}
