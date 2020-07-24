using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandWindow : MonoBehaviour
{
    public GameObject Window;

    public void Expand()
    {
        Debug.Log("expanded");
        Window.SetActive(!Window.activeSelf);
    }
}
