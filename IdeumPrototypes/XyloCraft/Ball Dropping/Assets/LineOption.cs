using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LineOption : MonoBehaviour, IPointerClickHandler
{
    public LineSelection Selection;

    public int CurrentLineIndex;
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
        this.gameObject.GetComponentsInChildren<Image>()[1].sprite = Selection.GetSprite();
        int oldLineIndex = Selection.GetLineIndex();
        Selection.SetSprite(CurrentLineIndex);
        CurrentLineIndex = oldLineIndex;
    }
}
