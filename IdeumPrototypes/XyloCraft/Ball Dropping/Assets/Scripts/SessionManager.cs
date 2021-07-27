using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class SessionManager : MonoBehaviour {
	[DllImport("__Internal")]
	static extern void JSGetSIDQuery(); // in the javascript
	[DllImport("__Internal")]
	static extern string JSGetDate(); // in the javascript

	private string sessionID = "_";
	private GameLog _gameLog = new GameLog();

	// For the table to be created and queried.
	private string _tableName = "Log";

	private CountLogger logger;
	private SOInterpreter soInterpreter;
	public float logIntervalInSeconds = 120f;
	private float nextLogTime = 0f;
	private float currentTime = 0f;

	void Start() {
		sessionID = PlayerPrefs.GetString("SessionID", "_");

		if (sessionID.Equals("_")) {
			sessionID = new RandomString().CreateRandomString();
		}

		PlayerPrefs.SetString("SessionID", sessionID);
		_gameLog.sessionID = sessionID;

		float halfInterval = logIntervalInSeconds/2;
		nextLogTime = Time.time + logIntervalInSeconds + Random.Range(-halfInterval, halfInterval);

		soInterpreter = FindObjectOfType<SOInterpreter>();
		if (soInterpreter == null) {
			soInterpreter = gameObject.AddComponent<SOInterpreter>();
		}


#if !UNITY_EDITOR && UNITY_WEBGL
        JSGetSIDQuery();
#endif
	}

	void Update() {
		if (Time.time >= nextLogTime) {
			soInterpreter.GenerateSaveDataString();
			CallLogSession(soInterpreter.GetTextInput());

			nextLogTime = Time.time + logIntervalInSeconds;
		}

		currentTime = Time.time;
	}

	public string GetSessionID() {
		return sessionID;
	}

	public void SetSessionID(string newID) {
		sessionID = newID;
		_gameLog.sessionID = sessionID;
		PlayerPrefs.SetString("SessionID", sessionID);
	}

	public void SetTimeStamp(string newTime) {
		_gameLog.timeStamp = newTime;
	}

	public void CallLogSession(string gameState) {
		StartCoroutine(LogSession(gameState));
	}

	IEnumerator LogSession(string gameState) {
		_gameLog.timeStamp = System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString();
		_gameLog.gameState = gameState;

		if (logger == null) logger = FindObjectOfType<CountLogger>();
		if (logger != null) _gameLog.trackedStates = logger.GetLog();

		WWWForm form = new WWWForm();
		form.AddField("timeStamp", _gameLog.timeStamp);
		form.AddField("sessionID", _gameLog.sessionID);
		form.AddField("trackedStates", _gameLog.trackedStates);
		form.AddField("gameState", _gameLog.gameState);

		Debug.Log(form);

		WWW www = new WWW("http://localhost:sqlconnect/register.php", form);
		yield return www;
		if (www.text == "0") {
			Debug.Log("Data logged successfully");
		}
		else {
			Debug.Log("Data log failed. Error # " + www.text);
		}
	}

}

[System.Serializable]
public struct GameLog {
	public string sessionID;
	public string timeStamp;
	public string trackedStates;
	public string gameState;
}