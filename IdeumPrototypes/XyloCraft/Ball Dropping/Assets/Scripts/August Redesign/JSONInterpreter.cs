using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class JSONInterpreter : MonoBehaviour
{
	[DllImport("__Internal")]
	static extern void JSInitialize(); // in the javascript

	public string textInput;
	public InputField textField;

	public GameObject[] emitterPrefabs;
	public GameObject[] linePrefabs;
	public GameObject[] ballPrefabs;

	private void Start()
	{
		Debug.Log("scene is ready");

		StartCoroutine(LateStart());
	}

	public void CopyToField()
	{
		textField.text = textInput;
		textInput.CopyToClipboard();
	}

	public void CopyFromField()
	{
		textInput = textField.text;
		textInput.CopyToClipboard();
	}

	public void SetTextInput(string newInput)
	{
		textInput = newInput;
	}

	public string GetTextInput()
	{
		return textInput;
	}

	public void GenerateSaveDataString()
	{
		textInput = "";

		SoundManager soundMan = FindObjectOfType<SoundManager>();
		textInput += soundMan.SoundManagerToSO() + "_";

		EmitterPanel8[] emitterPanels = FindObjectsOfType<EmitterPanel8>();
		foreach (EmitterPanel8 emitterPanel in emitterPanels)
		{
			textInput += emitterPanel.EmitterPanelToSO() + "_";
		}
		
		LinePanel8[] linePanels = FindObjectsOfType<LinePanel8>();
		foreach (LinePanel8 linePanel in linePanels)
		{
			textInput += linePanel.LinePanelToSO() + "_";
		}

		Emitter8[] emitters = FindObjectsOfType<Emitter8>();
		foreach (Emitter8 emitter in emitters)
		{
			textInput += emitter.BirdToSO() + "_";
		}
		
		Line8[] lines = FindObjectsOfType<Line8>();
		foreach (Line8 line in lines)
		{
			textInput += line.LineToSO() + "_";
		}

		Ball[] balls = FindObjectsOfType<Ball>();
		foreach (Ball ball in balls)
		{
			textInput += ball.BallToSO() + "_";
		}

		textInput = textInput.Substring(0, textInput.Length - 1);
	}

	public void ParseSaveDataString()
	{
		string[] contentsArray = textInput.Split(new[] { "_" }, System.StringSplitOptions.None);
		EmitterPanel8[] emitterPanels = FindObjectsOfType<EmitterPanel8>();
		LinePanel8[] linePanels = FindObjectsOfType<LinePanel8>();

		foreach (string item in contentsArray)
		{
			if (int.Parse(item[0].ToString()) == 0)
			{
				GameObject newBall = Instantiate(ballPrefabs[int.Parse(item[1].ToString())]);
				newBall.GetComponent<Ball>().BallFromSO(item);
			}
			else if (int.Parse(item[0].ToString()) == 1)
			{
				GameObject newLine = Instantiate(linePrefabs[int.Parse(item[1].ToString())]);
				newLine.GetComponent<Line8>().LineFromSO(item);
				newLine.transform.GetChild(0).gameObject.SetActive(false);
				newLine.transform.GetChild(1).gameObject.SetActive(false);
			}
			else if (int.Parse(item[0].ToString()) == 2)
			{
				GameObject newBird = Instantiate(emitterPrefabs[int.Parse(item[1].ToString())]);
				newBird.GetComponent<Emitter8>().BirdFromSO(item);
				newBird.transform.GetChild(0).gameObject.SetActive(false);
				newBird.transform.GetChild(1).gameObject.SetActive(false);
			}
			else if (int.Parse(item[0].ToString()) == 3)
			{
				string[] SOstring = item.Split(new[] { "," }, System.StringSplitOptions.None);
				foreach (LinePanel8 linePanel in linePanels)
				{
					if (SOstring[0] == linePanel.id)
					{
						linePanel.LinePanelFromSO(item);
					}
				}
			}
			else if (int.Parse(item[0].ToString()) == 4)
			{
				string[] SOstring = item.Split(new[] { "," }, System.StringSplitOptions.None);
				foreach (EmitterPanel8 emitterPanel in emitterPanels)
				{
					if (SOstring[0] == emitterPanel.id)
					{
						emitterPanel.EmitterPanelFromSO(item);
					}
				}

			}
			else if (int.Parse(item[0].ToString()) == 5)
			{
				FindObjectOfType<SoundManager>().SoundManagerFromSO(item);
			}
		}


	}

	public void GetSharableLink()
	{
		GenerateSaveDataString();

		string[] useURL = Application.absoluteURL.Split(new[] { "?" }, System.StringSplitOptions.None);

		textField.text = useURL[0] + "?game=" + textInput;
		textInput.CopyToClipboard();
	}

	IEnumerator LateStart()
	{
		yield return new WaitForEndOfFrame();
#if !UNITY_EDITOR && UNITY_WEBGL
        JSInitialize();
#endif
	}
}

public class RandomString
{
	public string CreateRandomString(int stringLength = 10)
	{
		int _stringLength = stringLength - 1;
		string randomString = "";
		string[] characters = new string[] { "a", "b", "c", "d", "e",
											 "f", "g", "h", "i", "j",
											 "k", "l", "m", "n", "o",
											 "p", "q", "r", "s", "t",
											 "u", "v", "w", "x", "y",
											 "z", "A", "B", "C", "D",
											 "E", "F", "G", "H", "I",
											 "J", "K", "L", "M", "N",
											 "O", "P", "Q", "R", "S",
											 "T", "U", "V", "W", "X",
											 "Y", "Z", "1", "2", "3",
											 "4", "5", "6", "7", "8",
											 "9", "0"};
		for (int i = 0; i <= _stringLength; i++)
		{
			randomString = randomString + characters[Random.Range(0, characters.Length)];
		}
		return randomString;
	}
}

public static class ClipboardExtension
{
	/// <summary>
	/// Puts the string into the Clipboard.
	/// </summary>
	public static void CopyToClipboard(this string str)
	{
		GUIUtility.systemCopyBuffer = str;
	}
}