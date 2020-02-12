using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CodeStateInformation3 : MonoBehaviour
{
    public Dropdown codeStateDropdown;
    public Dropdown ballStateDropdown;
    public Dropdown lineStateDropdown;
    //public Dropdown codeStateExtraDropdown;

    private int codeState;
    private int ballState;
    private int lineState;
    private int loopState;
    private int pitchState;
    private int colorState;

    // Start is called before the first frame update
    void Start()
    {
        codeState = codeStateDropdown.value;

        ballState = ballStateDropdown.value;

        lineState = lineStateDropdown.value;

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeCodeState(int newCodeState)
    {
        codeState = newCodeState;
        Debug.Log("Changed 'if' ball color to: ");
        //Backlog(codeState, ballState, lineState, );
    }

    public void ChangeBallState(int newBallState)
    {
        ballState = newBallState;
    }

    public void ChangeLineState(int newLineState)
    {
        lineState = newLineState;
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

    public int getCodeState()
    {
        return codeState;
    }

    public int getBallState()
    {
        return ballState;
    }

    public int getLineState()
    {
        return lineState;
    }

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

    private void PrintLog(string log)
    {
        Debug.Log(log);
    }

    //private void Backlog(int codeState, int ballState, int lineState, 
    //    int loopState, int pitchState,int colorState)
    //{

    //}
}
