	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {
	public GameObject popup;
	public GameObject pointer;
	public GameObject prevButton;
	public GameObject nextButton;
	public TMP_Text titleText;
	public TMP_Text pageText;
	public TMP_Text pageNumberText;
	public GameObject challengeIcon;
	public GameObject progressText;
	public GameObject progressItemPrefab;
	public GameObject tutorialPanel;

	public TutSequence[] sequences;

	public Color regularColor;
	public Color challengeColor;

	private int tutorialIndex = 0;
	private int popupIndex = 0;

	private bool tutorialActive = false;

	void Start() {
		popup.SetActive(false);
		pointer.SetActive(false);
		progressText.SetActive(false);
		tutorialPanel.SetActive(true);
	}

	public void StartTurotial(int index) {
		if (tutorialActive) return;
		tutorialIndex = index;
		popupIndex = sequences[tutorialIndex].progress;

		FillPopup();
		setupProgressText();
		FillPorgressText();
		popup.SetActive(true);
		progressText.SetActive(true);
		tutorialPanel.SetActive(false);
		tutorialActive = true;
	}

	public void StopTutorial() {
		popup.SetActive(false);
		pointer.SetActive(false);
		progressText.SetActive(false);
		tutorialPanel.SetActive(true);
		tutorialActive = false;
	}

	public void NextPopup() {
		if (popupIndex >= sequences[tutorialIndex].sequnce.Length - 1) return;
		popupIndex++;
		sequences[tutorialIndex].progress = popupIndex;
		FillPopup();
		FillPorgressText();
	}

	public void PreviousPopup() {
		if (popupIndex <= 0) return;
		popupIndex--;
		FillPopup();
	}

	public void navigateToIndex(Button i)
    {
		int index = int.Parse(i.GetComponent<TMP_Text>().text.Split('.')[0]);
		if (index >= sequences[tutorialIndex].sequnce.Length) return;
		sequences[tutorialIndex].progress = index;
		popupIndex = index;
		FillPopup();
		FillPorgressText();
	}

	private void FillPopup() {
		titleText.text = sequences[tutorialIndex].sequnce[popupIndex].cardTitle;
		pageText.text = sequences[tutorialIndex].sequnce[popupIndex].cardText;
		pageNumberText.text = "" + popupIndex + "/" + (sequences[tutorialIndex].sequnce.Length - 1);
		popup.GetComponent<RectTransform>().anchoredPosition = sequences[tutorialIndex].sequnce[popupIndex].popupPosition;

		if (sequences[tutorialIndex].sequnce[popupIndex].challenge) titleText.color = challengeColor;
		else titleText.color = regularColor;

		prevButton.SetActive(popupIndex >= 1);
		nextButton.SetActive(popupIndex < sequences[tutorialIndex].sequnce.Length - 1);
		challengeIcon.SetActive(sequences[tutorialIndex].sequnce[popupIndex].challenge);

		pointer.SetActive(sequences[tutorialIndex].sequnce[popupIndex].usePointer);
		pointer.GetComponent<RectTransform>().anchoredPosition = sequences[tutorialIndex].sequnce[popupIndex].pointerPosition;
		pointer.transform.rotation =  Quaternion.Euler(0f, 0f, sequences[tutorialIndex].sequnce[popupIndex].pointerRotation);
	}

	private void FillPorgressText() {

		GameObject[] tableOfContents = new GameObject[progressText.transform.childCount];
		int index = 0;

		foreach(Transform childObj in progressText.transform)
        {
			tableOfContents[index] = childObj.gameObject;
			index++;
        }

		TMP_Text toBold = tableOfContents[sequences[tutorialIndex].progress].GetComponent<TMP_Text>();
		toBold.fontStyle = FontStyles.Bold;

		//string newText = "<b>" + sequences[tutorialIndex].tutTitle + "\n\n";
		//for (int i = 0; i < sequences[tutorialIndex].sequnce.Length; i++) {
		//	newText += i + ". " + sequences[tutorialIndex].sequnce[i].cardTitle;

		//	if (i == sequences[tutorialIndex].progress) newText += "</b>";
		//	newText += "\n\n";
		//}

		//progressText.GetComponent<TMP_Text>().text = newText;
	}

	private void setupProgressText()
    {
		//clear any existing children
		foreach(Transform t in progressText.transform)
        {
			Destroy(t.gameObject);
        }
		for (int i = 0; i < sequences[tutorialIndex].sequnce.Length; i++) {
			GameObject newChild = Instantiate(progressItemPrefab, progressText.transform);
			newChild.GetComponent<TMP_Text>().SetText(i.ToString() + ". " + sequences[tutorialIndex].sequnce[i].cardTitle);
			
			Button b = newChild.GetComponent<Button>();
			b.onClick.AddListener(delegate { navigateToIndex(b); });

			if (sequences[tutorialIndex].sequnce[i].cardTitle == "Challenge"){
				Debug.Log("Challenge");
				newChild.GetComponent<TMP_Text>().color = challengeColor;
			}
			//	newText += i + ". " + sequences[tutorialIndex].sequnce[i].cardTitle;

			//	if (i == sequences[tutorialIndex].progress) newText += "</b>";

		}


	}
}

[System.Serializable]
public class TutSequence {
	public string tutTitle;
	public TutPopup[] sequnce;
	public int progress = 0;
}

[System.Serializable]
public class TutPopup {
	public string cardTitle;
	public Vector3 popupPosition;
	public Vector3 pointerPosition;
	public float pointerRotation;
	public bool usePointer;
	public bool challenge;
	[TextArea]
	public string cardText;
}