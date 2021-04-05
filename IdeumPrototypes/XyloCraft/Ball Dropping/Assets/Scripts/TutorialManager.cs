using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour {
	public GameObject popup;
	public GameObject prevButton;
	public GameObject nextButton;
	public TMP_Text titleText;
	public TMP_Text pageText;
	public TMP_Text pageNumberText;

	public TutSequence[] sequences;

	private int tutorialIndex;
}

[System.Serializable]
public class TutSequence {
	public string tutTitle;
	public TutPopup[] sequnce;
}

[System.Serializable]
public class TutPopup {
	public Transform mainTransform;
	public Transform pointerTransform;
	public bool usePointer;
	public string cardTitle;
	public string cardText;
}