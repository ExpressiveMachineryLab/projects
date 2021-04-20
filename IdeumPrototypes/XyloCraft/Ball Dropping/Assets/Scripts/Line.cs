using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Line : MonoBehaviour {
	public SelectedElementType type = SelectedElementType.Line;
	public string id = "";
	public ElemColor color;

	public Sprite[] chordSprites; //should always be of length 5
	public Sprite[] chordHitSprites;
	public float speed = 5f;

	public int pitchLevel = 0;
	public int visualLevel = 0;
	public bool pitchPositive = true;
	public bool visualPositive = true;

	private Animator effects;
	private SpriteRenderer lineSprite;

	private LinePanel[] panels;
	private SoundManager soundMan;
	private GameObject playClip;
	private CountLogger logger;

	private float startPosX;
	private float startPosY;
	private bool isBeingHeld = false;
	private bool isBeingRotated = false;

	private bool soudedThisFrame = false;

	private void Start() {
		soundMan = GameObject.Find("GameManager").GetComponent<SoundManager>();
		panels = FindObjectsOfType<LinePanel>();
		effects = GetComponent<Animator>();
		logger = FindObjectOfType<CountLogger>();
		SpriteRenderer[] findLineSprite = GetComponentsInChildren<SpriteRenderer>();

		foreach (SpriteRenderer item in findLineSprite) {
			if (item.gameObject.name == "LineSprite") lineSprite = item;
		}

		lineSprite.sprite = chordSprites[pitchLevel];

		//Create unique ID
		if (id == "") {
			id = "1" + (int)color;
			RandomString randomstring = new RandomString();
			id += randomstring.CreateRandomString(5);
		} else if (!id[0].Equals("1".ToCharArray()[0])) {
			id = "1" + id;
		}
	}

	private void Update() {
		//Draw line with the mouse
		if (isBeingHeld) {
			Vector3 mousePos;
			mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);
			this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX,
				mousePos.y - startPosY, 0);
		}

		if (isBeingRotated) {
			Rotate();
		}

		//Check if the rotator knob is being held by the mouse
		if (Input.GetMouseButtonDown(0)) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if (hit.collider != null && hit.collider.tag == "Rotator" && hit.collider == gameObject.transform.GetChild(1).GetComponent<Collider2D>()) {
				isBeingRotated = true;
			}
		}

		if (Input.GetMouseButtonUp(0)) {
			isBeingRotated = false;
		}

		soudedThisFrame = false;
	}

	void OnEnable() {
		soundMan = GameObject.Find("GameManager").GetComponent<SoundManager>();
	}

	private void OnMouseDown() {
		isBeingHeld = true;

		if (Input.GetMouseButtonDown(0)) {
			if (logger != null) logger.lineClicks++;

			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			startPosX = mousePos.x - this.transform.localPosition.x;
			startPosY = mousePos.y - this.transform.localPosition.y;

			isBeingHeld = true;
		}

	}

	private void OnMouseUp() {
		isBeingHeld = false;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		//if the colliding object has a ball component, PerformCodeBehvaior()
		if (collision.gameObject.GetComponent<Ball>() != null) {
			PerformCodeBehvaior(collision.gameObject.GetComponent<Ball>());
		}
	}

	//Can't see it, don't need it
	private void OnBecameInvisible() {
		gameObject.SetActive(false);
	}

	private void PerformCodeBehvaior(Ball ball) {
		if (soudedThisFrame) return;

		panels = FindObjectsOfType<LinePanel>();

		soudedThisFrame = true;

		int repeats = 0;
		bool playEffect = false;

		foreach (LinePanel panel in panels) {
			//check if the panel is active and if the colors in panel match colors in the line and ball
			if (!panel.gameObject.activeInHierarchy ||
				(panel.GetBallColor() != ball.color && panel.GetBallColor() != ElemColor.any) ||
				(panel.GetLineColor() != color && panel.GetLineColor() != ElemColor.any)) {
				continue;
			}

			//Chord panel
			if (panel.mode == PanelMode.Chord) {
				if (panel.selectedChord == SelectedPM.next) {
					pitchLevel++;
					pitchLevel = pitchLevel % 5;
				} else if (panel.selectedChord == SelectedPM.last) {
					pitchLevel--;
					if (pitchLevel < 0) pitchLevel = 4;
				}
			}

			//Rhythm panel
			if (panel.mode == PanelMode.Rhythm) {
				repeats += panel.selectedRhythm;
			}

			//Visual panel
			if (panel.mode == PanelMode.Visual) {
				playEffect = true;
			}

			panel.FlashBox();
		}

		if (repeats > 0) {
			StartCoroutine(LoopSound(0.2f, repeats));
		} else {
			MakeSound();
		}

		if (playEffect) effects.SetTrigger("Play" + visualLevel);

		StartCoroutine(ChangeSprite(0.15f, ball));
	}

	private void MakeSound() {
		//If we already have an object to play on, assign it to the new sound
		if (playClip != null) {
			soundMan.GetAudio(playClip, color, pitchLevel);
		}
		// if no object exists, create one to send to to sound manager
		else {
			playClip = new GameObject("Dummy");
			playClip.transform.SetParent(transform);
			playClip.AddComponent<AudioSource>();

			Destroy(playClip, 0.1f);

			playClip = soundMan.GetAudio(playClip, color, pitchLevel);
		}
	}

	private void Rotate() {
		Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
	}

	private IEnumerator ChangeSprite(float seconds, Ball ball) {

		Sprite ballOriginalObject = ball.originalSprite;
		Sprite ballHitObject = ball.hitSprite;
		SpriteRenderer collidedObject = ball.gameObject.GetComponent<SpriteRenderer>();

		collidedObject.sprite = ballHitObject;

		lineSprite.sprite = chordHitSprites[pitchLevel];

		yield return new WaitForSeconds(seconds);
		if (collidedObject != null) {
			collidedObject.sprite = ballOriginalObject;
		}

		lineSprite.sprite = chordSprites[pitchLevel];

	}

	private IEnumerator LoopSound(float seconds, int numLoops) {
		for (int i = 0; i < numLoops; i++) {
			MakeSound();
			yield return new WaitForSeconds(seconds);
		}
	}

	private IEnumerator DestroyObject(float seconds) {
		MakeSound();
		yield return new WaitForSeconds(seconds);
		Destroy(this.gameObject);
	}

	//Copy the values of another line, used by the game manager to manage the line object pool
	public void BecomeCloneOf(GameObject lineModel) {
		color = lineModel.GetComponent<Line>().color;
		chordSprites = lineModel.GetComponent<Line>().chordSprites;
		chordHitSprites = lineModel.GetComponent<Line>().chordHitSprites;
		pitchLevel = 0;
		visualLevel = 0;
		pitchPositive = true;
		visualPositive = true;
		GetComponent<Animator>().runtimeAnimatorController = lineModel.GetComponent<Animator>().runtimeAnimatorController;
		transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = lineModel.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite;
		transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = lineModel.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite;

		transform.position = lineModel.transform.position;
		transform.rotation = lineModel.transform.rotation;
	}

	//Create a string to encapulate the line's properties
	public string LineToSO() {
		string SOstring = id;
		SOstring += "," + transform.position.x.ToString("0.00") + "," + transform.position.y.ToString("0.00");
		SOstring += "," + transform.rotation.eulerAngles.z.ToString("0.00");
		SOstring += "," + pitchLevel;
		SOstring += visualLevel;
		SOstring += (pitchPositive ? "1" : "0");
		SOstring += (visualPositive ? "1" : "0");

		return SOstring;
	}

	//Assign properties from a string
	public void LineFromSO(string SOline) {
		string[] SOstring = SOline.Split(new[] { "," }, System.StringSplitOptions.None);

		id = SOstring[0];
		transform.position = new Vector3(float.Parse(SOstring[1]), float.Parse(SOstring[2]), 0);
		transform.eulerAngles = new Vector3(0, 0, float.Parse(SOstring[3]));
		pitchLevel = int.Parse(SOstring[4][0].ToString());
		visualLevel = int.Parse(SOstring[4][1].ToString());
		pitchPositive = int.Parse(SOstring[4][2].ToString()) == 1 ? true : false;
		visualPositive = int.Parse(SOstring[4][3].ToString()) == 1 ? true : false;
	}
}