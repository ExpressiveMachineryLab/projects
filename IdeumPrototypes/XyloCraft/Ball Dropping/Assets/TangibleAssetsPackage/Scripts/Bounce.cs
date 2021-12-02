using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float frequency;
    public float magnitute;
    private Vector3 _startPos;
    
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = _startPos + transform.up * Mathf.Sin(Time.time * frequency) * magnitute;
        
    }
}
