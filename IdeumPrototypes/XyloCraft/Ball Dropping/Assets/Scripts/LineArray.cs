using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineArray : MonoBehaviour
{
    public GameObject[] Lines;
    public Sprite[] Sprites;
    public Sprite[] Hit_Sprites;

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

    public Sprite GetSprite(int index)
    {
        return Sprites[index];
    }

    public Sprite GetHitSprite(int index)
    {
        return Hit_Sprites[index];
    }
}
