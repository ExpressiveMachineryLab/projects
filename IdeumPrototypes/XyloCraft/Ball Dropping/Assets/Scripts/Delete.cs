using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    public SelectionManager selectionManager;

    public void OnClickDelete()
    {
        selectionManager.DeleteSelection(selectionManager.selectedObject.gameObject);
    }
}
