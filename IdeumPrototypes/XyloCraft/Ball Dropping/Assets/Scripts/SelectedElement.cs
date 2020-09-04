using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedElement : MonoBehaviour
{
	public SelectedElementType type;
	public ElemColor color;
	public string currentColor;

	public Sprite Red;
	public Sprite Yellow;
	public Sprite Blue;
	public Sprite Green;
	public Sprite Wild;
	public Sprite None;

	private void Start()
	{
		if (color == ElemColor.Red)
		{
			SetRed();
		}
		else if (color == ElemColor.Yellow)
		{
			SetYellow();
		}
		else if (color == ElemColor.Blue)
		{
			SetBlue();
		}
		else if (color == ElemColor.Green)
		{
			SetGreen();
		}
		else if (color == ElemColor.All)
		{
			SetWild();
		}
		else if (color == ElemColor.None)
		{
			SetNone();
		}
	}

	public string GetCurrentColor() 
    {
        return currentColor;
    }

    public void SetRed()
    {
        this.gameObject.GetComponent<Image>().sprite = Red;
        currentColor = Red.name;
		color = ElemColor.Red;
	}

	public void SetYellow()
	{
		this.gameObject.GetComponent<Image>().sprite = Yellow;
		currentColor = Yellow.name;
		color = ElemColor.Yellow;
	}

	public void SetBlue()
	{
		this.gameObject.GetComponent<Image>().sprite = Blue;
		currentColor = Blue.name;
		color = ElemColor.Blue;
	}

	public void SetGreen()
    {
        this.gameObject.GetComponent<Image>().sprite = Green;
        currentColor = Green.name;
		color = ElemColor.Green;
	}

	public void SetWild()
	{
		this.gameObject.GetComponent<Image>().sprite = Wild;
		currentColor = "All";
		color = ElemColor.All;
	}

	public void SetNone()
	{
		this.gameObject.GetComponent<Image>().sprite = None;
		currentColor = "None";
		color = ElemColor.None;
	}

	public void SetNext()
	{
		if (color == ElemColor.Red)
		{
			SetYellow();
		}
		else if (color == ElemColor.Yellow)
		{
			SetBlue();
		}
		else if (color == ElemColor.Blue)
		{
			SetGreen();
		}
		else if (color == ElemColor.Green)
		{
			SetWild();
		}
		else if (color == ElemColor.All)
		{
			SetRed();
		}
	}

}

public enum SelectedElementType
{
	Ball,
	Line,
	Emitter
}

public enum ElemColor
{
	Red,
	Yellow,
	Blue,
	Green,
	All,
	None
}