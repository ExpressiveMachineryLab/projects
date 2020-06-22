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
        else if (currentChord == "Minus")
        {
            SetChordToPlusMinus();
        }
        else if (currentChord == "PlusMinus") 
        {
            currentChord = "Minus";
        }
        else { SetChordToNone(); }
    }

    public void SetChordToMinus() 
    {
        if (currentChord == "None")
        {
            currentChord = "Minus";
        }
        else if (currentChord == "Plus") 
        {
            SetChordToPlusMinus();
        }
        else if (currentChord == "PlusMinus")
        {
            currentChord = "Plus";
        }
        else { SetChordToNone(); }
    }

    public void SetChordToPlusMinus() 
    {
        currentChord = "PlusMinus";
    }

    public void SetChordToNone() 
    {
        currentChord = "None";
    }

    public string GetCurrentChord() 
    {
        return currentChord;
    }

    public void SetCurrentChord(string chord) 
    {
        currentChord = chord;
    }
}
