using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWithExpancion : MonoBehaviour {
	public float paddingLeft, paddingTop, spacing;

	private List<RectTransform> kids = new List<RectTransform>();

	void Start() {
		for (int i = 0; i < transform.childCount; i++) {
			kids.Add(transform.GetChild(i).GetComponent<RectTransform>());
		}
	}

	void Update() {
		float currentY = -paddingTop;
		foreach (RectTransform pos in kids) {
			pos.position = new Vector3(paddingLeft, currentY, 0) + transform.position;
			currentY -= pos.rect.height + spacing;
		}
	}
}
