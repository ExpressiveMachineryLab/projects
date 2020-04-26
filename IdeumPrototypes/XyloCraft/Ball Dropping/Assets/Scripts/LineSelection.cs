using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LineSelection : MonoBehaviour, IPointerClickHandler
{
    public GameObject Option1;
    public GameObject Option2;
    public GameObject Option3;
    public Dropdown localCodeStateDropdown;

    public Sprite[] Lines;

    public int CurrentLineIndex = 0;
    private bool expanded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        expanded = !expanded;
        Option1.SetActive(expanded);
        Option2.SetActive(expanded);
        Option3.SetActive(expanded);
    }

    public Sprite GetSprite()
    {
        return Lines[CurrentLineIndex];
    }

    public int GetLineIndex()
    {
        return CurrentLineIndex;
    }
    
    public void SetSprite(int NewLineIndex)
    {
        CurrentLineIndex = NewLineIndex;
        this.gameObject.GetComponentsInChildren<Image>()[1].sprite = GetSprite();
        localCodeStateDropdown.value = 0;
    }
}
