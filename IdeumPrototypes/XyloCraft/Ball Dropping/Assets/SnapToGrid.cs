using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour
{
    public float grid = 0.5f;
    float x = 0f, y = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float reciprocalGrid = 1f / grid;

        x = Mathf.Round(transform.position.x * reciprocalGrid) / reciprocalGrid;
        y = Mathf.Round(transform.position.y * reciprocalGrid) / reciprocalGrid;

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
