using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SendStateInformationCorner : MonoBehaviour
{
    public Dropdown localCodeStateDropdown;
    public Dropdown localBallStateDropdown;
    public Dropdown localLineStateDropdown;
    public Dropdown localLoopDropdown;
    public Dropdown localPitchDropdown;
    public Dropdown localColorDropdown;

    public CodeStateInformation codeInfo;

    // Start is called before the first frame update
    void Start()
    {
        localCodeStateDropdown.onValueChanged.AddListener(delegate {
            CodeStateHandler(localCodeStateDropdown);
        });

        localBallStateDropdown.onValueChanged.AddListener(delegate {
            BallStateHandler(localBallStateDropdown);
        });

        localLineStateDropdown.onValueChanged.AddListener(delegate {
            LineStateHandler(localLineStateDropdown);
        });

        localLoopDropdown.onValueChanged.AddListener(delegate {
            LoopStateHandler(localLoopDropdown);
        });

        localPitchDropdown.onValueChanged.AddListener(delegate {
            PitchStateHandler(localPitchDropdown);
        });

        localColorDropdown.onValueChanged.AddListener(delegate {
            ColorStateHandler(localColorDropdown);
        });
    }

    // Update is called once per frame
    void Update()
    {
        // if different, changes local info
        if (localCodeStateDropdown.value != codeInfo.getCodeState())
        {
            localCodeStateDropdown.value = codeInfo.getCodeState();

            if (codeInfo.getCodeState() == 2)
            {
                localLoopDropdown.gameObject.SetActive(true);
                localPitchDropdown.gameObject.SetActive(false);
                localColorDropdown.gameObject.SetActive(false);
            }
            else if (codeInfo.getCodeState() == 3)
            {
                localLoopDropdown.gameObject.SetActive(false);
                localPitchDropdown.gameObject.SetActive(true);
                localColorDropdown.gameObject.SetActive(false);
            }
            else if (codeInfo.getCodeState() == 4)
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
        if (localBallStateDropdown.value != codeInfo.getBallState())
        {
            localBallStateDropdown.value = codeInfo.getBallState();
        }
        if (localLineStateDropdown.value != codeInfo.getLineState())
        {
            localLineStateDropdown.value = codeInfo.getLineState();
        }

        if (localLoopDropdown.value != codeInfo.getLoopState())
        {
            localLoopDropdown.value = codeInfo.getLoopState();
        }
        if (localPitchDropdown.value != codeInfo.getPitchState())
        {
            localPitchDropdown.value = codeInfo.getPitchState();
        }
        if (localColorDropdown.value != codeInfo.getColorState())
        {
            localColorDropdown.value = codeInfo.getColorState();
        }
    }

    // if change, sets info hub
    private void CodeStateHandler(Dropdown localCodeStateDropdown)
    {
        codeInfo.ChangeCodeState(localCodeStateDropdown.value);

        if (codeInfo.getCodeState() == 2)
        {
            localLoopDropdown.gameObject.SetActive(true);
            localPitchDropdown.gameObject.SetActive(false);
            localColorDropdown.gameObject.SetActive(false);
        }
        else if (codeInfo.getCodeState() == 3)
        {
            localLoopDropdown.gameObject.SetActive(false);
            localPitchDropdown.gameObject.SetActive(true);
            localColorDropdown.gameObject.SetActive(false);
        }
        else if (codeInfo.getCodeState() == 4)
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

    private void BallStateHandler(Dropdown localBallStateDropdown)
    {
        codeInfo.ChangeBallState(localBallStateDropdown.value);
    }

    private void LineStateHandler(Dropdown localLineStateDropdown)
    {
        codeInfo.ChangeLineState(localLineStateDropdown.value);
    }

    private void LoopStateHandler(Dropdown localLoopStateDropdown)
    {
        codeInfo.ChangeLoopState(localLoopStateDropdown.value);
    }

    private void PitchStateHandler(Dropdown localPitchStateDropdown)
    {
        codeInfo.ChangePitchState(localPitchStateDropdown.value);
    }

    private void ColorStateHandler(Dropdown localColorStateDropdown)
    {
        codeInfo.ChangeColorState(localColorStateDropdown.value);
    }
}
