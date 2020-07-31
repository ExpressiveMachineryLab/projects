using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StyleHUD : MonoBehaviour {

	[SerializeField]
	private StyleBank[] availableStyles;
	[SerializeField]
	private SoundBank[] availableSounds;
	private ElemColor currentColor = ElemColor.Red;

	public int styleIndex = 0;
	public int redIndex = 0;
	public int yellowIndex = 0;
	public int blueIndex = 0;
	public int greenIndex = 0;

	public GridLayoutGroup styleGrid;
	public GridLayoutGroup soundGrid;

	private SoundManager soundMan;

	private void Start() {
		soundMan = GameObject.Find("GameManager").GetComponent<SoundManager>();
	}

	public void setStyle(int index) {
		//Debug.Log("Setting STyle to " + index);
		styleIndex = index;

		soundMan.redBank = availableStyles[styleIndex].redBank;
		soundMan.yellowBank = availableStyles[styleIndex].yellowBank;
		soundMan.blueBank = availableStyles[styleIndex].blueBank;
		soundMan.greenBank = availableStyles[styleIndex].GreenBank;
	}

	public void setSound(int index) {
		switch (currentColor) {
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

	public void SetColor(ElemColor newColor) {
		if (newColor != ElemColor.All) currentColor = newColor;
	}

	public void SetColorToRed() {
		currentColor = ElemColor.Red;
	}

	public void SetColorToYellow() {
		currentColor = ElemColor.Yellow;
	}

	public void SetColorToBlue() {
		currentColor = ElemColor.Blue;
	}

	public void SetColorToGreen() {
		currentColor = ElemColor.Green;
	}
}
