using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerSame : MonoBehaviour
{
    public AudioClip[] Sounds;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public AudioClip GetAudio(int lineColor)
    {

        return Sounds[lineColor];
    }
}
