using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TE.Examples;

public class SelectEmitter : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool isSelected;
    public GameObject emitter;
    private EmitterController TangibleController;

    // Start is called before the first frame update
    void Start()
    {
        TangibleController = GameObject.Find("Controller").GetComponent<EmitterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        isSelected = true;
        TangibleController.SetEmitter(emitter);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

}
