using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleHUD : MonoBehaviour
{

	public StyleBank[] availableStyles;
	public SoundBank[] availableSounds;
	public GameObject buttonPrefab;
	private ElemColor currentColor = ElemColor.Red;

	public int styleIndex = 0;
	public int redIndex = 0;
	public int yellowIndex = 0;
	public int blueIndex = 0;
	public int greenIndex = 0;

	public GridLayoutGroup styleGrid;
	public GridLayoutGroup[] soundGrids;

	private SoundManager soundMan;

	private Text redText;
	private Text yellowText;
	private Text blueText;
	private Text greenText;

	private void Start()
	{
		soundMan = GameObject.Find("GameManager").GetComponent<SoundManager>();

		for (var i = 0; i < availableStyles.Length; i++)
		{
			Button newButton = Instantiate(buttonPrefab).GetComponent<Button>();
			newButton.transform.SetParent(styleGrid.transform);
			newButton.gameObject.GetComponentInChildren<Text>().text = availableStyles[i].styleName;
			newButton.gameObject.name = availableStyles[i].name + "Button";
			newButton.gameObject.GetComponent<GridButtonComponent>().index = i;
			newButton.gameObject.GetComponent<GridButtonComponent>().type = GridButtonType.Style;
			newButton.gameObject.GetComponent<GridButtonComponent>().hud = this;
		}

		for (var i = 0; i < availableSounds.Length; i++)
		{
			foreach (GridLayoutGroup grid in soundGrids)
			{
				Button newButton = Instantiate(buttonPrefab).GetComponent<Button>();
				newButton.transform.SetParent(grid.transform);
				newButton.gameObject.GetComponentInChildren<Text>().text = availableSounds[i].bankName;
				newButton.gameObject.name = availableSounds[i].name + "Button";
				newButton.gameObject.GetComponent<GridButtonComponent>().index = i;
				newButton.gameObject.GetComponent<GridButtonComponent>().type = GridButtonType.Sound;
				newButton.gameObject.GetComponent<GridButtonComponent>().hud = this;
				newButton.gameObject.GetComponent<GridButtonComponent>().text = grid.transform.GetComponentInParent<Text>();
			}
		}

		Text[] textInChildren = gameObject.GetComponentsInChildren<Text>();
		foreach (Text text in textInChildren)
		{
			if (text.gameObject.name.Contains("Red"))
			{
				redText = text;
			}
			if (text.gameObject.name.Contains("Yellow"))
			{
				yellowText = text;
			}
			if (text.gameObject.name.Contains("Blue"))
			{
				blueText = text;
			}
			if (text.gameObject.name.Contains("Green"))
			{
				greenText = text;
			}
		}

		SetStyle(styleIndex);
	}

	public void SetStyle(int index)
	{
		styleIndex = index;

		soundMan.redBank = availableStyles[styleIndex].redBank;
		soundMan.yellowBank = availableStyles[styleIndex].yellowBank;
		soundMan.blueBank = availableStyles[styleIndex].blueBank;
		soundMan.greenBank = availableStyles[styleIndex].greenBank;

		if (redText != null) redText.text = availableStyles[styleIndex].redBank.bankName;
		if (yellowText != null) yellowText.text = availableStyles[styleIndex].yellowBank.bankName;
		if (blueText != null) blueText.text = availableStyles[styleIndex].blueBank.bankName;
		if (greenText != null) greenText.text = availableStyles[styleIndex].greenBank.bankName;
	}

	public void SetSound(int index)
	{
		switch (currentColor)
		{
			case ElemColor.Red:
				soundMan.redBank = availableSounds[index];
				redIndex = index;
				break;
			case ElemColor.Yellow:
				soundMan.yellowBank = availableSounds[index];
				yellowIndex = index;
				break;
			case ElemColor.Blue:
				soundMan.blueBank = availableSounds[index];
				blueIndex = index;
				break;
			case ElemColor.Green:
				soundMan.greenBank = availableSounds[index];
				greenIndex = index;
				break;
		}
	}

	public SoundBank GetSound(int index)
	{
		return availableSounds[index];
	}

	public void SetColor(ElemColor newColor)
	{
		if (newColor != ElemColor.All) currentColor = newColor;
	}

	public void SetColorToRed()
	{
		currentColor = ElemColor.Red;
	}

	public void SetColorToYellow()
	{
		currentColor = ElemColor.Yellow;
	}

	public void SetColorToBlue()
	{
		currentColor = ElemColor.Blue;
	}

	public void SetColorToGreen()
	{
		currentColor = ElemColor.Green;
	}
}
