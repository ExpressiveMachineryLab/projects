using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineInformation : MonoBehaviour
{
    public GameObject BlueLine;
    public GameObject GreenLine;
    public GameObject RedLine;
    public GameObject YellowLine;

    public Sprite BlueLineSprite;
    public Sprite GreenLineSprite;
    public Sprite RedLineSprite;
    public Sprite YellowLineSprite;

    public Sprite BlueLineHitSprite;
    public Sprite GreenLineHitSprite;
    public Sprite RedLineHitSprite;
    public Sprite YellowLineHitSprite;

    public GameObject GetObject(string Color)
    {
        if (Color == "Blue")
        {
            return BlueLine;
        }
        else if (Color == "Green")
        {
            return GreenLine;
        }
        else if (Color == "Red")
        {
            return RedLine;
        }
        else if (Color == "Yellow")
        {
            return YellowLine;
        }
        return null;
    }
    public Sprite GetSprite(string Color)
    {
        if (Color == "Blue")
        {
            return BlueLineSprite;
        }
        else if (Color == "Green")
        {
            return GreenLineSprite;
        }
        else if (Color == "Red")
        {
            return RedLineSprite;
        }
        else if (Color == "Yellow")
        {
            return YellowLineSprite;
        }
        return null;
    }

    public Sprite GetHitSprite(string Color)
    {
        if (Color == "Blue")
        {
            return BlueLineHitSprite;
        }
        else if (Color == "Green")
        {
            return GreenLineHitSprite;
        }
        else if (Color == "Red")
        {
            return RedLineHitSprite;
        }
        else if (Color == "Yellow")
        {
            return YellowLineHitSprite;
        }
        return null;
    }

}
