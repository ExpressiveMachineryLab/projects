using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InspectorPanel : MonoBehaviour {
	public TMP_Text codeText;
	public TMP_Text definitionText;
	
	EventSystem m_EventSystem;
	PointerEventData m_PointerEventData;

	void Start() {
		m_EventSystem = FindObjectOfType<EventSystem>();
	}

	void Update() {
		m_PointerEventData = new PointerEventData(m_EventSystem);
		m_PointerEventData.position = Input.mousePosition;

		List<RaycastResult> resultList = new List<RaycastResult>();
		EventSystem.current.RaycastAll(m_PointerEventData, resultList);

		codeText.text = "";
		definitionText.text = "";

		foreach (RaycastResult result in resultList) {
			if (result.gameObject.GetComponent<SetTextTMPFromSlider>() != null) {
				codeText.text = "If (slider is moved to the right)\nThen { increase speed by one }\n\nIf (slider is moved to the left)\nThen  { decrease speed by one }";
				definitionText.text = "<b>Definition</b>\n\nThe speed of your balls is stored in a global variable called speed. It holds the number that represents how fast the balls will move across the canvas.";
				break;
			} else if (result.gameObject.GetComponent<LinePanel>() != null) {
				LinePanel panel = result.gameObject.GetComponent<LinePanel>();
				if (panel.ballElement.color != ElemColor.None && panel.lineElement.color != ElemColor.None) {
					codeText.text = "If (" + panel.GetBallColor() + " ball hits a " + panel.GetLineColor() + " line)\n";
					if (panel.mode == PanelMode.Chord) {
						codeText.text += "Then { go to the " + panel.selectedChord + " sound in the soundbank }";
					}
					if (panel.mode == PanelMode.Rhythm) {
						codeText.text += "Then { play sound " + panel.selectedRhythm + " times }";
					}
					if (panel.mode == PanelMode.Visual) {
						codeText.text += "Then { trigger visual effect }";
					}
				}
				definitionText.text = "<b>Definition</b>\n\nIn a conditional block, decisions are made based on whether something is true or false. In computer programming, these true or false terms are data types that are called booleans.";
				break;
			} else if (result.gameObject.name == "Shapes") {
				codeText.text = "Drag a shape onto the canvas and release a ball inside the shape to hear your musical rhythms in action.";
				definitionText.text = "<b>Definition</b>\n\nYou can use shapes in Xylocode to create music.\n\nShapes can help you create rhythms and play with different instrument types.";
				break;
			} else if (result.gameObject.GetComponent<StyleHUD>() != null) {
				definitionText.text = "<b>Definition</b>\n\nYou can assign a family of sounds to the lines or you can choose an individual sound for each color. Test these things out!";
				break;
			} else if (result.gameObject.name == "SoundBankButton") {
				definitionText.text = "<b>Definition</b>\n\nYou can assign a family of sounds to the lines or you can choose an individual sound for each color. Test these things out!";
				break;
			}
		}

		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

		if (hit.collider != null) {
			if (hit.collider.gameObject.GetComponent<Line>() != null) {
				codeText.text = "Can you think of what the white highlighted piece of the lines mean?";
				definitionText.text = "<b>Definition</b>\n\nAn array is a group of values saved in a certain order. In xylocode, arrays are represented by the lines in the canvas. These lines represent a group of sounds.\n\nCan you make the sounds change using the conditional box?";
			} else if (hit.collider.gameObject.GetComponent<Emitter>() != null) {
				definitionText.text = "<b>Definition</b>\n\nFrogs are the “emitter” objects within Xylocode, they are needed to produce sound.";
			}
		}
	}
}
