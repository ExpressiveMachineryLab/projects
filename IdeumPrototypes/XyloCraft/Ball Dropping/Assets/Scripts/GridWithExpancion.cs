﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridWithExpancion : MonoBehaviour {
	public Button addStatementButton;
	public float paddingLeft, paddingTop, spacing;

	private List<RectTransform> kids = new List<RectTransform>();


	private int showing = 1;
	void Awake() {
		for (int i = 0; i < transform.childCount; i++) {
			RectTransform child = transform.GetChild(i).GetComponent<RectTransform>();
			kids.Add(child);
			if (i >= showing)
            {
				//child.gameObject.SetActive(false);
            }
		}
	}

    private void Start()
    {
		for (int i = 0; i < 4; i++)
		{
			if (i >= showing)
			{
				kids[i].gameObject.SetActive(false);
			}
		}
	}

    void Update() {
		float currentY = -paddingTop;
		foreach (RectTransform pos in kids) {
			pos.anchoredPosition = new Vector3(paddingLeft, currentY, 0);
			currentY -= pos.rect.height + spacing;
		}
		//Rect rect = addStatementButton.GetComponent<RectTransform>().rect;
		//rect.Set(rect.x, rect.y - (rect.height + spacing), rect.width, rect.height);
	}

	public void AddStatement()
    {
		if (showing < 4)
        {
			showing++;
			for (int i = 0; i < 4; i++)
            {
				if (i < showing)
                {
					kids[i].gameObject.SetActive(true);
                }
            }
			Rect rect = addStatementButton.GetComponent<RectTransform>().rect;
			addStatementButton.GetComponent<RectTransform>().Translate(Vector3.up * -1 * (108 + spacing + 10));
			//Rect rect = addStatementButton.GetComponent<RectTransform>().rect;
			//rect.Set(rect.x, rect.y - (rect.height + spacing), rect.width, rect.height);
		}
		if (showing >= 4)
        {
			addStatementButton.gameObject.SetActive(false);
        }
    }
}