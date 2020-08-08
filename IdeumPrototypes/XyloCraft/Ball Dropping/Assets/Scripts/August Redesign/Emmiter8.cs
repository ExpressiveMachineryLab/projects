using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Emitter Class:
// Allows emitter to be dragged

public class Emitter8 : MonoBehaviour
{
	public ElemColor color;
	public Transform firePoint;
	public GameObject ballPrefab;
	public float speed = 5f;
	public Animator emitterAnimator;
	public string launchKey;
	
	private float startPosX;
	private float startPosY;
	private bool isBeingHeld = false;
	private bool isBeingRotated = false;
	private float clickTimer;
	private bool clickTimerOn;
	private float lastLaunch = 0f;
	private bool justLaunched = false;
	
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
			ShootBall();
			justLaunched = true;
		}
		if (justLaunched && Input.GetAxisRaw(launchKey) < 0.1)
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
		clickTimerOn = true;
		isBeingHeld = true;
	}

	private void OnMouseUp()
	{
		clickTimerOn = false;
		if (clickTimer < 0.15)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if (hit.collider != null && hit.collider == gameObject.GetComponent<Collider2D>())
				ShootBall();
		}
		isBeingHeld = false;
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
}
