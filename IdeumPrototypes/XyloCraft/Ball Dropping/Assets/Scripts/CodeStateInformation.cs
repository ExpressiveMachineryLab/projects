using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CodeStateInformation : MonoBehaviour
{
    public Dropdown codeStateDropdown;
    public Dropdown ballStateDropdown;
    public Dropdown lineStateDropdown;

    private int codeState;
    private int ballState;
    private int lineState;

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
    }

    public void ChangeBallState(int newBallState)
    {
        ballState = newBallState;
    }

    public void ChangeLineState(int newLineState)
    {
        lineState = newLineState;
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
}
