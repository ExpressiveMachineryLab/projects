using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    BoxCollider2D thisCollider;
    AudioSource playClip;

    public AudioClip collisionSound;

    //public AudioClip clip2;


    // Start is called before the first frame update
    void Start()
    {
        thisCollider = GetComponent<BoxCollider2D>();
        playClip = GetComponent<AudioSource>();

        // thisCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    playClip.clip = collisionSound;
    //    playClip.Play();
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{

    ////   playClip.Stop();
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playClip.clip = collisionSound;
        playClip.Play();
        //SpriteRenderer collidedObject = collision.gameObject.transform.GetComponent<SpriteRenderer>();
        //collidedObject.sprite = (Sprite)Resources.Load<Sprite>(collidedObject.sprite + "_hit");
        //collision.gameObject.transform.GetComponent<SpriteRenderer>().sprite = collision.gameObject.ToString() + "_hit";
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        //playClip.Stop();
    }
}