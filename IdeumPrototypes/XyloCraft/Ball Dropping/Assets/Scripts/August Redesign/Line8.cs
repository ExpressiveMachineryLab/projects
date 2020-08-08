using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Line8 : MonoBehaviour
{
	public ElemColor color;

	public Sprite[] chordSprites; //should always be of length 5
	public Sprite[] chordHitSprites;
	public float speed = 5f;

	public Animator effects;
	public SpriteRenderer lineSprite;

	private AudioSource playClip;
	private CodePanel8[] panels;
	private SoundManager soundMan;

	private float startPosX;
	private float startPosY;
	private bool isBeingHeld = false;
	private bool isBeingRotated = false;

	private int pitchLevel = 0;
	private int visualLevel = 0;
	private bool pitchPositive = true;
	private bool visualPositive = true;
	

	private void Start()
	{
		playClip = GetComponent<AudioSource>();
		soundMan = GameObject.Find("GameManager").GetComponent<SoundManager>();
		panels = FindObjectsOfType<CodePanel8>();
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

		foreach (CodePanel8 panel in panels)
		{
			//check if the panel is active and if the colors in panel match colors in the line and ball
			if (!panel.gameObject.activeInHierarchy ||
				(panel.lineColor != color && panel.lineColor != ElemColor.All) ||
				(panel.ballColor != ball.color && panel.ballColor != ElemColor.All))
			{
				continue;
			}

			//Chord panel
			if (panel.mode == PanelMode.Chord)
			{
				if (panel.selectedChord == SelectedPM.Plus)
				{
					pitchLevel = pitchLevel++ % 5;
				}
				else if (panel.selectedChord == SelectedPM.Minus)
				{
					pitchLevel = pitchLevel-- % 5;
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
		soundMan.GetAudio(playClip, color, pitchLevel);
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
}

public enum ElemColor
{
	Red,
	Yellow,
	Blue,
	Green,
	All
}