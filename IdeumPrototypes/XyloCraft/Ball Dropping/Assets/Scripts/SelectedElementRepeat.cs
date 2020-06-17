using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedElementRepeat : MonoBehaviour
{
    public string currentRepeatMode;
    public Image grey;
    public Toggle NoneToggle;
    public Toggle OnceToggle;
    public ToggleRepeat None;
    public ToggleRepeat Once;
    public ToggleRepeat Repeat;
    public Text ifStatement;
    // Start is called before the first frame update
    void Start()
    {
        currentRepeatMode = "None";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetModeToNone()
    {
        currentRepeatMode = "None";
        grey.enabled = true;
        ifStatement.text = "   (";
    }

    public void SetNoneToggle() 
    {
        NoneToggle.isOn = true;
        OnceToggle.isOn = false;
    }

    public void SetModeToOnce()
    {
        if (currentRepeatMode != "Once")
        {
            currentRepeatMode = "Once";
            grey.enabled = false;
            ifStatement.text = "If (";
        }
        else 
        {
            SetModeToNone();
        }
    }

    public void SetModeToRepeat()
    {
        if (currentRepeatMode != "Repeat")
        {
            currentRepeatMode = "Repeat";
            grey.enabled = false;
            ifStatement.text = "While (";
        }
        else 
        {
            SetModeToNone();
        }
    }

    public string GetCurrentRhythym()
    {
        return currentRepeatMode;
    }
}
