using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOutsideToClose : MonoBehaviour {

	[SerializeField]
	private GameObject[] alsoInclude;

	void Update() {
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) {
			this.gameObject.SetActive(ClickingSelfOrChild());
		}
	}
	private bool ClickingSelfOrChild() {
		RectTransform[] rectTransforms = GetComponentsInChildren<RectTransform>();
		foreach (RectTransform rectTransform in rectTransforms) {
			if (EventSystem.current.currentSelectedGameObject == rectTransform.gameObject) {
				return true;
			};
		}

		foreach(GameObject parent in alsoInclude) {
			RectTransform[] rectTransforms2 = parent.GetComponentsInChildren<RectTransform>();
			foreach (RectTransform rectTransform in rectTransforms2) {
				if (EventSystem.current.currentSelectedGameObject == rectTransform.gameObject) {
					return true;
				};
			}
		}

		return false;
	}
}
