using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SendStateInformation : MonoBehaviour
{
    public Dropdown localCodeStateDropdown;
    public Dropdown localBallStateDropdown;
    public Dropdown localLineStateDropdown;
    public Dropdown localCodeStateExtraDropdown;

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

        localCodeStateExtraDropdown.onValueChanged.AddListener(delegate {
            CodeStateExtraHandler(localCodeStateExtraDropdown);
        });
    }

    // Update is called once per frame
    void Update()
    {
        // if different, changes local info
        if (localCodeStateDropdown.value != codeInfo.getCodeState())
        {
            localCodeStateDropdown.value = codeInfo.getCodeState();

            if (codeInfo.getCodeState() > 1)
            {
                localCodeStateExtraDropdown.gameObject.SetActive(true);
            }
            else
            {
                localCodeStateExtraDropdown.gameObject.SetActive(false);
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
        if (localCodeStateExtraDropdown.value != codeInfo.getCodeExtraState())
        {
            localCodeStateExtraDropdown.value = codeInfo.getCodeExtraState();
        }
    }

    // if change, sets info hub
    private void CodeStateHandler(Dropdown localCodeStateDropdown)
    {
        codeInfo.ChangeCodeState(localCodeStateDropdown.value);

        if (codeInfo.getCodeState() > 1)
        {
            localCodeStateExtraDropdown.gameObject.SetActive(true);
        }
        else
        {
            localCodeStateExtraDropdown.gameObject.SetActive(false);
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

    private void CodeStateExtraHandler(Dropdown localCodeStateExtraDropdown)
    {
        codeInfo.ChangeCodeExtraState(localCodeStateExtraDropdown.value);
    }
}
