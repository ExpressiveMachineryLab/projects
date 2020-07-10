using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stores all the sounds produced by the lines

public class SoundManager : MonoBehaviour
{
    public float[] Volume;
    private int mode = 0;

    // Each sound can consist of one or more sounds
    public AudioClip[] BlueSounds11;
    public AudioClip[] BlueSounds12;
    public AudioClip[] BlueSounds13;
    public AudioClip[] BlueSounds14;
    public AudioClip[] BlueSounds15;

    public AudioClip[] BlueSounds21;
    public AudioClip[] BlueSounds22;
    public AudioClip[] BlueSounds23;
    public AudioClip[] BlueSounds24;
    public AudioClip[] BlueSounds25;

    public AudioClip[] BlueSounds31;
    public AudioClip[] BlueSounds32;
    public AudioClip[] BlueSounds33;
    public AudioClip[] BlueSounds34;
    public AudioClip[] BlueSounds35;

    // The encompassing array
    private AudioClip[][][] BlueSounds = new AudioClip[3][][];

    public AudioClip[] GreenSounds11;
    public AudioClip[] GreenSounds12;
    public AudioClip[] GreenSounds13;
    public AudioClip[] GreenSounds14;
    public AudioClip[] GreenSounds15;

    public AudioClip[] GreenSounds21;
    public AudioClip[] GreenSounds22;
    public AudioClip[] GreenSounds23;
    public AudioClip[] GreenSounds24;
    public AudioClip[] GreenSounds25;

    public AudioClip[] GreenSounds31;
    public AudioClip[] GreenSounds32;
    public AudioClip[] GreenSounds33;
    public AudioClip[] GreenSounds34;
    public AudioClip[] GreenSounds35;

    private AudioClip[][][] GreenSounds = new AudioClip[3][][];

    public AudioClip[] RedSounds11;
    public AudioClip[] RedSounds12;
    public AudioClip[] RedSounds13;
    public AudioClip[] RedSounds14;
    public AudioClip[] RedSounds15;

    public AudioClip[] RedSounds21;
    public AudioClip[] RedSounds22;
    public AudioClip[] RedSounds23;
    public AudioClip[] RedSounds24;
    public AudioClip[] RedSounds25;

    public AudioClip[] RedSounds31;
    public AudioClip[] RedSounds32;
    public AudioClip[] RedSounds33;
    public AudioClip[] RedSounds34;
    public AudioClip[] RedSounds35;

    private AudioClip[][][] RedSounds = new AudioClip[3][][];

    public AudioClip[] YellowSounds11;
    public AudioClip[] YellowSounds12;
    public AudioClip[] YellowSounds13;
    public AudioClip[] YellowSounds14;
    public AudioClip[] YellowSounds15;

    public AudioClip[] YellowSounds21;
    public AudioClip[] YellowSounds22;
    public AudioClip[] YellowSounds23;
    public AudioClip[] YellowSounds24;
    public AudioClip[] YellowSounds25;

    public AudioClip[] YellowSounds31;
    public AudioClip[] YellowSounds32;
    public AudioClip[] YellowSounds33;
    public AudioClip[] YellowSounds34;
    public AudioClip[] YellowSounds35;

    private AudioClip[][][] YellowSounds = new AudioClip[3][][];

    void Start() 
    {
        BlueSounds[0] = new AudioClip[5][];
        BlueSounds[0][0] = BlueSounds11;
        BlueSounds[0][1] = BlueSounds12;
        BlueSounds[0][2] = BlueSounds13;
        BlueSounds[0][3] = BlueSounds14;
        BlueSounds[0][4] = BlueSounds15;

        BlueSounds[1] = new AudioClip[5][];
        BlueSounds[1][0] = BlueSounds21;
        BlueSounds[1][1] = BlueSounds22;
        BlueSounds[1][2] = BlueSounds23;
        BlueSounds[1][3] = BlueSounds24;
        BlueSounds[1][4] = BlueSounds25;

        BlueSounds[2] = new AudioClip[5][];
        BlueSounds[2][0] = BlueSounds31;
        BlueSounds[2][1] = BlueSounds32;
        BlueSounds[2][2] = BlueSounds33;
        BlueSounds[2][3] = BlueSounds34;
        BlueSounds[2][4] = BlueSounds35;

        GreenSounds[0] = new AudioClip[5][];
        GreenSounds[0][0] = GreenSounds11;
        GreenSounds[0][1] = GreenSounds12;
        GreenSounds[0][2] = GreenSounds13;
        GreenSounds[0][3] = GreenSounds14;
        GreenSounds[0][4] = GreenSounds15;

        GreenSounds[1] = new AudioClip[5][];
        GreenSounds[1][0] = GreenSounds21;
        GreenSounds[1][1] = GreenSounds22;
        GreenSounds[1][2] = GreenSounds23;
        GreenSounds[1][3] = GreenSounds24;
        GreenSounds[1][4] = GreenSounds25;

        GreenSounds[2] = new AudioClip[5][];
        GreenSounds[2][0] = GreenSounds31;
        GreenSounds[2][1] = GreenSounds32;
        GreenSounds[2][2] = GreenSounds33;
        GreenSounds[2][3] = GreenSounds34;
        GreenSounds[2][4] = GreenSounds35;

        RedSounds[0] = new AudioClip[5][];
        RedSounds[0][0] = RedSounds11;
        RedSounds[0][1] = RedSounds12;
        RedSounds[0][2] = RedSounds13;
        RedSounds[0][3] = RedSounds14;
        RedSounds[0][4] = RedSounds15;

        RedSounds[1] = new AudioClip[5][];
        RedSounds[1][0] = RedSounds21;
        RedSounds[1][1] = RedSounds22;
        RedSounds[1][2] = RedSounds23;
        RedSounds[1][3] = RedSounds24;
        RedSounds[1][4] = RedSounds25;

        RedSounds[2] = new AudioClip[5][];
        RedSounds[2][0] = RedSounds31;
        RedSounds[2][1] = RedSounds32;
        RedSounds[2][2] = RedSounds33;
        RedSounds[2][3] = RedSounds34;
        RedSounds[2][4] = RedSounds35;

        YellowSounds[0] = new AudioClip[5][];
        YellowSounds[0][0] = YellowSounds11;
        YellowSounds[0][1] = YellowSounds12;
        YellowSounds[0][2] = YellowSounds13;
        YellowSounds[0][3] = YellowSounds14;
        YellowSounds[0][4] = YellowSounds15;

        YellowSounds[1] = new AudioClip[5][];
        YellowSounds[1][0] = YellowSounds21;
        YellowSounds[1][1] = YellowSounds22;
        YellowSounds[1][2] = YellowSounds23;
        YellowSounds[1][3] = YellowSounds24;
        YellowSounds[1][4] = YellowSounds25;

        YellowSounds[2] = new AudioClip[5][];
        YellowSounds[2][0] = YellowSounds31;
        YellowSounds[2][1] = YellowSounds32;
        YellowSounds[2][2] = YellowSounds33;
        YellowSounds[2][3] = YellowSounds34;
        YellowSounds[2][4] = YellowSounds35;
    }

    public AudioClip GetAudio(AudioSource playClip, string lineColor, int pitch, int mode)
    {
        if (lineColor == "BlueLine") 
        {
            for (int i = 0; i < BlueSounds[mode][pitch].Length; i++) 
            {
                playClip.PlayOneShot(BlueSounds[mode][pitch][i]);
            }
        }

        if (lineColor == "GreenLine")
        {
            for (int i = 0; i < GreenSounds[mode][pitch].Length; i++)
            {
                playClip.PlayOneShot(GreenSounds[mode][pitch][i]);
            }
        }

        if (lineColor == "RedLine")
        {
            for (int i = 0; i < RedSounds[mode][pitch].Length; i++)
            {
                playClip.volume = 0.7f;
                playClip.PlayOneShot(RedSounds[mode][pitch][i]);
            }
        }

        if (lineColor == "YellowLine")
        {
            for (int i = 0; i < YellowSounds[mode][pitch].Length; i++)
            {   
                playClip.volume = 0.3f;
                playClip.PlayOneShot(YellowSounds[mode][pitch][i]);
            }
        }
        return null;
    }
}
