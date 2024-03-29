﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Component to build and manage our soundbanks
public class StyleHUD : MonoBehaviour {

	public StyleBank[] availableStyles;
	public SoundBank[] availableSounds;
	Dictionary<string, SoundBank> soundMap;

	public GameObject buttonPrefab;
	public ElemColor currentColor = ElemColor.red;

	public GridLayoutGroup styleGrid;
	public GridLayoutGroup[] soundGrids;
	[Space(5)]
	public SoundBankDropdown redDropdown;
	public SoundBankDropdown blueDropdown;
	public SoundBankDropdown yellowDropdown;
	public SoundBankDropdown greenDropdown;
	[Space(5)]

	public Color styleColor;
	public Color soundColor;

	public GameObject footer;

	private SoundManager soundMan;
	private CountLogger countLogger;

	private TMP_Text redText;
	private TMP_Text yellowText;
	private TMP_Text blueText;
	private TMP_Text greenText;

	void Awake()
    {
		footer.SetActive(true);
    }
	// note: style7 and word style are both empty.
	private void Start() {
		soundMan = GameObject.Find("GameManager").GetComponent<SoundManager>();

		List<SoundBank> redBanks = new List<SoundBank>();
		List<SoundBank> blueBanks = new List<SoundBank>();
		List<SoundBank> yellowBanks = new List<SoundBank>();
		List<SoundBank> greenBanks = new List<SoundBank>();

		List<SoundBank> activeBanks = new List<SoundBank>();

		for (var i = 0; i < availableStyles.Length; i++) {
			StyleBank style = availableStyles[i];
			if (style == null)
            {
				continue;
            }
			Toggle newButton = Instantiate(buttonPrefab).GetComponent<Toggle>();
			newButton.gameObject.SetActive(true);
			newButton.transform.SetParent(styleGrid.transform);
			newButton.gameObject.GetComponentInChildren<TMP_Text>().text = availableStyles[i].styleName;
			newButton.gameObject.GetComponentInChildren<TMP_Text>().color = styleColor;
			newButton.gameObject.name = availableStyles[i].name + "Button";
			newButton.gameObject.GetComponent<GridButtonComponent>().index = i;
			newButton.gameObject.GetComponent<GridButtonComponent>().type = GridButtonType.Style;
			newButton.gameObject.GetComponent<GridButtonComponent>().hud = this;
			newButton.transform.localScale = new Vector3(1f, 1f, 1f);

			redBanks.Add(style.redBank);
			activeBanks.Add(style.redBank);

			blueBanks.Add(style.blueBank);
			activeBanks.Add(style.blueBank);

			yellowBanks.Add(style.yellowBank);
			activeBanks.Add(style.yellowBank);

			greenBanks.Add(style.greenBank);
			activeBanks.Add(style.greenBank);
		}

		//activeBanks.AddRange(redBanks);
		//activeBanks.AddRange(blueBanks);
		//activeBanks.AddRange(yellowBanks);
		//activeBanks.AddRange(greenBanks);

		soundMap = new Dictionary<string, SoundBank>();
		for (var i = 0; i < activeBanks.Count; i++)
		{
			SoundBank bank = activeBanks[i];
			if (bank != null)
            {
				soundMap[bank.bankName] = bank;
			}		
		}

		redDropdown.AddSounds(activeBanks.ToArray());
		blueDropdown.AddSounds(activeBanks.ToArray());
		yellowDropdown.AddSounds(activeBanks.ToArray());
		greenDropdown.AddSounds(activeBanks.ToArray());
		/*
		for (var i = 0; i < availableSounds.Length; i++) {
			foreach (GridLayoutGroup grid in soundGrids) {
				Button newButton = Instantiate(buttonPrefab).GetComponent<Button>();
				newButton.transform.SetParent(grid.transform);
				newButton.gameObject.GetComponentInChildren<TMP_Text>().text = availableSounds[i].bankName;
				newButton.gameObject.GetComponentInChildren<TMP_Text>().color = soundColor;
				newButton.gameObject.name = availableSounds[i].name + "Button";
				newButton.gameObject.GetComponent<GridButtonComponent>().index = i;
				newButton.gameObject.GetComponent<GridButtonComponent>().type = GridButtonType.Sound;
				newButton.gameObject.GetComponent<GridButtonComponent>().hud = this;
				newButton.gameObject.GetComponent<ExpandWindow>().Window = grid.gameObject.transform.parent.transform.parent.transform.parent.gameObject; //Fragile, I don't like it
				newButton.gameObject.SetActive(true);
			}
		}
		*/
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

		//GetComponentInChildren<RawImage>().gameObject.SetActive(false);
		ResetTextNames();
		countLogger = FindObjectOfType<CountLogger>();

		footer.SetActive(false);
	}

	public void SetStyle(int index) {
		SetSound(ElemColor.red, availableStyles[index].redBank);
		//soundMan.redBank = availableStyles[index].redBank;
		SetSound(ElemColor.yellow, availableStyles[index].yellowBank);
		//soundMan.yellowBank = availableStyles[index].yellowBank;
		SetSound(ElemColor.blue, availableStyles[index].blueBank);
		//soundMan.blueBank = availableStyles[index].blueBank;
		SetSound(ElemColor.green, availableStyles[index].greenBank);
		//soundMan.greenBank = availableStyles[index].greenBank;

		ResetTextNames();
	}

	public void SetSound(int index) {
		switch (currentColor) {
			case ElemColor.red:
				soundMan.redBank = availableSounds[index];
				break;
			case ElemColor.yellow:
				soundMan.yellowBank = availableSounds[index];
				break;
			case ElemColor.blue:
				soundMan.blueBank = availableSounds[index];
				break;
			case ElemColor.green:
				soundMan.greenBank = availableSounds[index];
				break;
		}
	}

	public void SetSound(ElemColor color, SoundBank bank)
    {
		//Debug.Log("set " + color + " sound to " + bank.bankName);
		switch (color)
		{
			case ElemColor.red:
				soundMan.redBank = bank;
				break;
			case ElemColor.yellow:
				soundMan.yellowBank = bank;
				break;
			case ElemColor.blue:
				soundMan.blueBank = bank;
				break;
			case ElemColor.green:
				soundMan.greenBank = bank;
				break;
		}
	}

	public void SetSound(ElemColor color, string name)
    {
		if (soundMap.TryGetValue(name, out SoundBank bank))
        {
			SetSound(color, bank);
        }
    }
	public SoundBank GetSound(int index) {
		return availableSounds[index];
	}

	public void SetColor(ElemColor newColor) {
		if (newColor != ElemColor.any) currentColor = newColor;
	}

	public void SetColorToRed() {
		currentColor = ElemColor.red;
	}

	public void SetColorToYellow() {
		currentColor = ElemColor.yellow;
	}

	public void SetColorToBlue() {
		currentColor = ElemColor.blue;
	}

	public void SetColorToGreen() {
		currentColor = ElemColor.green;
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

		redDropdown.SetItemFromName(soundMan.redBank.bankName);
		yellowDropdown.SetItemFromName(soundMan.yellowBank.bankName);
		blueDropdown.SetItemFromName(soundMan.blueBank.bankName);
		greenDropdown.SetItemFromName(soundMan.greenBank.bankName);
	}
}
