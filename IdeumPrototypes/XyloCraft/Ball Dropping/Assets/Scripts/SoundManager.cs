using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stores all the sounds produced by the lines

public class SoundManager : MonoBehaviour
{
    private int mode = 0;

	public bool devScene = false;

	public SoundBank redBank;
	public SoundBank yellowBank;
	public SoundBank blueBank;
	public SoundBank greenBank;

	[SerializeField]
	private SoundBank[] RedSounds = new SoundBank[6];
	[SerializeField]
	private SoundBank[] YellowSounds = new SoundBank[6];
	[SerializeField]
	private SoundBank[] BlueSounds = new SoundBank[6];
	[SerializeField]
	private SoundBank[] GreenSounds = new SoundBank[6];

	private void Start() {
		if (redBank == null) redBank = RedSounds[0];
		if (yellowBank == null) yellowBank = YellowSounds[0];
		if (blueBank == null) blueBank = BlueSounds[0];
		if (greenBank == null) greenBank = GreenSounds[0];
	}


	public void GetAudio(AudioSource playClip, string lineColor, int pitch, int mode)
	{
		if(!devScene)
		{
			if (lineColor == "RedLine")
			{
				RedSounds[mode].playAudioClip(playClip, pitch);
			}

			if (lineColor == "YellowLine")
			{
				YellowSounds[mode].playAudioClip(playClip, pitch);
			}

			if (lineColor == "BlueLine")
			{
				BlueSounds[mode].playAudioClip(playClip, pitch);
			}

			if (lineColor == "GreenLine")
			{
				GreenSounds[mode].playAudioClip(playClip, pitch);
			}

			return;
		}


		if (lineColor == "RedLine")
		{
			redBank.playAudioClip(playClip, pitch);
		}

		if (lineColor == "YellowLine")
		{
			yellowBank.playAudioClip(playClip, pitch);
		}

		if (lineColor == "BlueLine")
		{
			blueBank.playAudioClip(playClip, pitch);
		}

		if (lineColor == "GreenLine")
		{
			greenBank.playAudioClip(playClip, pitch);
		}



	}
}
