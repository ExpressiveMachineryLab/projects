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

	public Image secondImage;

	private Image image;

	private void Start()
	{
		image = gameObject.GetComponent<Image>();
		UpdateImage();
	}

	public string GetCurrentColor() 
    {
        return currentColor;
    }

    public void SetRed()
    {
        image.sprite = Red;
		if (secondImage != null) secondImage.sprite = Red;
		currentColor = Red.name;
		color = ElemColor.Red;
	}

	public void SetYellow()
	{
		image.sprite = Yellow;
		if (secondImage != null) secondImage.sprite = Yellow;
		currentColor = Yellow.name;
		color = ElemColor.Yellow;
	}

	public void SetBlue()
	{
		image.sprite = Blue;
		if (secondImage != null) secondImage.sprite = Blue;
		currentColor = Blue.name;
		color = ElemColor.Blue;
	}

	public void SetGreen()
    {
		image.sprite = Green;
		if (secondImage != null) secondImage.sprite = Green;
		currentColor = Green.name;
		color = ElemColor.Green;
	}

	public void SetWild()
	{
		image.sprite = Wild;
		if (secondImage != null) secondImage.sprite = Wild;
		currentColor = "All";
		color = ElemColor.All;
	}

	public void SetNone()
	{
		image.sprite = None;
		if (secondImage != null) secondImage.sprite = None;
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

	public void UpdateImage()
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