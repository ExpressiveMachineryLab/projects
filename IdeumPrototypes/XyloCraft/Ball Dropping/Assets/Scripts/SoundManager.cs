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

    public AudioClip[] BlueSounds41;
    public AudioClip[] BlueSounds42;
    public AudioClip[] BlueSounds43;
    public AudioClip[] BlueSounds44;
    public AudioClip[] BlueSounds45;

    public AudioClip[] BlueSounds51;
    public AudioClip[] BlueSounds52;
    public AudioClip[] BlueSounds53;
    public AudioClip[] BlueSounds54;
    public AudioClip[] BlueSounds55;

    // The encompassing array
    private AudioClip[][][] BlueSounds = new AudioClip[5][][];

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

    public AudioClip[] GreenSounds41;
    public AudioClip[] GreenSounds42;
    public AudioClip[] GreenSounds43;
    public AudioClip[] GreenSounds44;
    public AudioClip[] GreenSounds45;

    public AudioClip[] GreenSounds51;
    public AudioClip[] GreenSounds52;
    public AudioClip[] GreenSounds53;
    public AudioClip[] GreenSounds54;
    public AudioClip[] GreenSounds55;

    private AudioClip[][][] GreenSounds = new AudioClip[5][][];

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

    public AudioClip[] RedSounds41;
    public AudioClip[] RedSounds42;
    public AudioClip[] RedSounds43;
    public AudioClip[] RedSounds44;
    public AudioClip[] RedSounds45;

    public AudioClip[] RedSounds51;
    public AudioClip[] RedSounds52;
    public AudioClip[] RedSounds53;
    public AudioClip[] RedSounds54;
    public AudioClip[] RedSounds55;

    private AudioClip[][][] RedSounds = new AudioClip[5][][];

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

    public AudioClip[] YellowSounds41;
    public AudioClip[] YellowSounds42;
    public AudioClip[] YellowSounds43;
    public AudioClip[] YellowSounds44;
    public AudioClip[] YellowSounds45;

    public AudioClip[] YellowSounds51;
    public AudioClip[] YellowSounds52;
    public AudioClip[] YellowSounds53;
    public AudioClip[] YellowSounds54;
    public AudioClip[] YellowSounds55;

    private AudioClip[][][] YellowSounds = new AudioClip[5][][];

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

        BlueSounds[3] = new AudioClip[5][];
        BlueSounds[3][0] = BlueSounds41;
        BlueSounds[3][1] = BlueSounds42;
        BlueSounds[3][2] = BlueSounds43;
        BlueSounds[3][3] = BlueSounds44;
        BlueSounds[3][4] = BlueSounds45;

        BlueSounds[4] = new AudioClip[5][];
        BlueSounds[4][0] = BlueSounds51;
        BlueSounds[4][1] = BlueSounds52;
        BlueSounds[4][2] = BlueSounds53;
        BlueSounds[4][3] = BlueSounds54;
        BlueSounds[4][4] = BlueSounds55;

        

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

        GreenSounds[3] = new AudioClip[5][];
        GreenSounds[3][0] = GreenSounds41;
        GreenSounds[3][1] = GreenSounds42;
        GreenSounds[3][2] = GreenSounds43;
        GreenSounds[3][3] = GreenSounds44;
        GreenSounds[3][4] = GreenSounds45;

        GreenSounds[4] = new AudioClip[5][];
        GreenSounds[4][0] = GreenSounds51;
        GreenSounds[4][1] = GreenSounds52;
        GreenSounds[4][2] = GreenSounds53;
        GreenSounds[4][3] = GreenSounds54;
        GreenSounds[4][4] = GreenSounds55;



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

        RedSounds[3] = new AudioClip[5][];
        RedSounds[3][0] = RedSounds41;
        RedSounds[3][1] = RedSounds42;
        RedSounds[3][2] = RedSounds43;
        RedSounds[3][3] = RedSounds44;
        RedSounds[3][4] = RedSounds45;

        RedSounds[4] = new AudioClip[5][];
        RedSounds[4][0] = RedSounds51;
        RedSounds[4][1] = RedSounds52;
        RedSounds[4][2] = RedSounds53;
        RedSounds[4][3] = RedSounds54;
        RedSounds[4][4] = RedSounds55;

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

        YellowSounds[3] = new AudioClip[5][];
        YellowSounds[3][0] = YellowSounds41;
        YellowSounds[3][1] = YellowSounds42;
        YellowSounds[3][2] = YellowSounds43;
        YellowSounds[3][3] = YellowSounds44;
        YellowSounds[3][4] = YellowSounds45;

        YellowSounds[4] = new AudioClip[5][];
        YellowSounds[4][0] = YellowSounds51;
        YellowSounds[4][1] = YellowSounds52;
        YellowSounds[4][2] = YellowSounds53;
        YellowSounds[4][3] = YellowSounds54;
        YellowSounds[4][4] = YellowSounds55;
    }

    public AudioClip GetAudio(AudioSource playClip, string lineColor, int pitch, int mode)
    {
        if (lineColor == "BlueLine") 
        {
            for (int i = 0; i < BlueSounds[mode][pitch].Length; i++) 
            {
                playClip.volume = 0.6f;
                playClip.PlayOneShot(BlueSounds[mode][pitch][i]);
            }
        }

        if (lineColor == "GreenLine")
        {
            for (int i = 0; i < GreenSounds[mode][pitch].Length; i++)
            {
                playClip.volume = 0.6f;
                playClip.PlayOneShot(GreenSounds[mode][pitch][i]);
            }
        }

        if (lineColor == "RedLine")
        {
            for (int i = 0; i < RedSounds[mode][pitch].Length; i++)
            {
                playClip.volume = 0.3f;
                playClip.PlayOneShot(RedSounds[mode][pitch][i]);
            }
        }

        if (lineColor == "YellowLine")
        {
            for (int i = 0; i < YellowSounds[mode][pitch].Length; i++)
            {   
                //playClip.volume = 1f;
                playClip.PlayOneShot(YellowSounds[mode][pitch][i]);
            }
        }
        return null;
    }
}
