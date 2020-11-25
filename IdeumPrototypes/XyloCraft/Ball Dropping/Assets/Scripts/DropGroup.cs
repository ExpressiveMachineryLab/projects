using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGroup : MonoBehaviour
{
	public GameObject[] dropGroup;

	private void Start()
	{
		FindObjectOfType<SelectionManager>().NewSelection(dropGroup);
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			gameObject.transform.GetChild(i).gameObject.SetActive(true);
		}
	}
}