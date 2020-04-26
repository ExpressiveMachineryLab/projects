using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public float[] Volume;
    private int mode = 0;

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
    private AudioClip[][][] BlueSounds = new AudioClip[2][][];

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
    private AudioClip[][][] GreenSounds = new AudioClip[2][][];

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
    private AudioClip[][][] RedSounds = new AudioClip[2][][];

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
    private AudioClip[][][] YellowSounds = new AudioClip[2][][];

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
                playClip.volume = 0.2f;
                playClip.PlayOneShot(RedSounds[mode][pitch][i]);
            }
        }

        if (lineColor == "YellowLine")
        {
            for (int i = 0; i < YellowSounds[mode][pitch].Length; i++)
            {
                playClip.PlayOneShot(YellowSounds[mode][pitch][i]);
            }
        }
        return null;
    }
    public void PlayAudio (AudioSource lineAudioSource, string color, int chord, int mode) 
    {
        AudioClip[][] CurrentSounds = null;
        if (color == "Blue") 
        {
            CurrentSounds = BlueSounds[mode];
        }

        if (color == "Green")
        {
            CurrentSounds = GreenSounds[mode];
        }

        if (color == "Red")
        {
            CurrentSounds = RedSounds[mode];
        }

        if (color == "Yellow")
        {
            CurrentSounds = YellowSounds[mode];
        }

        for (int i = 0; i < CurrentSounds.Length; i++) 
        {
            lineAudioSource.PlayOneShot(CurrentSounds[chord][i]);
        }
    }

    public float GetVolume(int ball)
    {
        return Volume[ball];
    }
}
