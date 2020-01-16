using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    Vector2 startPosition;
    Vector2 EndPosition;

    GameObject lineStartPrefab;
    GameObject lineEndPrefab;

    GameObject wallPrefab;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            //spawn line start and end objects at mouse XY position
            
        }
    }
    

    void createWall(Vector2 startPos, Vector2 endPos)
    {

    }


}
