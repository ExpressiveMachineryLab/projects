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
        Debug.Log("Mouse Up");
        List<Collider2D> overlapping = new List<Collider2D>();
        int isOverlap = thisCollider.OverlapCollider(new ContactFilter2D(), overlapping);
        Debug.Log(isOverlap.ToString() + " colliders overlapping");
        if(isOverlap > 0)
        {
            foreach(Collider2D toBeTrashed in overlapping)
            {
                toBeTrashed.gameObject.SetActive(false);
            }
        }
    }
}
