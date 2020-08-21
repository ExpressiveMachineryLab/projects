using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinePanel8 : MonoBehaviour
{
	public PanelMode mode = PanelMode.Chord;

	public SelectedElement ballElement;
	public SelectedElement lineElement;

	public SelectedPM selectedChord = SelectedPM.Off;
	public int selectedRhythm = 1;
	public SelectedPM selectedVisual = SelectedPM.Off;

	private bool chordPlus = false;
	private bool chordMinus = false;
	private bool visualPlus = false;
	private bool visualMinus = false;

	public Dropdown modeDropdown;
	public GameObject chordItems;
	public GameObject rhythmItems;
	public GameObject visualItems;

	private void Start()
	{
		SelectedElement[] elements = GetComponentsInChildren<SelectedElement>();
		foreach (SelectedElement element in elements)
		{
			if (element.type == SelectedElementType.Ball) ballElement = element;
			if (element.type == SelectedElementType.Line) lineElement = element;
		}
	}

	//Flash the box
	public void FlashBox()
	{

	}

	public ElemColor GetBallColor()
	{
		return ballElement.color;
	}

	public ElemColor GetLineColor()
	{
		return lineElement.color;
	}

	//Panel modes
	public void SetModeToChord()
	{
		mode = PanelMode.Chord;
		chordItems.SetActive(true);
		rhythmItems.SetActive(false);
		visualItems.SetActive(false);
	}

	public void SetModeToRhythm()
	{
		mode = PanelMode.Rhythm;
		chordItems.SetActive(false);
		rhythmItems.SetActive(true);
		visualItems.SetActive(false);
	}

	public void SetModeToVisual()
	{
		mode = PanelMode.Visual;
		chordItems.SetActive(false);
		rhythmItems.SetActive(false);
		visualItems.SetActive(true);
	}

	public void UpdateFromDropdown()
	{
		switch (modeDropdown.value)
		{
			case 0:
				SetModeToChord();
				break;
			case 1:
				SetModeToRhythm();
				break;
			case 2:
				SetModeToVisual();
				break;
		}
	}

	public void UpdateFromInt(int value)
	{
		switch (value)
		{
			case 0:
				SetModeToChord();
				break;
			case 1:
				SetModeToRhythm();
				break;
			case 2:
				SetModeToVisual();
				break;
		}
	}

	//Chord options
	public void ToggleChordPlus()
	{
		chordPlus = !chordPlus;
		SetSelectedChord();
	}

	public void ToggleChordMinus()
	{
		chordMinus = !chordMinus;
		SetSelectedChord();
	}

	private void SetSelectedChord()
	{
		if (chordPlus && chordMinus)
		{
			selectedChord = SelectedPM.PlusMinus;
		}
		else if (chordPlus && !chordMinus)
		{
			selectedChord = SelectedPM.Plus;
		}
		else if (!chordPlus && chordMinus)
		{
			selectedChord = SelectedPM.Minus;
		}
		else if (!chordPlus && !chordMinus)
		{
			selectedChord = SelectedPM.Off;
		}
	}

	//Rhythm options
	public void SetRhythmToNumber(int number)
	{
		selectedRhythm = number;
	}

	//Visual options
	public void ToggleVisualPlus()
	{
		visualPlus = !visualPlus;
		SetSelectedVisual();
	}

	public void ToggleVisualMinus()
	{
		visualMinus = !visualMinus;
		SetSelectedVisual();
	}

	private void SetSelectedVisual()
	{
		if (visualPlus && visualMinus)
		{
			selectedVisual = SelectedPM.PlusMinus;
		}
		else if (visualPlus && !visualMinus)
		{
			selectedVisual = SelectedPM.Plus;
		}
		else if (!visualPlus && visualMinus)
		{
			selectedVisual = SelectedPM.Minus;
		}
		else if (!visualPlus && !visualMinus)
		{
			selectedVisual = SelectedPM.Off;
		}
	}
}

public enum PanelMode
{
	Chord,
	Rhythm,
	Visual
}

public enum SelectedPM
{
	Plus,
	Minus,
	PlusMinus,
	Off
}