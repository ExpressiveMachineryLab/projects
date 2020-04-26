using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandWindow : MonoBehaviour
{
    public GameObject Window;
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
        Window.SetActive(expanded);
    }
}
