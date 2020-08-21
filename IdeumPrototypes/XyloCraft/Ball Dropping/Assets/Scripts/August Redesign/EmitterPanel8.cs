using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmitterPanel8 : MonoBehaviour
{
	public EmmiterIf ifType;
	public SelectedElement birdElement;
	public int numberToShoot = 1;

	public Dropdown actionDropdown;

	private void Start()
	{
		SelectedElement[] elements = GetComponentsInChildren<SelectedElement>();
		foreach (SelectedElement element in elements)
		{
			if (element.type == SelectedElementType.Emitter) birdElement = element;
		}
	}

	public ElemColor GetBirdColor()
	{
		return birdElement.color;
	}

}

public enum EmmiterIf
{
	Click,
	Space,
	Key
}