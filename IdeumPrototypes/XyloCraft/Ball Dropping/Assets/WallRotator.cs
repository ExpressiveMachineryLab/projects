using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRotator : MonoBehaviour
{


    public GameObject wallChild;

    public float wallScalex;

    public float rotationSpeed = 5;

    public float scalingSpeed = 1;

    public enum WALL_COLOR { BLUE, GREEN, RED };

    public WALL_COLOR thisColor;

    // Start is called before the first frame update
    void Start()
    {
        wallScalex = wallChild.transform.localScale.x;
        thisColor = WALL_COLOR.RED;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            //scale the wall down
            float newX = wallScalex - scalingSpeed * Time.deltaTime;
            wallChild.transform.localScale = new Vector3(newX, wallChild.transform.localScale.y, wallChild.transform.localScale.z);
            wallScalex = newX;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //scale the wall down
            float newX = wallScalex + scalingSpeed * Time.deltaTime;

            wallChild.transform.localScale = new Vector3(newX, wallChild.transform.localScale.y, wallChild.transform.localScale.z);
            wallScalex = newX;
        }


        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //scale the wall down
            transform.Rotate(new Vector3(0, 0, -1* rotationSpeed * Time.deltaTime));
        }
    }
}
