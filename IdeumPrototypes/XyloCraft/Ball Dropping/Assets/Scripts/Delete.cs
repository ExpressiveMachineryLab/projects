using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    public SelectionManager selectedObject;

    public void OnClickDelete()
    {
        Debug.Log(selectedObject.gameObject);
        //Destroy(selectedObject);
    }
}
