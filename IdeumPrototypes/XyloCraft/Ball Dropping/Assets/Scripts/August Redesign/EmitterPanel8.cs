﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmitterPanel8 : MonoBehaviour
{
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

}

public enum EmmiterIf
{
	Click,
	Space,
	Key
}