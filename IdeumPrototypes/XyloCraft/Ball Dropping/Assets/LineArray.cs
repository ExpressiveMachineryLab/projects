using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineArray : MonoBehaviour
{
    public GameObject[] Lines;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetObject(int index)
    {
        return Lines[index];
    }
}
