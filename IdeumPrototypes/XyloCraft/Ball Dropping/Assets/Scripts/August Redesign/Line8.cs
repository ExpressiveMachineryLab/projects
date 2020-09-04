using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Line8 : MonoBehaviour
{
	public SelectedElementType type = SelectedElementType.Line;
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

	private LinePanel8[] panels;
	private SoundManager soundMan;
	private GameObject playClip;

	private float startPosX;
	private float startPosY;
	private bool isBeingHeld = false;
	private bool isBeingRotated = false;
	

	private void Start()
	{
		soundMan = GameObject.Find("GameManager").GetComponent<SoundManager>();
		panels = FindObjectsOfType<LinePanel8>();
		effects = GetComponent<Animator>();
		SpriteRenderer[] findLineSprite = GetComponentsInChildren<SpriteRenderer>();

		foreach (SpriteRenderer item in findLineSprite)
		{
			if (item.gameObject.name == "LineSprite") lineSprite = item;
		}

		lineSprite.sprite = chordSprites[pitchLevel];

		//Debug.Log(JsonUtility.ToJson(this));
	}

	private void Update()
	{
		if (isBeingHeld)
		{
			Vector3 mousePos;
			mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);
			this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX,
				mousePos.y - startPosY, 0);
		}

		if (isBeingRotated)
		{
			Rotate();
		}

		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if (hit.collider != null && hit.collider.tag == "Rotator" && hit.collider == gameObject.transform.GetChild(1).GetComponent<Collider2D>())
			{
				isBeingRotated = true;
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			isBeingRotated = false;
		}
	}

	private void OnMouseDown()
	{
		isBeingHeld = true;

		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos;
			mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);

			startPosX = mousePos.x - this.transform.localPosition.x;
			startPosY = mousePos.y - this.transform.localPosition.y;

			isBeingHeld = true;
		}


	}

	private void OnMouseUp()
	{
		isBeingHeld = false;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		//if the colliding object has a ball component, PerformCodeBehvaior()
		if (collision.gameObject.GetComponent<Ball>() != null)
		{
			PerformCodeBehvaior(collision.gameObject.GetComponent<Ball>());
		}
	}

	private void OnBecameInvisible()
	{
		Destroy(this.gameObject);
	}

	private void PerformCodeBehvaior(Ball ball)
	{
		int repeats = 0;

		foreach (LinePanel8 panel in panels)
		{
			//check if the panel is active and if the colors in panel match colors in the line and ball
			if (!panel.gameObject.activeInHierarchy ||
				(panel.GetBallColor() != ball.color && panel.GetBallColor() != ElemColor.All) ||
				(panel.GetLineColor() != color && panel.GetLineColor() != ElemColor.All))
			{
				continue;
			}

			//Chord panel
			if (panel.mode == PanelMode.Chord)
			{
				if (panel.selectedChord == SelectedPM.Plus)
				{
					pitchLevel++;
					pitchLevel = pitchLevel % 5;
				}
				else if (panel.selectedChord == SelectedPM.Minus)
				{
					pitchLevel--;
					if (pitchLevel < 0) pitchLevel = 4;
				}
				else if (panel.selectedChord == SelectedPM.PlusMinus)
				{
					if (pitchLevel == 4)
					{
						pitchPositive = false;
					}
					else if (pitchLevel == 0)
					{
						pitchPositive = true;
					}

					if (pitchPositive)
					{
						pitchLevel++;
					}
					else
					{
						pitchLevel--;
					}
				}
			}

			//Rhythm panel
			if (panel.mode == PanelMode.Rhythm)
			{
				repeats += panel.selectedRhythm;
			}

			//Visual panel
			if (panel.mode == PanelMode.Visual)
			{
				if (panel.selectedVisual == SelectedPM.Plus)
				{
					visualLevel = visualLevel++ % 4;
				}
				else if (panel.selectedVisual == SelectedPM.Minus)
				{
					visualLevel = visualLevel-- % 4;
				}
				else if (panel.selectedVisual == SelectedPM.PlusMinus)
				{
					if (visualLevel == 3)
					{
						visualPositive = false;
					}
					else if (visualLevel == 0)
					{
						visualPositive = true;
					}

					if (visualPositive)
					{
						visualLevel++;
					}
					else
					{
						visualLevel--;
					}
				}

				effects.SetTrigger("Play" + visualLevel);
			}
			
			panel.FlashBox();
		}

		if (repeats > 0)
		{
			StartCoroutine(LoopSound(0.2f, repeats));
		}
		else
		{
			MakeSound();
		}

		StartCoroutine(ChangeSprite(0.15f, ball));
	}

	private void MakeSound()
	{
		if (playClip != null)
		{
			soundMan.GetAudio(playClip, color, pitchLevel);
		}
		else
		{
			playClip = new GameObject("Dummy");
			playClip.transform.SetParent(transform);
			AudioSource sound = playClip.AddComponent<AudioSource>();

			Destroy(playClip, 0.1f);

			playClip = soundMan.GetAudio(playClip, color, pitchLevel);
		}
	}

	private void Rotate()
	{
		Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
	}

	private IEnumerator ChangeSprite(float seconds, Ball ball)
	{

		Sprite ballOriginalObject = ball.originalSprite;
		Sprite ballHitObject = ball.hitSprite;
		SpriteRenderer collidedObject = ball.gameObject.GetComponent<SpriteRenderer>();

		collidedObject.sprite = ballHitObject;

		lineSprite.sprite = chordHitSprites[pitchLevel];

		yield return new WaitForSeconds(seconds);
		if (collidedObject != null)
		{
			collidedObject.sprite = ballOriginalObject;
		}

		lineSprite.sprite = chordSprites[pitchLevel];

	}

	private IEnumerator LoopSound(float seconds, int numLoops)
	{
		for (int i = 0; i < numLoops; i++)
		{
			MakeSound();
			yield return new WaitForSeconds(seconds);
		}
	}

	private IEnumerator DestroyObject(float seconds)
	{
		MakeSound();
		yield return new WaitForSeconds(seconds);
		Destroy(this.gameObject);
	}

	public string LineToSO()
	{
		string SOstring = "1";
		SOstring += (int)color;
		SOstring += "i";
		SOstring += "," + speed;
		SOstring += "," + transform.position.x + "," + transform.position.y + "," + transform.position.z;
		SOstring += "," + transform.rotation.w + "," + transform.rotation.x + "," + transform.rotation.y + "," + transform.rotation.z;
		SOstring += "," + pitchLevel;
		SOstring += "," + visualLevel;
		SOstring += "," + (pitchPositive ? "1" : "0");
		SOstring += "," + (visualPositive ? "1" : "0");

		return SOstring;
	}

	public void LineFromSO(string SOline)
	{
		string[] SOstring = SOline.Split(new[] { "," }, System.StringSplitOptions.None);

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
		pitchLevel = int.Parse(SOstring[9]);
		visualLevel = int.Parse(SOstring[10]);
		pitchPositive = int.Parse(SOstring[11]) == 1 ? true: false;
		visualPositive = int.Parse(SOstring[12]) == 1 ? true : false;
	}
}