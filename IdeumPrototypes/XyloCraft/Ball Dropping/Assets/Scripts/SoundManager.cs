using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stores all the sounds produced by the lines

public class SoundManager : MonoBehaviour
{
    private int mode = 0;
	
	[SerializeField]
	private SoundBank[] BlueSounds = new SoundBank[5];
	[SerializeField]
	private SoundBank[] GreenSounds = new SoundBank[5];
	[SerializeField]
    private SoundBank[] RedSounds = new SoundBank[5];
	[SerializeField]
    private SoundBank[] YellowSounds = new SoundBank[5];

    public AudioClip GetAudio(AudioSource playClip, string lineColor, int pitch, int mode)
    {
        if (lineColor == "BlueLine") {
			BlueSounds[mode].playAudioClip(playClip, pitch);
		}

		if (lineColor == "GreenLine") 
		{
			GreenSounds[mode].playAudioClip(playClip, pitch);
		}

        if (lineColor == "RedLine")
        {
			RedSounds[mode].playAudioClip(playClip, pitch);
		}

        if (lineColor == "YellowLine") 
		{
			YellowSounds[mode].playAudioClip(playClip, pitch);
		}
        return null;
    }
}
