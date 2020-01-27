using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    public GameObject selectedObject;

    public void OnClickDelete()
    {
        Debug.Log(selectedObject.gameObject);
        //Destroy(selectedObject);
    }
}
