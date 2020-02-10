using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SendStateInformationDestroy : MonoBehaviour
{
    public Dropdown localLineStateDropdown;
    public CodeStateInformationCorners codeInfo;

    // Start is called before the first frame update
    void Start()
    {
        localLineStateDropdown.onValueChanged.AddListener(delegate {
            LineStateHandler(localLineStateDropdown);
        });
    }

    // Update is called once per frame
    void Update()
    {
    }

    // if change, sets info hub

    private void LineStateHandler(Dropdown localLineStateDropdown)
    {
        codeInfo.ChangeLineStateDestroy(localLineStateDropdown.value);
        codeInfo.setAllFalse();
        codeInfo.setDestroyActive(true);
    }
}
