using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedElementChord : MonoBehaviour
{
    public string currentChord;

    public void SetChordToPlus() 
    {
        if (currentChord == "None")
        {
            currentChord = "Plus";
        }
        else { SetChordToNone(); }
        
    }

    public void SetChordToMinus() 
    {
        if (currentChord == "None")
        {
            currentChord = "Minus";
        }
        else { SetChordToNone(); }
        
    }

    public void SetChordToPlusMinus() 
    {
        if (currentChord == "None")
        {
            currentChord = "PlusMinus";
        }
        else { SetChordToNone(); }
        
    }

    public void SetChordToNone() 
    {
        currentChord = "None";
    }

    public string GetCurrentChord() 
    {
        return currentChord;
    }
}
