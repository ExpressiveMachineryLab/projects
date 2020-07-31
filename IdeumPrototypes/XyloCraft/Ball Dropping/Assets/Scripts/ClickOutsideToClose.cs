using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOutsideToClose : MonoBehaviour {

	[SerializeField]
	private GameObject[] alsoInclude;

	void LateUpdate() {
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) {
			this.gameObject.SetActive(ClickingSelfOrChild());
		}
	}
	private bool ClickingSelfOrChild() {
		//Debug.Log("```Clicking Self Or Child");
		//Debug.Log("```Current Selected Game Object: " + EventSystem.current.currentSelectedGameObject.name);
		RectTransform[] rectTransforms = gameObject.GetComponentsInChildren<RectTransform>();
		foreach (RectTransform rectTransform in rectTransforms) {
			//Debug.Log("Checking " + rectTransform.gameObject.name);
			if (EventSystem.current.currentSelectedGameObject == rectTransform.gameObject) {
				//Debug.Log("```Found");
				return true;
			}
		}
		
		//Debug.Log("```Checking Included");
		foreach (GameObject parent in alsoInclude) {
			RectTransform[] rectTransforms2 = parent.GetComponentsInChildren<RectTransform>();
			foreach (RectTransform rectTransform in rectTransforms2) {
				//Debug.Log("Checking " + rectTransform.gameObject.name);
				if (EventSystem.current.currentSelectedGameObject == rectTransform.gameObject) {
					//Debug.Log("Found");
					return true;
				}
			}
		}


		//Debug.Log("```Not Found");
		return false;
	}
}
