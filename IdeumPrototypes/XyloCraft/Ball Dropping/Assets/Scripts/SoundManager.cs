using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public float[] Volume;
    public AudioClip[] BlueSounds;
    public AudioClip[] GreenSounds;
    //public AudioClip[] PinkSounds;
    public AudioClip[] RedSounds;
    public AudioClip[] YellowSounds;

    public AudioClip GetAudio(int lineColor, int pitch)
    {

        if (lineColor == 0)
        {
            return BlueSounds[pitch];
        }
        else if (lineColor == 1)
        {
            return GreenSounds[pitch];
        }
        else if (lineColor == 2)
        {
            return RedSounds[pitch];
        }
        else if (lineColor == 3)
        {
            return YellowSounds[pitch];
        }
        return null;
    }

    public float GetVolume(int ball)
    {
        return Volume[ball];
    }
}
