using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedElementRhythym : MonoBehaviour
{
    public int currentRhythym;
 
    public void SetRhythymToOne()
    {
        if (currentRhythym == 0)
        {
            currentRhythym = 1;
        }
        else { SetRhythymToZero(); }
    }

    public void SetRhythymToTwo()
    {
        if (currentRhythym == 0)
        {
            currentRhythym = 2;
        }
        else { SetRhythymToZero(); }
    }

    public void SetRhythymToThree()
    {
        if (currentRhythym == 0)
        {
            currentRhythym = 3;
        }
        else { SetRhythymToZero(); }
    }

    public void SetRhythymToFour()
    {
        if (currentRhythym == 0)
        {
            currentRhythym = 4;
        }
        else { SetRhythymToZero(); }
    }

    public void SetRhythymToFive()
    {
        if (currentRhythym == 0)
        {
            currentRhythym = 5;
        }
        else { SetRhythymToZero(); }
    }

    public void SetRhythymToZero()
    {
        currentRhythym = 0;
    }

    public int GetCurrentRhythym() 
    {
        return currentRhythym;
    }
}
