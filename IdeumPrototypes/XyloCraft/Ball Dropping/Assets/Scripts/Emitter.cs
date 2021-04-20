using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emitter : MonoBehaviour {
	public SelectedElementType type = SelectedElementType.Emitter;
	public string id = "";
	public ElemColor color;
	public GameObject ballPrefab;
	public float speed = 5f;
	public string launchKey;

	private Transform firePoint;
	private Animator emitterAnimator;
	
	private SoundManager soundMan;
	private CountLogger logger;

	private float startPosX;
	private float startPosY;
	private bool isBeingHeld = false;
	private bool isBeingRotated = false;
	private float clickTimer;

	void Start() {
		soundMan = GameObject.Find("GameManager").GetComponent<SoundManager>();
		emitterAnimator = GetComponent<Animator>();
		logger = FindObjectOfType<CountLogger>();

		Transform[] gettingFirePoint = GetComponentsInChildren<Transform>();
		foreach (Transform fire in gettingFirePoint) {
			if (fire.gameObject.name == "FirePoint") firePoint = fire;
		}

		//Create a unique ID
		if (id == "") {
			id = "2" + (int)color;
			RandomString randomstring = new RandomString();
			id += randomstring.CreateRandomString(5);
		} else if (!id[0].Equals("2".ToCharArray()[0])) {
			id = "2" + id;
		}
	}

	void Update() {
		clickTimer += Time.deltaTime;

		if (isBeingHeld) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
		}

		if (isBeingRotated) Rotate();

		if (Input.GetMouseButtonDown(0)) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if (hit.collider != null && hit.collider.CompareTag("Rotator") && hit.collider == gameObject.transform.GetChild(1).GetComponent<Collider2D>()) {
				isBeingRotated = true;
			}
		}

		if (Input.GetMouseButtonUp(0)) {
			isBeingRotated = false;
		}

	}

	void OnEnable() {
		soundMan = GameObject.Find("GameManager").GetComponent<SoundManager>();
	}

	void OnMouseDown() {
		if (logger != null) logger.emitterClicks++;

		// Drag with left click
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		startPosX = mousePos.x - this.transform.localPosition.x;
		startPosY = mousePos.y - this.transform.localPosition.y;

		clickTimer = 0;
		isBeingHeld = true;
	}

	void OnMouseUp() {
		if (clickTimer < 0.15) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if (hit.collider != null && hit.collider == gameObject.GetComponent<Collider2D>()) {
				ShootBall();
			}
		}
		isBeingHeld = false;
	}

	public void ShootBall() {
		GameObject newBall = FindObjectOfType<GameManager>().AssignBall(ballPrefab);
		newBall.transform.position = firePoint.position;
		newBall.transform.rotation = firePoint.rotation;
		newBall.GetComponent<Ball>().ResetVelocity();

		emitterAnimator.SetTrigger("Shoot");
	}

	private void Rotate() {
		Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		rotation *= Quaternion.Euler(0, 0, -90);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
	}

	public void BecomeCloneOf(GameObject emitterModel) {
		color = emitterModel.GetComponent<Emitter>().color;
		ballPrefab = emitterModel.GetComponent<Emitter>().ballPrefab;
		launchKey = emitterModel.GetComponent<Emitter>().launchKey;
		GetComponent<Animator>().runtimeAnimatorController = emitterModel.GetComponent<Animator>().runtimeAnimatorController;
		GetComponent<SpriteRenderer>().sprite = emitterModel.GetComponent<SpriteRenderer>().sprite;

		transform.position = emitterModel.transform.position;
		transform.rotation = emitterModel.transform.rotation;
	}

	public string BirdToSO() {
		string SOstring = id;
		SOstring += "," + transform.position.x.ToString("0.00") + "," + transform.position.y.ToString("0.00");
		SOstring += "," + transform.rotation.eulerAngles.z.ToString("0.00");

		return SOstring;
	}

	public void BirdFromSO(string SObird) {
		string[] SOstring = SObird.Split(new[] { "," }, System.StringSplitOptions.None);

		id = SOstring[0];
		transform.position = new Vector3(float.Parse(SOstring[1]), float.Parse(SOstring[2]), 0);
		transform.eulerAngles = new Vector3(0, 0, float.Parse(SOstring[3]));
	}
}
