using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SendStateInformation : MonoBehaviour
{
    public Dropdown localCodeStateDropdown;
    public Dropdown localBallStateDropdown;
    public Dropdown localLineStateDropdown;

    public CodeStateInformation codeInfo;

    // Start is called before the first frame update
    void Start()
    {
        localCodeStateDropdown.onValueChanged.AddListener(delegate {
            CodeStateHandler(localCodeStateDropdown);
        });
    }

    // Update is called once per frame
    void Update()
    {
        // if different, changes local info
        if (localCodeStateDropdown.value != codeInfo.getCodeState())
        {
            localCodeStateDropdown.value = codeInfo.getCodeState();
            //codeInfo.ChangeCodeState(localCodeStateDropdown.value);
        }
        if (localBallStateDropdown.value != codeInfo.getBallState())
        {
            codeInfo.ChangeBallState(localBallStateDropdown.value);
        }
        if (localLineStateDropdown.value != codeInfo.getLineState())
        {
            codeInfo.ChangeLineState(localLineStateDropdown.value);
        }
    }

    // if change, sets info hub
    private void CodeStateHandler(Dropdown localCodeStateDropdown)
    {
        Debug.Log("update info");
        codeInfo.ChangeCodeState(localCodeStateDropdown.value);
    }
}
