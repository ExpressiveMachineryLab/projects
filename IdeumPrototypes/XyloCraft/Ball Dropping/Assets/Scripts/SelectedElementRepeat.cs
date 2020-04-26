using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedElementRepeat : MonoBehaviour
{
    public string currentRepeatMode;
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
    }

    public void SetNoneToggle() 
    {
        Once.toggled();
        None.toggled();
    }

    public void SetModeToOnce()
    {
        currentRepeatMode = "Once";
    }

    public void SetModeToRepeat()
    {
        currentRepeatMode = "Repeat";
    }

    public string GetCurrentRhythym()
    {
        return currentRepeatMode;
    }
}
