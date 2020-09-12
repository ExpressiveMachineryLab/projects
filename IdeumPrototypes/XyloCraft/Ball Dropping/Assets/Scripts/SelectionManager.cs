using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject[] selectedObject = new GameObject[0];
	
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.tag != "Rotator" && hit.collider.tag != "Ball")
            {
                SetSelection(new GameObject[] { hit.collider.gameObject });
            }
        }
    }

    private void SetSelection(GameObject[] selectedGameObject)
    {
        if (selectedObject.Length >= 1)
        {
			foreach (GameObject item in selectedObject)
			{
				item.transform.GetChild(0).gameObject.SetActive(false);
				item.transform.GetChild(1).gameObject.SetActive(false);

			}
        }

        selectedObject = selectedGameObject;
		foreach (GameObject item in selectedObject)
		{
			item.transform.GetChild(0).gameObject.SetActive(true);
			item.transform.GetChild(1).gameObject.SetActive(true);

		}
    }

    public void NewSelection(GameObject[] selectedGameObject)
    {
        SetSelection(selectedGameObject);
    }

    public void DeleteSelection(GameObject[] selectedGameObject)
    {
		selectedObject = new GameObject[0];
		foreach (GameObject item in selectedGameObject)
		{
			Destroy(item);
		}
    }
}
