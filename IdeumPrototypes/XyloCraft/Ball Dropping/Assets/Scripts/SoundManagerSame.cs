using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerSame : MonoBehaviour
{
    public AudioClip[] Sounds;

    public AudioClip GetAudio(int lineColor)
    {
        return Sounds[lineColor];
    }
}
