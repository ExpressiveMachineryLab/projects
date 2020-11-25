using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class SessionManager : MonoBehaviour
{
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
	public float logIntervalInSeconds = 60f;
	private float nextLogTime = 0f;
	private float currentTime = 0f;

	void Start()
	{
		sessionID = PlayerPrefs.GetString("SessionID", "_");

		if (sessionID.Equals("_"))
		{
			sessionID = new RandomString().CreateRandomString();
		}

		PlayerPrefs.SetString("SessionID", sessionID);
		_gameLog.sessionID = sessionID;

		nextLogTime = Time.time + logIntervalInSeconds;

		soInterpreter = FindObjectOfType<SOInterpreter>();
		if (soInterpreter == null)
		{
			soInterpreter = gameObject.AddComponent<SOInterpreter>();
		}

#if !UNITY_EDITOR && UNITY_WEBGL
        JSGetSIDQuery();
#endif
	}

	void Update()
	{
		if (Time.time >= nextLogTime)
		{
			soInterpreter.GenerateSaveDataString();
			LogSession(soInterpreter.GetTextInput());

			nextLogTime = Time.time + logIntervalInSeconds;
		}

		currentTime = Time.time;
	}

	public string GetSessionID()
	{
		return sessionID;
	}

	public void SetSessionID(string newID)
	{
		sessionID = newID;
		_gameLog.sessionID = sessionID;
		PlayerPrefs.SetString("SessionID", sessionID);
	}

	public void SetTimeStamp(string newTime)
	{
		_gameLog.timeStamp = newTime;
	}

	public void LogSession(string gameState)
	{
		_gameLog.timeStamp = System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString();
		_gameLog.gameState = gameState;

		if (logger == null) logger = FindObjectOfType<CountLogger>();
		if (logger != null) _gameLog.trackedStates = logger.GetLog();

		string jsonLog = JsonUtility.ToJson(_gameLog);
		Debug.Log(jsonLog);

		GoogleSheetsForUnity.Drive.CreateObject(jsonLog, _tableName, true);

	}

	public void CreateLogTable()
	{
		Debug.Log("<color=yellow>Creating a table in the cloud for players data.</color>");

		// Creating a string array for field names (table headers) .
		string[] fieldNames = new string[4];
		fieldNames[0] = "sessionID";
		fieldNames[1] = "timeStamp";
		fieldNames[2] = "trackedStates";
		fieldNames[3] = "gameState";

		// Request for the table to be created on the cloud.
		GoogleSheetsForUnity.Drive.CreateTable(fieldNames, _tableName, true);
	}
}

[System.Serializable]
public struct GameLog
{
	public string sessionID;
	public string timeStamp;
	public string trackedStates;
	public string gameState;
}