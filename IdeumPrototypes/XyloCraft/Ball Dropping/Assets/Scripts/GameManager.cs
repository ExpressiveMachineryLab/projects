using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	private SelectionManager selectionManager;
    private float speed = 2.5f;
	private TMP_InputField speedMultiplier;

	public int maxLines, maxEmitters, maxBalls;
	private GameObjectAgePair[] linePool, emitterPool, ballPool;
	
	public GameObject linePrefabs, emitterPrefabs, ballPrefabs;

	private CountLogger logger;
	
	void Start()
    {
        speedMultiplier = GameObject.Find("GlobalSpeedMultiplier").GetComponent<TMP_InputField>();
		UpdateSpeed();

        selectionManager = GameObject.Find("SelectedObject").GetComponent<SelectionManager>();

		logger = FindObjectOfType<CountLogger>();

		linePool = new GameObjectAgePair[maxLines];
		for (int i = 0; i < linePool.Length; i++)
		{
			linePool[i] = new GameObjectAgePair();
			linePool[i].myObject = Instantiate(linePrefabs);
			linePool[i].myObject.SetActive(false);
			linePool[i].myObject.name = "Line" + i;
			linePool[i].age = 0f;
		}
		emitterPool = new GameObjectAgePair[maxEmitters];
		for (int i = 0; i < emitterPool.Length; i++)
		{
			emitterPool[i] = new GameObjectAgePair();
			emitterPool[i].myObject = Instantiate(emitterPrefabs);
			emitterPool[i].myObject.SetActive(false);
			emitterPool[i].myObject.name = "Emitter" + i;
			emitterPool[i].age = 0f;
		}
		ballPool = new GameObjectAgePair[maxBalls];
		for (int i = 0; i < ballPool.Length; i++)
		{
			ballPool[i] = new GameObjectAgePair();
			ballPool[i].myObject = Instantiate(ballPrefabs);
			ballPool[i].myObject.SetActive(false);
			ballPool[i].myObject.name = "Ball" + i;
			ballPool[i].age = 0f;
		}

	}
	
    public void UpdateSpeed()
	{
		if (speedMultiplier.text == "") return;
		float checkSpeed = float.Parse(speedMultiplier.text) * 2;
		if (checkSpeed < 0.2f) checkSpeed = 0.2f;
		speed = checkSpeed;
    }

	public float GetSpeedMultiplier()
	{
		return speed;
	}

	public void SetSpeedMultiplier(float newSpeed)
	{
		speed = newSpeed;
		speedMultiplier.text = newSpeed.ToString();
	}

    public void OnClickDelete()
    {
        selectionManager.DeleteSelection();
    }

    public void ResetApplication()
    {
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		LinePanel[] linePanels = FindObjectsOfType<LinePanel>();
		EmitterPanel[] emitterPanels = FindObjectsOfType<EmitterPanel>();

		foreach (GameObjectAgePair pair in linePool)
		{
			pair.myObject.gameObject.SetActive(false);
		}
		foreach (GameObjectAgePair pair in emitterPool)
		{
			pair.myObject.gameObject.SetActive(false);
		}
		foreach (GameObjectAgePair pair in ballPool)
		{
			pair.myObject.gameObject.SetActive(false);
		}
		foreach (LinePanel panel in linePanels)
		{
			panel.ResetPanel();
		}
		foreach (EmitterPanel panel in emitterPanels)
		{
			panel.ResetPanel();
		}
		SetSpeedMultiplier(1f);
	}

	public GameObject AssignLine(GameObject lineModel)
	{
		if (logger != null) logger.totalLinesCreated++;

		float lowestAge = Time.time;
		int index = 0;
		GameObjectAgePair myObject = null;

		for (int i = 0; i < linePool.Length; i++)
		{
			if (linePool[i] == null)
			{
				linePool[i] = new GameObjectAgePair();
				linePool[i].myObject = Instantiate(linePrefabs);
				linePool[i].age = 0f;
				linePool[i].myObject.SetActive(false);
				linePool[i].myObject.name = "Line" + i;
			}
			if (!linePool[i].myObject.activeSelf)
			{
				myObject = linePool[i];
				index = i;
				break;
			}
			if (linePool[i].age < lowestAge)
			{
				lowestAge = linePool[i].age;
				index = i;
			}
		}

		if (myObject == null)
		{
			myObject = linePool[index];
		}

		myObject.myObject.name = lineModel.name + index;
		myObject.age = Time.time;

		myObject.myObject.SetActive(true);
		myObject.myObject.GetComponent<Line>().BecomeCloneOf(lineModel);

		return myObject.myObject;
	}

	public GameObject AssignEmitter(GameObject emitterModel)
	{
		if (logger != null) logger.totalEmittersCreated++;

		float lowestAge = Time.time;
		int index = 0;
		GameObjectAgePair myObject = null;

		for (int i = 0; i < emitterPool.Length; i++)
		{
			if (emitterPool[i] == null)
			{
				emitterPool[i] = new GameObjectAgePair();
				emitterPool[i].myObject = Instantiate(emitterPrefabs);
				emitterPool[i].age = 0f;
				emitterPool[i].myObject.SetActive(false);
				emitterPool[i].myObject.name = "Emitter" + i;
			}
			if (!emitterPool[i].myObject.activeSelf)
			{
				myObject = emitterPool[i];
				index = i;
				break;
			}
			if (emitterPool[i].age < lowestAge)
			{
				lowestAge = emitterPool[i].age;
				index = i;
			}
		}

		if (myObject == null)
		{
			myObject = emitterPool[index];
		}

		myObject.myObject.name = emitterModel.name + index;
		myObject.age = Time.time;

		myObject.myObject.SetActive(true);
		myObject.myObject.GetComponent<Emitter>().BecomeCloneOf(emitterModel);

		return myObject.myObject;
	}

	public GameObject AssignBall(GameObject ballModel)
	{
		if (logger != null) logger.totalBallsCreated++;

		float lowestAge = Time.time;
		int index = 0;
		GameObjectAgePair myObject = null;

		for (int i = 0; i < ballPool.Length; i++)
		{
			if (ballPool[i] == null)
			{
				ballPool[i] = new GameObjectAgePair();
				ballPool[i].myObject = Instantiate(ballPrefabs);
				ballPool[i].age = Time.time;
				ballPool[i].myObject.SetActive(false);
				ballPool[i].myObject.name = "Ball" + i;
			}
			if (!ballPool[i].myObject.activeSelf)
			{
				myObject = ballPool[i];
				index = i;
				break;
			}
			if (ballPool[i].age < lowestAge)
			{
				lowestAge = ballPool[i].age;
				index = i;
			}
		}

		if (myObject == null)
		{
			myObject = ballPool[index];
		}

		myObject.myObject.name = ballModel.name + index;
		myObject.age = Time.time;

		myObject.myObject.SetActive(true);
		myObject.myObject.GetComponent<Ball>().BecomeCloneOf(ballModel);

		return myObject.myObject;
	}

	public void ReplaceLine(GameObject lineReplacement)
	{
		if (logger != null) logger.totalLinesCreated++;

		float lowestAge = Time.time;
		int index = 0;
		GameObjectAgePair myObject = null;

		for (int i = 0; i < linePool.Length; i++)
		{
			if (linePool[i] == null)
			{
				linePool[i] = new GameObjectAgePair();
				linePool[i].myObject = Instantiate(linePrefabs);
				linePool[i].age = 0f;
				linePool[i].myObject.SetActive(false);
				linePool[i].myObject.name = "Line" + i;
			}
			if (!linePool[i].myObject.activeSelf)
			{
				myObject = linePool[i];
				index = i;
				break;
			}
			if (linePool[i].age < lowestAge)
			{
				lowestAge = linePool[i].age;
				index = i;
			}
		}

		if (myObject == null)
		{
			myObject = linePool[index];
		}

		Destroy(myObject.myObject.gameObject);
		myObject.myObject = lineReplacement;

		myObject.myObject.name = lineReplacement.name + index;
		myObject.age = Time.time;

		myObject.myObject.SetActive(true);

	}

	public void ReplaceEmitter(GameObject emitterReplacement)
	{
		if (logger != null) logger.totalEmittersCreated++;

		float lowestAge = Time.time;
		int index = 0;
		GameObjectAgePair myObject = null;

		for (int i = 0; i < emitterPool.Length; i++)
		{
			if (emitterPool[i] == null)
			{
				emitterPool[i] = new GameObjectAgePair();
				emitterPool[i].myObject = Instantiate(emitterPrefabs);
				emitterPool[i].age = 0f;
				emitterPool[i].myObject.SetActive(false);
				emitterPool[i].myObject.name = "Emitter" + i;
			}
			if (!emitterPool[i].myObject.activeSelf)
			{
				myObject = emitterPool[i];
				index = i;
				break;
			}
			if (emitterPool[i].age < lowestAge)
			{
				lowestAge = emitterPool[i].age;
				index = i;
			}
		}

		if (myObject == null)
		{
			myObject = emitterPool[index];
		}

		Destroy(myObject.myObject.gameObject);
		myObject.myObject = emitterReplacement;

		myObject.myObject.name = emitterReplacement.name + index;
		myObject.age = Time.time;

		myObject.myObject.SetActive(true);

	}

	public void ReplaceBall(GameObject ballReplacement)
	{
		if (logger != null) logger.totalBallsCreated++;

		float lowestAge = Time.time;
		int index = 0;
		GameObjectAgePair myObject = null;

		for (int i = 0; i < ballPool.Length; i++)
		{
			if (ballPool[i] == null)
			{
				ballPool[i] = new GameObjectAgePair();
				ballPool[i].myObject = Instantiate(ballPrefabs);
				ballPool[i].age = 0f;
				ballPool[i].myObject.SetActive(false);
				ballPool[i].myObject.name = "Ball" + i;
			}
			if (!ballPool[i].myObject.activeSelf)
			{
				myObject = ballPool[i];
				index = i;
				break;
			}
			if (ballPool[i].age < lowestAge)
			{
				lowestAge = ballPool[i].age;
				index = i;
			}
		}

		if (myObject == null)
		{
			myObject = ballPool[index];
		}

		Destroy(myObject.myObject.gameObject);
		myObject.myObject = ballReplacement;

		myObject.myObject.name = ballReplacement.name + index;
		myObject.age = Time.time;

		myObject.myObject.SetActive(true);

	}

}

[System.Serializable]
public class GameObjectAgePair
{
	public GameObject myObject;
	public float age;
}