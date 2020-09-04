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
	public Text rhythmText;
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
		if (selectedRhythm > 8) selectedRhythm = 8;
		if (selectedRhythm < 1) selectedRhythm = 1;
		rhythmText.text = "" + selectedRhythm;
	}

	public void IncRhythm()
	{
		selectedRhythm++;
		if (selectedRhythm > 8) selectedRhythm = 8;
		if (rhythmText != null) rhythmText.text = "" + selectedRhythm;
	}

	public void DecRhyth()
	{
		selectedRhythm--;
		if (selectedRhythm < 1) selectedRhythm = 1;
		if (rhythmText != null) rhythmText.text = "" + selectedRhythm;
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

	public string LinePanelToSO()
	{
		string SOstring = "3";
		SOstring += "i";
		SOstring += "," + (int)mode;
		SOstring += "," + (int)selectedChord;
		SOstring += "," + selectedRhythm;
		SOstring += "," + (int)selectedVisual;
		SOstring += "," + (chordPlus ? "1" : "0");
		SOstring += "," + (chordMinus ? "1" : "0");
		SOstring += "," + (visualPlus ? "1" : "0");
		SOstring += "," + (visualMinus ? "1" : "0");
		SOstring += "," + (int)ballElement.color;
		SOstring += "," + (int)lineElement.color;

		return SOstring;
	}

	public void LinePanelFromSO(string SOlinePanel)
	{
		string[] SOstring = SOlinePanel.Split(new[] { "," }, System.StringSplitOptions.None);

		mode = (PanelMode)int.Parse(SOstring[1]);
		selectedChord = (SelectedPM)int.Parse(SOstring[2]);
		selectedRhythm = int.Parse(SOstring[3]);
		selectedVisual = (SelectedPM)int.Parse(SOstring[4]);
		chordPlus = int.Parse(SOstring[5]) == 1 ? true : false;
		chordMinus = int.Parse(SOstring[6]) == 1 ? true : false;
		visualPlus = int.Parse(SOstring[7]) == 1 ? true : false;
		visualMinus = int.Parse(SOstring[8]) == 1 ? true : false;
		ballElement.color = (ElemColor)int.Parse(SOstring[9]);
		lineElement.color = (ElemColor)int.Parse(SOstring[10]);
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