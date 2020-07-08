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
        if (GameObject.Find("GameManager").GetComponent<GameManager>().OneBox)
        {
            currentRepeatMode = "Repeat";

        }
        else 
        {
            SetModeToNone();
        }
        
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
        if (Once.isToggled)
        {
            Once.toggled();
        }
        if (Repeat.isToggled)
        {
            Repeat.toggled();
        }
        if (!None.isToggled)
        {
            None.toggled();
        }
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
            //  ifStatement.text = "If (";
            if (None.isToggled)
            {
                None.toggled();
            }
            if (Repeat.isToggled)
            {
                Repeat.toggled();
            }
            if (!Once.isToggled)
            {
                Once.toggled();
            }
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
            // ifStatement.text = "While (";
            if (Once.isToggled)
            {
                Once.toggled();
            }
            if (None.isToggled)
            {
                None.toggled();
            }
            if (!Repeat.isToggled)
            {
                Repeat.toggled();
            }
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
