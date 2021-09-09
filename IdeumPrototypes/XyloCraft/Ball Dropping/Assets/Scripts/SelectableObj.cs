using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectableObj : MonoBehaviour
{
	protected float startPosX;
	protected float startPosY;
	protected bool isBeingHeld = false;
	protected bool isBeingRotated = false;
	protected float clickTimer;
	public float speed = 5f;

	protected void SelectUpdate()
    {
		bool inSquareSelect = !(this.transform.parent == null || this.transform.parent.tag != "SelectionParent");
		if (isBeingHeld)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (!inSquareSelect)
			{
				transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
			}
			else
			{
				this.transform.parent.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
			}
		}

		if (isBeingRotated && !inSquareSelect) Rotate();

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
	}
	void Update()
	{
		SelectUpdate();
	}

	protected void MouseDown()
    {
		// Drag with left click
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if (this.transform.parent == null || this.transform.parent.tag != "SelectionParent")
		{
			startPosX = mousePos.x - this.transform.localPosition.x;
			startPosY = mousePos.y - this.transform.localPosition.y;
		}
		else
		{
			startPosX = mousePos.x - this.transform.parent.localPosition.x;
			startPosY = mousePos.y - this.transform.parent.localPosition.y;
		}

		clickTimer = 0;
		isBeingHeld = true;
	}
	void OnMouseDown()
	{
		MouseDown();
	}

    protected void MouseUp()
    {
		isBeingHeld = false;
    }
    private void OnMouseUp()
    {
		MouseUp();
	}

	private void Rotate()
	{
		Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		rotation *= Quaternion.Euler(0, 0, -90);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
	}
	public virtual void Select() { }
    public virtual void Deselect() { }
}
