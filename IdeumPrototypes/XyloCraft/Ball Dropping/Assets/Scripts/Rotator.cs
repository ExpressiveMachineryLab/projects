using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rotator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public SelectionManager manager;
    public bool isLeft;
    private bool pressed;

    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            if (isLeft)
            {
                manager.selectedObject.transform.Rotate(Vector3.forward * 2.5f);
            }
            else
            {
                manager.selectedObject.transform.Rotate(Vector3.back * 2.5f);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
}
