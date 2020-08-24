using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stores all the sounds produced by the lines

public class SoundManager : MonoBehaviour
{
    private int mode = 0;

	public SoundBank redBank;
	public SoundBank yellowBank;
	public SoundBank blueBank;
	public SoundBank greenBank;

	[SerializeField]
	private SoundBank[] RedSounds = new SoundBank[8];
	[SerializeField]
	private SoundBank[] YellowSounds = new SoundBank[8];
	[SerializeField]
	private SoundBank[] BlueSounds = new SoundBank[8];
	[SerializeField]
	private SoundBank[] GreenSounds = new SoundBank[8];

	private GameObject[] audioSourceArray = new GameObject[16];
	private int audioSourceIndex = 0;



	private void Start() {
		if (redBank == null) redBank = RedSounds[0];
		if (yellowBank == null) yellowBank = YellowSounds[0];
		if (blueBank == null) blueBank = BlueSounds[0];
		if (greenBank == null) greenBank = GreenSounds[0];

		for (int i = 0; i < audioSourceArray.Length; i++)
		{
			audioSourceArray[i] = new GameObject("SoundSource");
			audioSourceArray[i].transform.SetParent(transform);
			audioSourceArray[i].AddComponent<AudioSource>();
			audioSourceArray[i].GetComponent<AudioSource>().spatialBlend = 1;
			audioSourceArray[i].GetComponent<AudioSource>().rolloffMode = AudioRolloffMode.Linear;
		}
	}


	public void GetAudio(AudioSource playClip, string lineColor, int pitch, int mode)
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

	public void GetAudio(Vector3 position, ElemColor color, int pitch)
	{
		if (color == ElemColor.Red)
		{
			audioSourceArray[audioSourceIndex].transform.position = position;
			AudioSource sound = audioSourceArray[audioSourceIndex].GetComponent<AudioSource>();
			sound.Stop();
			sound.clip = redBank.clips[pitch];
			sound.volume = redBank.volumes[pitch];
			sound.Play();

			audioSourceIndex++;
			if (audioSourceIndex >= audioSourceArray.Length) audioSourceIndex = 0;
		}

		if (color == ElemColor.Yellow)
		{
			audioSourceArray[audioSourceIndex].transform.position = position;
			AudioSource sound = audioSourceArray[audioSourceIndex].GetComponent<AudioSource>();
			sound.Stop();
			sound.clip = yellowBank.clips[pitch];
			sound.volume = yellowBank.volumes[pitch];
			sound.Play();

			audioSourceIndex++;
			if (audioSourceIndex >= audioSourceArray.Length) audioSourceIndex = 0;
		}

		if (color == ElemColor.Blue)
		{
			audioSourceArray[audioSourceIndex].transform.position = position;
			AudioSource sound = audioSourceArray[audioSourceIndex].GetComponent<AudioSource>();
			sound.Stop();
			sound.clip = blueBank.clips[pitch];
			sound.volume = blueBank.volumes[pitch];
			sound.Play();

			audioSourceIndex++;
			if (audioSourceIndex >= audioSourceArray.Length) audioSourceIndex = 0;
		}

		if (color == ElemColor.Green)
		{
			audioSourceArray[audioSourceIndex].transform.position = position;
			AudioSource sound = audioSourceArray[audioSourceIndex].GetComponent<AudioSource>();
			sound.Stop();
			sound.clip = greenBank.clips[pitch];
			sound.volume = greenBank.volumes[pitch];
			sound.Play();

			audioSourceIndex++;
			if (audioSourceIndex >= audioSourceArray.Length) audioSourceIndex = 0;
		}
	}
}
