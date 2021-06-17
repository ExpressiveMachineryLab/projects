using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollider : MonoBehaviour
{
    Collider2D thisCollider;

    private void Start()
    {
        thisCollider = gameObject.GetComponent<Collider2D>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            checkTrash();
        }
    }

    public void checkTrash()
    {
        List<Collider2D> overlapping = new List<Collider2D>();
        int isOverlap = thisCollider.OverlapCollider(new ContactFilter2D(), overlapping);

        bool mouseOver = false;
        //get mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        Debug.Log(hit.collider.gameObject.name);
        if(hit.collider.tag == "trash")
        {
            mouseOver = true;
        }
        if (isOverlap > 0 && mouseOver)
        {
            foreach(Collider2D toBeTrashed in overlapping)
            {
                toBeTrashed.gameObject.SetActive(false);
            }
        }
        
    }
}
