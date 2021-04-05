using System.Collections.Generic;
using UnityEngine;

public class CountLogger : MonoBehaviour {
	private string version = "v0.1";

	//Clicks
	public int emitterPanelClicks;
	public int linePanelClicks;
	public int soundBankClicks;
	public int lineClicks;
	public int emitterClicks;
	public int shapesButtonClicks;
	public int deleteButtonClicks;
	public int helpButtonClicks;
	public int restartButtonClicks;

	//Quantities
	public int totalLinesCreated;
	public int totalEmittersCreated;
	public int totalBallsCreated;

	void Start() {
		string lastLog = Omnipresent.instance?.logContinuation;
		JsonUtility.FromJsonOverwrite(lastLog, this);
	}

	void OnDestroy() {
		if (Omnipresent.instance != null) Omnipresent.instance.logContinuation = JsonUtility.ToJson(this);
	}

	public string GetLog() {
		string output = version;
		output += "," + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		output += "," + Time.time.ToString("0");
		output += "," + emitterPanelClicks;
		output += "," + linePanelClicks;
		output += "," + soundBankClicks;
		output += "," + lineClicks;
		output += "," + emitterClicks;
		output += "," + shapesButtonClicks;
		output += "," + deleteButtonClicks;
		output += "," + helpButtonClicks;
		output += "," + restartButtonClicks;
		output += "," + totalLinesCreated;
		output += "," + totalEmittersCreated;
		output += "," + totalBallsCreated;
		output += "," + PlayerPrefs.GetInt("RhythmTutorialCompleted", 0);
		output += "," + PlayerPrefs.GetInt("InstrumentTutorialCompleted", 0);

		return output;
	}

	public void IncEmitterPanelClicks() {
		emitterPanelClicks++;
	}

	public void IncLinePanelClicks() {
		linePanelClicks++;
	}

	public void IncSoundBankClicks() {
		soundBankClicks++;
	}

	public void IncShapesButtonClicks() {
		shapesButtonClicks++;
	}

	public void IncDeleteButtonClicks() {
		deleteButtonClicks++;
	}

	public void IncHelpButtonClicks() {
		helpButtonClicks++;
	}

	public void IncRestartButtonClicks() {
		restartButtonClicks++;
	}
}
