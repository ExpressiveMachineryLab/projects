using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{


    Vector2 forceVector;
    public float magnitude = 160;

    // Start is called before the first frame update
    void Start()
    {
       // float xVal = Random.Range(0, 1.0f);
       // float yVal = Random.Range(0, 1.0f);

        forceVector = new Vector2(0, 1).normalized;
        forceVector *= magnitude;


        GetComponent<Rigidbody2D>().AddForce(forceVector);

        Destroy(gameObject, 10f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
