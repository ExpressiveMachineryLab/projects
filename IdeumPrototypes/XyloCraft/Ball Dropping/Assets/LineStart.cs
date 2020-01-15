using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineStart : MonoBehaviour
{

    public GameObject lineEnd;

    LineRenderer thisLine;


    // Start is called before the first frame update
    void Start()
    {
        thisLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lineEnd != null)
        {

            Vector3[] newPositions = { transform.position, lineEnd.transform.position };

            thisLine.SetPositions(newPositions);
                
        }

    }
}
