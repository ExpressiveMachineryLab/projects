using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Component to build and manage our soundbanks
public class StyleHUD : MonoBehaviour {

	public StyleBank[] availableStyles;
	public SoundBank[] availableSounds;
	public GameObject buttonPrefab;
	public ElemColor currentColor = ElemColor.Red;

	public GridLayoutGroup styleGrid;
	public GridLayoutGroup[] soundGrids;

	private SoundManager soundMan;
	private CountLogger countLogger;

	private TMP_Text redText;
	private TMP_Text yellowText;
	private TMP_Text blueText;
	private TMP_Text greenText;

	private void Start() {
		soundMan = GameObject.Find("GameManager").GetComponent<SoundManager>();

		for (var i = 0; i < availableStyles.Length; i++) {
			Button newButton = Instantiate(buttonPrefab).GetComponent<Button>();
			newButton.transform.SetParent(styleGrid.transform);
			newButton.gameObject.GetComponentInChildren<TMP_Text>().text = availableStyles[i].styleName;
			newButton.gameObject.name = availableStyles[i].name + "Button";
			newButton.gameObject.GetComponent<GridButtonComponent>().index = i;
			newButton.gameObject.GetComponent<GridButtonComponent>().type = GridButtonType.Style;
			newButton.gameObject.GetComponent<GridButtonComponent>().hud = this;
		}

		for (var i = 0; i < availableSounds.Length; i++) {
			foreach (GridLayoutGroup grid in soundGrids) {
				Button newButton = Instantiate(buttonPrefab).GetComponent<Button>();
				newButton.transform.SetParent(grid.transform);
				newButton.gameObject.GetComponentInChildren<TMP_Text>().text = availableSounds[i].bankName;
				newButton.gameObject.name = availableSounds[i].name + "Button";
				newButton.gameObject.GetComponent<GridButtonComponent>().index = i;
				newButton.gameObject.GetComponent<GridButtonComponent>().type = GridButtonType.Sound;
				newButton.gameObject.GetComponent<GridButtonComponent>().hud = this;
				newButton.gameObject.GetComponent<ExpandWindow>().Window = grid.gameObject.transform.parent.transform.parent.transform.parent.gameObject; //Fragile, I don't like it
			}
		}

		TMP_Text[] textInChildren = gameObject.GetComponentsInChildren<TMP_Text>();
		foreach (TMP_Text text in textInChildren) {
			if (text.gameObject.name.Contains("Red")) {
				redText = text;
			}
			if (text.gameObject.name.Contains("Yellow")) {
				yellowText = text;
			}
			if (text.gameObject.name.Contains("Blue")) {
				blueText = text;
			}
			if (text.gameObject.name.Contains("Green")) {
				greenText = text;
			}
		}

		GetComponentInChildren<RawImage>().gameObject.SetActive(false);
		ResetTextNames();
		countLogger = FindObjectOfType<CountLogger>();
	}

	public void SetStyle(int index) {
		soundMan.redBank = availableStyles[index].redBank;
		soundMan.yellowBank = availableStyles[index].yellowBank;
		soundMan.blueBank = availableStyles[index].blueBank;
		soundMan.greenBank = availableStyles[index].greenBank;

		ResetTextNames();
	}

	public void SetSound(int index) {
		switch (currentColor) {
			case ElemColor.Red:
				soundMan.redBank = availableSounds[index];
				break;
			case ElemColor.Yellow:
				soundMan.yellowBank = availableSounds[index];
				break;
			case ElemColor.Blue:
				soundMan.blueBank = availableSounds[index];
				break;
			case ElemColor.Green:
				soundMan.greenBank = availableSounds[index];
				break;
		}
	}

	public SoundBank GetSound(int index) {
		return availableSounds[index];
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

	//Interface CountLogger

	public void IncSoundBankClicks() {
		countLogger?.IncSoundBankClicks();
	}

	public void ResetTextNames() {
		//Debug.Log("Resetting names");
		if (redText != null) redText.text = soundMan.redBank.bankName;
		if (yellowText != null) yellowText.text = soundMan.yellowBank.bankName;
		if (blueText != null) blueText.text = soundMan.blueBank.bankName;
		if (greenText != null) greenText.text = soundMan.greenBank.bankName;
	}
}
