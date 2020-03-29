using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public bool onlyOne = true;
    private bool instantiated = false;
    public Sprite selectedImage;
    public GameObject cloneObject;
    private SelectionManager SelectionManagerCode;
    private GameObject dragObject;
    
    void Start()
    {
        SelectionManagerCode = GameObject.Find("SelectedObject").GetComponent<SelectionManager>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        if (dragObject == null) {
            if (onlyOne) 
            {
                this.transform.GetChild(0).GetComponent<Image>().sprite = selectedImage;
                if (!instantiated) 
                {
                    dragObject = Instantiate(cloneObject, mousePos, cloneObject.transform.rotation) as GameObject;
                    SelectionManagerCode.NewSelection(dragObject);
                    instantiated = !instantiated;
                }
            }
            
        }
        if (dragObject) 
        {
            dragObject.transform.position = mousePos;
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragObject = null;
    }
}
