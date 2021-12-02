using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Vector3 dir;

    public float speed;

    public float life;

    private bool init;
    // Start is called before the first frame update
    void Start()
    {
        //dir = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (!init) return;
        if (life > 0)
        {
            transform.position += dir * speed * Time.deltaTime;
            life--;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void setDir(Vector3 d)
    {
        dir = d;
        Debug.Log("SET DIRECTION TO: " + d);
        init = true;
    }
}
