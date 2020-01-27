using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public GameObject selectedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //selectedObject = 
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        //    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        //    //Debug.Log(hit.collider.gameObject);
        //    if (hit.collider != null)
        //    {
        //        selectedObject.transform.GetChild(0).gameObject.SetActive(false);
        //        selectedObject = hit.collider.gameObject;
        //        selectedObject.transform.GetChild(0).gameObject.SetActive(true);
        //    }
        //}
    }

    public void OnClickLeft()
    {
        selectedObject.transform.Rotate(Vector3.forward * 5.0f);
    }

    public void OnClickRight()
    {
        selectedObject.transform.Rotate(Vector3.back * 5.0f);
    }
}
