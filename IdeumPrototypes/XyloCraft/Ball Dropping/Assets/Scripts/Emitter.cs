using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emitter : MonoBehaviour
{
	public SelectedElementType type = SelectedElementType.Emitter;
	public string id = "";
	public ElemColor color;
	public GameObject ballPrefab;
	public float speed = 5f;
	public string launchKey;

	private Transform firePoint;
	private Animator emitterAnimator;

	private EmitterPanel[] panels;
	private CountLogger logger;

	private float startPosX;
	private float startPosY;
	private bool isBeingHeld = false;
	private bool isBeingRotated = false;
	private float clickTimer;
	private float lastLaunch = 0f;
	private bool justLaunched = false;

	private void Start()
	{
		panels = FindObjectsOfType<EmitterPanel>();
		emitterAnimator = GetComponent<Animator>();
		logger = FindObjectOfType<CountLogger>();

		Transform[] gettingFirePoint = GetComponentsInChildren<Transform>();
		foreach (Transform fire in gettingFirePoint)
		{
			if (fire.gameObject.name == "FirePoint") firePoint = fire;
		}

		if (id == "")
		{
			id = "2" + (int)color;
			RandomString randomstring = new RandomString();
			id += randomstring.CreateRandomString(5);
		}
		else if (!id[0].Equals("2".ToCharArray()[0]))
		{
			id = "2" + id;
		}
	}

	void Update()
	{
		clickTimer += Time.deltaTime;

		if (isBeingHeld)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
		}

		if (isBeingRotated)	Rotate();

		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if (hit.collider != null && hit.collider.CompareTag("Rotator") && hit.collider == gameObject.transform.GetChild(1).GetComponent<Collider2D>())
			{
				isBeingRotated = true;
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			isBeingRotated = false;
		}

		lastLaunch += Time.deltaTime;
		if (!justLaunched && Input.GetAxisRaw(launchKey) > 0.1 && lastLaunch > 0.1)
		{
			lastLaunch = 0f;
			foreach (EmitterPanel panel in panels)
			{
				if (panel.ifType == EmmiterIf.Key)
				{
					PerformCodeBehvaior();
					break;
				}
			}
			justLaunched = true;
		}
		if (!justLaunched && Input.GetAxisRaw("Space") > 0.1 && lastLaunch > 0.1)
		{
			lastLaunch = 0f;
			foreach (EmitterPanel panel in panels)
			{
				if (panel.ifType == EmmiterIf.Space)
				{
					PerformCodeBehvaior();
					break;
				}
			}
			justLaunched = true;
		}
		if (justLaunched && Input.GetAxisRaw(launchKey) + Input.GetAxisRaw("Space") < 0.1)
		{
			lastLaunch = 0f;
			justLaunched = false;
		}

	}

	private void OnMouseDown()
	{
		if (logger != null) logger.emitterClicks++;

		// Drag with left click
		Vector3 mousePos;
		mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		startPosX = mousePos.x - this.transform.localPosition.x;
		startPosY = mousePos.y - this.transform.localPosition.y;

		clickTimer = 0;
		isBeingHeld = true;
	}

	private void OnMouseUp()
	{
		if (clickTimer < 0.15)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if (hit.collider != null && hit.collider == gameObject.GetComponent<Collider2D>())
			{
				foreach (EmitterPanel panel in panels)
				{
					if (panel.ifType == EmmiterIf.Click)
					{
						PerformCodeBehvaior();
						break;
					}
				}
			}
		}
		isBeingHeld = false;
	}

	void PerformCodeBehvaior()
	{
		int repeats = 0;

		foreach (EmitterPanel panel in panels)
		{
			//check if the panel is active and if the colors in panel match colors the emitter
			if (!panel.gameObject.activeInHierarchy ||
				(panel.GetBirdColor() != color && panel.GetBirdColor() != ElemColor.All))
			{
				continue;
			}

			repeats += panel.numberToShoot;

			panel.FlashBox();
		}
		
		if (repeats > 0)
		{
			StartCoroutine(LoopShoot(0.3f, repeats));
		}
	}

	void ShootBall()
	{
		GameObject newBall = FindObjectOfType<GameManager>().AssignBall(ballPrefab);
		newBall.transform.position = firePoint.position;
		newBall.transform.rotation = firePoint.rotation;
		newBall.GetComponent<Ball>().ResetVelocity();

		emitterAnimator.SetTrigger("Shoot");
	}

	void Rotate()
	{
		Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		rotation *= Quaternion.Euler(0, 0, -90);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
	}

	private IEnumerator LoopShoot(float seconds, int numLoops)
	{
		for (int i = 0; i < numLoops; i++)
		{
			ShootBall();
			yield return new WaitForSeconds(seconds);
		}
	}

	public void BecomeCloneOf(GameObject emitterModel)
	{
		color = emitterModel.GetComponent<Emitter>().color;
		ballPrefab = emitterModel.GetComponent<Emitter>().ballPrefab;
		launchKey = emitterModel.GetComponent<Emitter>().launchKey;
		GetComponent<Animator>().runtimeAnimatorController = emitterModel.GetComponent<Animator>().runtimeAnimatorController;
		GetComponent<SpriteRenderer>().sprite = emitterModel.GetComponent<SpriteRenderer>().sprite;

		transform.position = emitterModel.transform.position;
		transform.rotation = emitterModel.transform.rotation;
	}

	public string BirdToSO()
	{
		string SOstring = id;
		SOstring += "," + transform.position.x.ToString("0.00") + "," + transform.position.y.ToString("0.00");
		SOstring += "," + transform.rotation.eulerAngles.z.ToString("0.00");

		return SOstring;
	}

	public void BirdFromSO(string SObird)
	{
		string[] SOstring = SObird.Split(new[] { "," }, System.StringSplitOptions.None);

		id = SOstring[0];
		transform.position = new Vector3(float.Parse(SOstring[1]), float.Parse(SOstring[2]), 0);
		transform.eulerAngles = new Vector3(0, 0, float.Parse(SOstring[3]));
	}
}
