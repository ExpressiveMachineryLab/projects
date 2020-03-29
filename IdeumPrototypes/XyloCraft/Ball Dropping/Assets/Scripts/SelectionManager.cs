using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject selectedObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.tag != "Rotator")
            {
                SetSelection(hit.collider.gameObject);
            }
        }
    }

    private void SetSelection(GameObject selectedGameObject)
    {
        Debug.Log(selectedGameObject + " selected");
        if (selectedGameObject.tag != "Ignore")
        {
            if (selectedObject != null)
            {
                selectedObject.transform.GetChild(0).gameObject.SetActive(false);
                selectedObject.transform.GetChild(1).gameObject.SetActive(false);
            }

            selectedObject = selectedGameObject;
            selectedObject.transform.GetChild(0).gameObject.SetActive(true);
            selectedObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        
    }

    public void NewSelection(GameObject selectedGameObject)
    {
        SetSelection(selectedGameObject);
    }

    public void DeleteSelection(GameObject selectedGameObject)
    {
        selectedObject = null;
        Destroy(selectedGameObject.gameObject);
    }
}
