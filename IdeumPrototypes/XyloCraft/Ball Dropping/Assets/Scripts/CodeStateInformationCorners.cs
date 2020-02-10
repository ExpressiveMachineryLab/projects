using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CodeStateInformationCorners : MonoBehaviour
{
    // Whether corner is active
    private bool destroyActive;
    private bool loopActive;
    private bool pitchActive;
    private bool colorActive;

    // Dropdown value for line
    private int lineDestroyState;
    private int lineLoopState;
    private int linePitchState;
    private int lineColorState;

    // Extra dropdown information
    private int loopState;
    private int pitchState;
    private int colorState;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Update dropdown values
    public void ChangeLineStateDestroy(int newLineState)
    {
        lineDestroyState = newLineState;
    }

    public void ChangeLoopState(int newLoopState)
    {
        loopState = newLoopState;
    }

    public void ChangePitchState(int newPitchState)
    {
        pitchState = newPitchState;
    }

    public void ChangeColorState(int newColorState)
    {
        colorState = newColorState;
    }

    // Set Active values
    public void setDestroyActive(bool value)
    {
        destroyActive = value;
    }

    public void setLoopActive(bool value)
    {
        loopActive = value;
    }

    public void setPitchActive(bool value)
    {
        pitchActive = value;
    }

    public void setColorActive(bool value)
    {
        colorActive = value;
    }


    // Get Active values
    public bool getDestroyActive()
    {
        return destroyActive;
    }

    public bool getLoopActive()
    {
        return loopActive;
    }

    public bool getPitchActive()
    {
        return pitchActive;
    }

    public bool getColorActive()
    {
        return colorActive;
    }

    // Get line values for each corner
    public int getLineDestroyState()
    {
        return lineDestroyState;
    }

    public int getLineLoopState()
    {
        return lineLoopState;
    }

    public int getLinePitchState()
    {
        return linePitchState;
    }

    public int getLineColorState()
    {
        return lineColorState;
    }

    // Get extra dropdown values
    public int getLoopState()
    {
        return loopState;
    }

    public int getPitchState()
    {
        return pitchState;
    }

    public int getColorState()
    {
        return colorState;
    }


    public void setAllFalse()
    {
        destroyActive = false;
        loopActive = false;
        pitchActive = false;
        colorActive = false;
}

    private void PrintLog(string log)
    {
        Debug.Log(log);
    }

    //private void Backlog(int codeState, int ballState, int lineState, 
    //    int loopState, int pitchState,int colorState)
    //{

    //}
}
