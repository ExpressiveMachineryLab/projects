using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public bool limited = true;
    public int color;
    private int numberInstantiated = 0;
    public Sprite emitterImage;
    public Sprite selectedImage;
    public GameObject cloneObject;
    private GameManager gameManager;
    private SelectionManager SelectionManagerCode;
    private GameObject dragObject;
    
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SelectionManagerCode = GameObject.Find("SelectedObject").GetComponent<SelectionManager>();
    }

    void Update()
    {
        if (limited) 
        {
            if (gameManager.GetBird(color) < 5)
            {
                this.transform.GetChild(0).GetComponent<Image>().sprite = emitterImage;
            }
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        if (dragObject == null) {
            if (limited)
            {
                if (gameManager.GetBird(color) < 5)
                {
                    dragObject = Instantiate(cloneObject, mousePos, cloneObject.transform.rotation) as GameObject;
                    SelectionManagerCode.NewSelection(dragObject);
                    gameManager.InstantiateBird(color);
                    if (gameManager.GetBird(color) == 5) 
                    {
                        this.transform.GetChild(0).GetComponent<Image>().sprite = selectedImage;
                    } 
                }
            }
            else 
            {
                dragObject = Instantiate(cloneObject, mousePos, cloneObject.transform.rotation) as GameObject;
                SelectionManagerCode.NewSelection(dragObject);
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