using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public SelectionManager manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClickLeft()
    {
        Debug.Log("left");
        manager.selectedObject.transform.Rotate(Vector3.forward * 5.0f);
    }

    public void OnClickRight()
    {
        manager.selectedObject.transform.Rotate(Vector3.back * 5.0f);
    }
}
