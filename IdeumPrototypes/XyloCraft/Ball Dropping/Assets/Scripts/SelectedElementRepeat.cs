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
    }

    public void SetNoneToggle() 
    {
        
        //Once.toggled();
        //None.toggled();
        NoneToggle.isOn = true;
        OnceToggle.isOn = false;
    }

    public void SetModeToOnce()
    {
        currentRepeatMode = "Once";
        grey.enabled = false;
    }

    public void SetModeToRepeat()
    {
        currentRepeatMode = "Repeat";
        grey.enabled = false;
    }

    public string GetCurrentRhythym()
    {
        return currentRepeatMode;
    }
}
