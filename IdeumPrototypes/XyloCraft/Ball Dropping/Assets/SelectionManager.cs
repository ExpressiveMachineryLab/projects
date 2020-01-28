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
            RaycastHit2D UIhit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            Debug.Log(hit.collider);
            if (hit.collider != null)
            {
                selectedObject.transform.GetChild(0).gameObject.SetActive(false);
                selectedObject = hit.collider.gameObject;
                selectedObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            //if (hit.collider.gameObject)
        }
    }

    public void NewSelection(GameObject selectedGameObject) 
    {
        selectedObject.transform.GetChild(0).gameObject.SetActive(false);
        selectedObject = selectedGameObject;
        selectedObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
