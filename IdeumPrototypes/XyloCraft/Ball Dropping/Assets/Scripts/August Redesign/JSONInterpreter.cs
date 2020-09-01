using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JSONInterpreter : MonoBehaviour
{
	public string textInput;
	public InputField textField;

	public GameObject emitterRedPrefab;
	public GameObject emitterYellowPrefab;
	public GameObject emitterBluePrefab;
	public GameObject emitterGreenPrefab;

	public GameObject lineRedPrefab;
	public GameObject lineYellowPrefab;
	public GameObject lineBluedPrefab;
	public GameObject lineGreenPrefab;

	public GameObject ballRedPrefab;
	public GameObject ballYellowPrefab;
	public GameObject ballBluePrefab;
	public GameObject ballGreenPrefab;

	public void CopyToField()
	{
		textField.text = textInput;
	}

	public void CopyFromField()
	{
		textInput = textField.text;
	}

	public void GenerateJSON()
	{
		Emitter8[] emitters = FindObjectsOfType<Emitter8>();
		Line8[] lines = FindObjectsOfType<Line8>();
		Ball[] balls = FindObjectsOfType<Ball>();

		textInput = "";

		foreach (Emitter8 emitter in emitters)
		{
			textInput += emitter.EmitterToJSON() + "##";
		}

		foreach (Line8 line in lines)
		{
			textInput += line.LineToJSON() + "##";
		}

		foreach (Ball ball in balls)
		{
			textInput += ball.BallToJSON() + "##";
		}

		textInput = textInput.Substring(0, textInput.Length - 2);
	}

	public void ParseJSON()
	{
		string[] contents = textInput.Split(new[] { "##" }, System.StringSplitOptions.RemoveEmptyEntries);

		foreach (string item in contents)
		{
			switch (int.Parse(item[8].ToString()))
			{
				case 2:
					GameObject newEmitter = null;
					if (int.Parse(item[18].ToString()) == 0) newEmitter = Instantiate(emitterRedPrefab);
					if (int.Parse(item[18].ToString()) == 1) newEmitter = Instantiate(emitterYellowPrefab);
					if (int.Parse(item[18].ToString()) == 2) newEmitter = Instantiate(emitterBluePrefab);
					if (int.Parse(item[18].ToString()) == 3) newEmitter = Instantiate(emitterGreenPrefab);
					newEmitter.GetComponent<Emitter8>().EmitterFromJSON(item);
					break;
				case 1:
					GameObject newLine = null;
					if (int.Parse(item[18].ToString()) == 0) newLine = Instantiate(lineRedPrefab);
					if (int.Parse(item[18].ToString()) == 1) newLine = Instantiate(lineYellowPrefab);
					if (int.Parse(item[18].ToString()) == 2) newLine = Instantiate(lineBluedPrefab);
					if (int.Parse(item[18].ToString()) == 3) newLine = Instantiate(lineGreenPrefab);
					newLine.GetComponent<Line8>().Init();
					newLine.GetComponent<Line8>().LineFromJSON(item);
					break;
				case 0:
					GameObject newBall = null;
					if (int.Parse(item[18].ToString()) == 0) newBall = Instantiate(ballRedPrefab);
					if (int.Parse(item[18].ToString()) == 1) newBall = Instantiate(ballYellowPrefab);
					if (int.Parse(item[18].ToString()) == 2) newBall = Instantiate(ballBluePrefab);
					if (int.Parse(item[18].ToString()) == 3) newBall = Instantiate(ballGreenPrefab);
					newBall.GetComponent<Ball>().BallFromJSON(item);
					break;
			}
		}
	}
}
