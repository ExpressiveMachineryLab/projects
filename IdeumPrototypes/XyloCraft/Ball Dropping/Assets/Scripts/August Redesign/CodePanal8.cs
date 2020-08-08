using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePanel8 : MonoBehaviour
{
	public PanelMode mode = PanelMode.Chord;
	public ElemColor ballColor;
	public ElemColor lineColor;

	public SelectedPM selectedChord;
	public int selectedRhythm;
	public SelectedPM selectedVisual;

	public void FlashBox()
	{

	}

	public void SetModeToChord()
	{
		mode = PanelMode.Chord;
	}

	public void SetModeToRhythm()
	{
		mode = PanelMode.Rhythm;
	}

	public void SetModeToVisual()
	{
		mode = PanelMode.Visual;
	}

	public void SetBallColorToRed()
	{
		ballColor = ElemColor.Red;
	}

	public void SetBallColorToYellow()
	{
		ballColor = ElemColor.Yellow;
	}

	public void SetBallColorToBlue()
	{
		ballColor = ElemColor.Blue;
	}

	public void SetBallColorToGreen()
	{
		ballColor = ElemColor.Green;
	}

	public void SetBallColorToAll()
	{
		ballColor = ElemColor.All;
	}

	public void SetLineColorToRed()
	{
		lineColor = ElemColor.Red;
	}

	public void SetLineColorToYellow()
	{
		lineColor = ElemColor.Yellow;
	}

	public void SetLineColorToBlue()
	{
		lineColor = ElemColor.Blue;
	}

	public void SetLineColorToGreen()
	{
		lineColor = ElemColor.Green;
	}

	public void SetLineColorToAll()
	{
		lineColor = ElemColor.All;
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
	PlusMinus
}