using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public float[] Volume;

    public AudioClip[] BlueSounds1;
    public AudioClip[] BlueSounds2;
    public AudioClip[] BlueSounds3;
    public AudioClip[] BlueSounds4;
    public AudioClip[] BlueSounds5;
    private AudioClip[][] BlueSounds = new AudioClip[5][];

    public AudioClip[] GreenSounds1;
    public AudioClip[] GreenSounds2;
    public AudioClip[] GreenSounds3;
    public AudioClip[] GreenSounds4;
    public AudioClip[] GreenSounds5;
    private AudioClip[][] GreenSounds = new AudioClip[5][];

    public AudioClip[] RedSounds1;
    public AudioClip[] RedSounds2;
    public AudioClip[] RedSounds3;
    public AudioClip[] RedSounds4;
    public AudioClip[] RedSounds5;
    private AudioClip[][] RedSounds = new AudioClip[5][];

    public AudioClip[] YellowSounds1;
    public AudioClip[] YellowSounds2;
    public AudioClip[] YellowSounds3;
    public AudioClip[] YellowSounds4;
    public AudioClip[] YellowSounds5;
    private AudioClip[][] YellowSounds = new AudioClip[5][];

    void Start() 
    {
        BlueSounds[0] = BlueSounds1;
        BlueSounds[1] = BlueSounds2;
        BlueSounds[2] = BlueSounds3;
        BlueSounds[3] = BlueSounds4;
        BlueSounds[4] = BlueSounds5;

        GreenSounds[0] = GreenSounds1;
        GreenSounds[1] = GreenSounds2;
        GreenSounds[2] = GreenSounds3;
        GreenSounds[3] = GreenSounds4;
        GreenSounds[4] = GreenSounds5;

        RedSounds[0] = RedSounds1;
        RedSounds[1] = RedSounds2;
        RedSounds[2] = RedSounds3;
        RedSounds[3] = RedSounds4;
        RedSounds[4] = RedSounds5;

        YellowSounds[0] = YellowSounds1;
        YellowSounds[1] = YellowSounds2;
        YellowSounds[2] = YellowSounds3;
        YellowSounds[3] = YellowSounds4;
        YellowSounds[4] = YellowSounds5;
    }

    public AudioClip GetAudio(AudioSource playClip, string lineColor, int pitch)
    {
        Debug.Log(pitch);
        if (lineColor == "BlueLine") 
        {
            for (int i = 0; i < BlueSounds[pitch].Length; i++) 
            {
                playClip.PlayOneShot(BlueSounds[pitch][i]);
            }
        }

        if (lineColor == "GreenLine")
        {
            for (int i = 0; i < GreenSounds[pitch].Length; i++)
            {
                playClip.PlayOneShot(GreenSounds[pitch][i]);
            }
        }

        if (lineColor == "RedLine")
        {
            for (int i = 0; i < RedSounds[pitch].Length; i++)
            {
                playClip.volume = 0.2f;
                playClip.PlayOneShot(RedSounds[pitch][i]);
            }
        }

        if (lineColor == "YellowLine")
        {
            for (int i = 0; i < YellowSounds[pitch].Length; i++)
            {
                playClip.PlayOneShot(YellowSounds[pitch][i]);
            }
        }
        return null;
    }
    public void PlayAudio (AudioSource lineAudioSource, string color, int chord) 
    {
        AudioClip[][] CurrentSounds = null;
        if (color == "Blue") 
        {
            CurrentSounds = BlueSounds;
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
