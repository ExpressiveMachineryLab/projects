using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    public SelectionManager selectionManager;

    public void OnClickDelete()
    {
        Destroy(selectionManager.selectedObject.gameObject);
        // null 처리 
    }
}
