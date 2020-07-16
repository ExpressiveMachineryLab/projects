using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedElementVisual : MonoBehaviour
{
    public string currentVisual;

    public void SetVisualToPlus()
    {
        if (currentVisual == "None")
        {
            currentVisual = "Plus";
        }
        else if (currentVisual == "Minus")
        {
            SetVisualToPlusMinus();
        }
        else if (currentVisual == "PlusMinus")
        {
            currentVisual = "Minus";
        }
        else { SetVisualToNone(); }
    }

    public void SetVisualToMinus()
    {
        if (currentVisual == "None")
        {
            currentVisual = "Minus";
        }
        else if (currentVisual == "Plus")
        {
            SetVisualToPlusMinus();
        }
        else if (currentVisual == "PlusMinus")
        {
            currentVisual = "Plus";
        }
        else { SetVisualToNone(); }
    }

    public void SetVisualToPlusMinus()
    {
        currentVisual = "PlusMinus";
    }

    public void SetVisualToNone()
    {
        currentVisual = "None";
    }

    public string GetCurrentVisual()
    {
        return currentVisual;
    }

    public void SetCurrentVisual(string Visual)
    {
        currentVisual = Visual;
    }
}
