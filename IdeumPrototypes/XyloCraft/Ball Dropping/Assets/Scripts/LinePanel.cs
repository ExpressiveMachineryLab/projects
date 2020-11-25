using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinePanel : MonoBehaviour
{
	public string id = "";

	public PanelMode mode = PanelMode.Chord;

	[HideInInspector]
	public SelectedElement ballElement, lineElement;

	public SelectedPM selectedChord = SelectedPM.Plus;
	public int selectedRhythm = 1;
	public SelectedPM selectedVisual = SelectedPM.Off;

	private bool chordPlus = true;
	private bool chordMinus = false;
	private bool visualPlus = false;
	private bool visualMinus = false;

	public Dropdown modeDropdown;
	public Text rhythmText;
	public GameObject chordItems;
	public GameObject rhythmItems;
	public GameObject visualItems;

	private string copyStartState;

	private void Start()
	{
		SelectedElement[] elements = GetComponentsInChildren<SelectedElement>();
		foreach (SelectedElement element in elements)
		{
			if (element.type == SelectedElementType.Ball) ballElement = element;
			if (element.type == SelectedElementType.Line) lineElement = element;
		}

		if (id == "")
		{
			id = "3";
			RandomString randomstring = new RandomString();
			id += randomstring.CreateRandomString(1);
		}
		else if (!id[0].Equals("3".ToCharArray()[0]))
		{
			id = "3" + id;
		}

		copyStartState = LinePanelToSO();
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

	public void SetChordToPlus()
	{
		chordPlus = true;
		chordMinus = false;
		SetSelectedChord();
	}

	public void SetChordToMinus()
	{
		chordPlus = false;
		chordMinus = true;
		SetSelectedChord();
	}

	public void ToggleChord()
	{
		chordPlus = !chordPlus;
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

	//Save and load

	public void ResetPanel()
	{
		LinePanelFromSO(copyStartState);
	}

	public string LinePanelToSO()
	{
		string SOstring = id;
		SOstring += "," + (int)mode;
		SOstring += selectedRhythm;
		SOstring += (chordPlus ? "1" : "0");
		SOstring += (chordMinus ? "1" : "0");
		SOstring += (visualPlus ? "1" : "0");
		SOstring += (visualMinus ? "1" : "0");
		SOstring += (int)ballElement.color;
		SOstring += (int)lineElement.color;

		return SOstring;
	}

	public void LinePanelFromSO(string SOlinePanel)
	{
		chordItems.SetActive(true);
		rhythmItems.SetActive(true);
		visualItems.SetActive(true);
		ToggleSelected[] toggles = GetComponentsInChildren<ToggleSelected>();
		string[] SOstring = SOlinePanel.Split(new[] { "," }, System.StringSplitOptions.None);

		mode = (PanelMode)int.Parse(SOstring[1][0].ToString());
		modeDropdown.value = (int)mode;
		UpdateFromDropdown();

		selectedRhythm = int.Parse(SOstring[1][1].ToString());
		rhythmText.text = "" + selectedRhythm;

		chordPlus = int.Parse(SOstring[1][2].ToString()) == 1 ? true : false;
		toggles[0].SetToggle(chordPlus);

		chordMinus = int.Parse(SOstring[1][3].ToString()) == 1 ? true : false;
		toggles[1].SetToggle(chordMinus);
		SetSelectedChord();

		visualPlus = int.Parse(SOstring[1][4].ToString()) == 1 ? true : false;
		//toggles[2].SetToggle(visualPlus);

		visualMinus = int.Parse(SOstring[1][5].ToString()) == 1 ? true : false;
		//toggles[3].SetToggle(visualMinus);
		SetSelectedVisual();

		ballElement.color = (ElemColor)int.Parse(SOstring[1][6].ToString());
		ballElement.UpdateImage();

		lineElement.color = (ElemColor)int.Parse(SOstring[1][7].ToString());
		lineElement.UpdateImage();
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