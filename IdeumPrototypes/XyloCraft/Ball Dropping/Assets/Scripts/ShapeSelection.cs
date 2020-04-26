using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeSelection : MonoBehaviour
{
    public GameObject ShapesWindow;
    private bool expanded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Expand()
    {
        Debug.Log("expanded");
        expanded = !expanded;
        ShapesWindow.SetActive(expanded);
    }
}
