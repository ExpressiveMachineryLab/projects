using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] BlueSounds;
    public AudioClip[] GreenSounds;
    public AudioClip[] PinkSounds;
    public AudioClip[] RedSounds;
    public AudioClip[] YellowSounds;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(BlueSounds);
    }

    // Update is called once per frame
    void Update()
    {
    }

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
            return PinkSounds[pitch];
        }
        else if (lineColor == 3)
        {
            return RedSounds[pitch];
        }
        else if (lineColor == 4)
        {
            return YellowSounds[pitch];
        }
        return null;
    }
}
