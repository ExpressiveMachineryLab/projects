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

    // Start is called before the first frame update
    void Start()
    { 
        localCodeStateDropdown.onValueChanged.AddListener(delegate {
            CodeStateHandler(localCodeStateDropdown);
        });

        localLineStateDropdown.onValueChanged.AddListener(delegate
        {
            LineStateHandler(localLineStateDropdown);
        });
    }

    // if change, sets info hub
    private void CodeStateHandler(Dropdown localCodeStateDropdown)
    {
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

    public int GetBallNumber()
    {
        return ballNumber;
    }

    public int GetCodeState()
    {
        return localCodeStateDropdown.value;
    }

    public int GetLineState()
    {
        return localLineStateDropdown.value;
    }

    public int GetLoopState()
    {
        return localLoopDropdown.value;
    }

    public int GetPitchState()
    {
        return localPitchDropdown.value;
    }

    public int GetColorState()
    {
        return localColorDropdown.value;
    }

    private void LineStateHandler(Dropdown localLineStateDropdown)
    {
        localCodeStateDropdown.value = 0;
    }
}
