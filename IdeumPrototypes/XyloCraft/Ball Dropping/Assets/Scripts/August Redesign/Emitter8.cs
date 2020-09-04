using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emitter8 : MonoBehaviour
{
	public SelectedElementType type = SelectedElementType.Emitter;
	public ElemColor color;
	public GameObject ballPrefab;
	public float speed = 5f;
	public string launchKey;

	private Transform firePoint;
	private Animator emitterAnimator;

	private EmitterPanel8[] panels;

	private float startPosX;
	private float startPosY;
	private bool isBeingHeld = false;
	private bool isBeingRotated = false;
	private float clickTimer;
	private float lastLaunch = 0f;
	private bool justLaunched = false;

	private void Start()
	{
		panels = FindObjectsOfType<EmitterPanel8>();
		emitterAnimator = GetComponent<Animator>();

		Transform[] gettingFirePoint = GetComponentsInChildren<Transform>();
		foreach (Transform fire in gettingFirePoint)
		{
			if (fire.gameObject.name == "FirePoint") firePoint = fire;
		}

		//Debug.Log(JsonUtility.ToJson(this));
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
			foreach (EmitterPanel8 panel in panels)
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
			foreach (EmitterPanel8 panel in panels)
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
				foreach (EmitterPanel8 panel in panels)
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

		foreach (EmitterPanel8 panel in panels)
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
		Instantiate(ballPrefab.GetComponent<Ball>().SetSpeed(10), firePoint.position, firePoint.rotation);
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

	public string BirdToSO()
	{
		string SOstring = "2";
		SOstring += (int)color;
		SOstring += "i";
		SOstring += "," + speed;
		SOstring += "," + transform.position.x + "," + transform.position.y + "," + transform.position.z;
		SOstring += "," + transform.rotation.w + "," + transform.rotation.x + "," + transform.rotation.y + "," + transform.rotation.z;

		return SOstring;
	}

	public void BirdFromSO(string SObird)
	{
		string[] SOstring = SObird.Split(new[] { "," }, System.StringSplitOptions.None);

		speed = float.Parse(SOstring[1]);
		Vector3 position = new Vector3
		{
			x = float.Parse(SOstring[2]),
			y = float.Parse(SOstring[3]),
			z = float.Parse(SOstring[4])
		};
		transform.position = position;
		Quaternion rotation = new Quaternion
		{
			w = float.Parse(SOstring[5]),
			x = float.Parse(SOstring[6]),
			y = float.Parse(SOstring[7]),
			z = float.Parse(SOstring[8])
		};
		transform.rotation = rotation;
	}
}
