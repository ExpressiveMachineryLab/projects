﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmitterPanel8 : MonoBehaviour
{
	public string id = "";
	public EmmiterIf ifType;
	public SelectedElement birdElement;
	public int numberToShoot = 1;

	public Dropdown actionDropdown;
	public Text numberText;

	private void Start()
	{
		SelectedElement[] elements = GetComponentsInChildren<SelectedElement>();
		foreach (SelectedElement element in elements)
		{
			if (element.type == SelectedElementType.Emitter) birdElement = element;
		}

		if (id == "")
		{
			id = "4";
			RandomString randomstring = new RandomString();
			id += randomstring.CreateRandomString(1);
		}
		else if (!id[0].Equals("4".ToCharArray()[0]))
		{
			id = "4" + id;
		}
	}

	public void FlashBox()
	{

	}

	public ElemColor GetBirdColor()
	{
		return birdElement.color;
	}

	public void UpdateFromDropdown()
	{
		switch (actionDropdown.value)
		{
			case 0:
				ifType = EmmiterIf.Click;
				break;
			case 1:
				ifType = EmmiterIf.Space;
				break;
			case 2:
				ifType = EmmiterIf.Key;
				break;
		}
	}

	public void UpdateFromInt (int value)
	{
		switch (value)
		{
			case 0:
				ifType = EmmiterIf.Click;
				break;
			case 1:
				ifType = EmmiterIf.Space;
				break;
			case 2:
				ifType = EmmiterIf.Key;
				break;
		}
	}

	public void IncNumberToShoot()
	{
		numberToShoot++;
		if (numberToShoot > 8) numberToShoot = 8;
		if (numberText != null) numberText.text = "" + numberToShoot;
	}

	public void DecNumberToShoot()
	{
		numberToShoot--;
		if (numberToShoot < 1) numberToShoot = 1;
		if (numberText != null) numberText.text = "" + numberToShoot;
	}

	public string EmitterPanelToSO()
	{
		string SOstring = id;
		SOstring += "," + (int)ifType;
		SOstring += "," + numberToShoot;
		SOstring += "," + (int)birdElement.color;

		return SOstring;
	}

	public void EmitterPanelFromSO(string SOlinePanel)
	{
		string[] SOstring = SOlinePanel.Split(new[] { "," }, System.StringSplitOptions.None);

		ifType = (EmmiterIf)int.Parse(SOstring[1]);
		actionDropdown.value = int.Parse(SOstring[1]);

		numberToShoot = int.Parse(SOstring[2]);
		numberText.text = "" + int.Parse(SOstring[2]);

		birdElement.color = (ElemColor)int.Parse(SOstring[3]);
		birdElement.UpdateImage();
	}
}

public enum EmmiterIf
{
	Click,
	Space,
	Key
}