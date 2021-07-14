﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour {
	private GameObject[] selectedObject = new GameObject[0];
	public SquareSelector square;

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero,Mathf.Infinity,~LayerMask.GetMask("SquareSelect"));
			if (hit.collider != null && hit.transform.TryGetComponent(out ISelectableObj selectObj))//hit.collider != null && hit.collider.tag != "Rotator" && hit.collider.tag != "Ball") {
			{
				if (square.GetSelected().Contains(hit.collider.gameObject))
				{
					// um do nothing i guess
				}
				else
				{
					square.StopSelecting();
					SetSelection(new GameObject[] { hit.collider.gameObject });
				}
			} else if (hit.collider == null && !MenuDragHandlerLnE.dragging) {
				RemoveSelection();
				square.StartSelecting();
			}
		}
		if (MenuDragHandlerLnE.dragging)
        {
			square.StopSelecting();
        }
		if (Input.GetButtonUp("Delete")) {
			DeleteSelection();
		}
	}

	public int selectionLength()
    {
		return selectedObject.Length;
    }

	private void SetSelection(GameObject[] selectedGameObject) {
		if (selectedObject.Length > 0) {
			foreach (GameObject item in selectedObject) {
				if (item.TryGetComponent(out ISelectableObj selectItem))
				{
					selectItem.Deselect();
				}
				else
				{
					//item.transform.GetChild(0).gameObject.SetActive(false);
					//item.transform.GetChild(1).gameObject.SetActive(false);
				} 
			}
		}

		selectedObject = selectedGameObject;
		foreach (GameObject item in selectedObject)
		{
			if (item.TryGetComponent(out ISelectableObj selectItem))
			{
				selectItem.Select();
			}
			else
			{
				//item.transform.GetChild(0).gameObject.SetActive(true);
				//item.transform.GetChild(1).gameObject.SetActive(true);
			}
		}
	}

	private void RemoveSelection() {
		if (selectedObject.Length > 0) {
			foreach (GameObject item in selectedObject) {
				if (item.TryGetComponent(out ISelectableObj selectItem))
				{
					selectItem.Deselect();
				}
				else
				{
					//item.transform.GetChild(0).gameObject.SetActive(false);
					//item.transform.GetChild(1).gameObject.SetActive(false);
				} 
			}
		}
		selectedObject = new GameObject[0];
	}

	public void NewSelection(GameObject[] selectedGameObject) {
		SetSelection(selectedGameObject);
	}

	public void DeleteSelection() {
		foreach (GameObject item in selectedObject) {
			item.SetActive(false);
		}
		square.Delete();

		selectedObject = new GameObject[0];
	}
}
