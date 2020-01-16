using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject cloneObject;
    //private GameObject cloneImage = Instantiate(emitter) as GameObject;
    private GameObject dragObject;

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        if (dragObject == null) {
            dragObject = Instantiate(cloneObject, mousePos, cloneObject.transform.rotation) as GameObject;
        }

        dragObject.transform.position = mousePos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragObject = null;
    }
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
