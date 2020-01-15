using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{

    public GameObject ballPrefab;

    public float rotationSpeed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        transform.eulerAngles = new Vector3(0, 0, 90);
    }


    private void OnMouseDown()
    {
        Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }
}
