using System;
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

	public GameObject[] audioSourceArray = new GameObject[16];
	public int audioSourceIndex = 0;

	private float fadeTime = 0.25f;

	private void Start() {
		if (redBank == null) redBank = RedSounds[0];
		if (yellowBank == null) yellowBank = YellowSounds[0];
		if (blueBank == null) blueBank = BlueSounds[0];
		if (greenBank == null) greenBank = GreenSounds[0];
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

	public GameObject GetAudio(GameObject source, ElemColor color, int pitch)
	{

		if (color == ElemColor.Red)
		{
			GameObject newSource = redBank.playAudioClip(source, pitch);

			if (redBank.voice == Phonic.Mono)
			{
				StartCoroutine(FadeOutAndStop(source.GetComponent<AudioSource>(), redBank.fadeTime));
			}

			audioSourceArray[audioSourceIndex] = newSource;
			audioSourceIndex++;
			if (audioSourceIndex >= audioSourceArray.Length) audioSourceIndex = 0;
			if (audioSourceArray[audioSourceIndex] != null) StartCoroutine(FadeOutAndStop(audioSourceArray[audioSourceIndex].GetComponent<AudioSource>(), fadeTime));

			return newSource;
		}

		if (color == ElemColor.Yellow)
		{
			GameObject newSource = yellowBank.playAudioClip(source, pitch);

			if (yellowBank.voice == Phonic.Mono)
			{
				StartCoroutine(FadeOutAndStop(source.GetComponent<AudioSource>(), yellowBank.fadeTime));
			}

			audioSourceArray[audioSourceIndex] = newSource;
			audioSourceIndex++;
			if (audioSourceIndex >= audioSourceArray.Length) audioSourceIndex = 0;
			if (audioSourceArray[audioSourceIndex] != null) StartCoroutine(FadeOutAndStop(audioSourceArray[audioSourceIndex].GetComponent<AudioSource>(), fadeTime));

			return newSource;
		}

		if (color == ElemColor.Blue)
		{
			GameObject newSource = blueBank.playAudioClip(source, pitch);

			if (blueBank.voice == Phonic.Mono)
			{
				StartCoroutine(FadeOutAndStop(source.GetComponent<AudioSource>(), blueBank.fadeTime));
			}

			audioSourceArray[audioSourceIndex] = newSource;
			audioSourceIndex++;
			if (audioSourceIndex >= audioSourceArray.Length) audioSourceIndex = 0;
			if (audioSourceArray[audioSourceIndex] != null) StartCoroutine(FadeOutAndStop(audioSourceArray[audioSourceIndex].GetComponent<AudioSource>(), fadeTime));

			return newSource;
		}

		if (color == ElemColor.Green)
		{
			GameObject newSource = greenBank.playAudioClip(source, pitch);

			if (greenBank.voice == Phonic.Mono)
			{
				StartCoroutine(FadeOutAndStop(source.GetComponent<AudioSource>(), greenBank.fadeTime));
			}

			audioSourceArray[audioSourceIndex] = newSource;
			audioSourceIndex++;
			if (audioSourceIndex >= audioSourceArray.Length) audioSourceIndex = 0;
			if (audioSourceArray[audioSourceIndex] != null) StartCoroutine(FadeOutAndStop(audioSourceArray[audioSourceIndex].GetComponent<AudioSource>(), fadeTime));

			return newSource;
		}

		return null;
	}

	IEnumerator FadeOutAndStop(AudioSource source, float fadeTime)
	{
		float startTime = Time.time;
		float currentTime = 0f;
		float startVolume = source.volume;

		while (startTime + fadeTime > Time.time && source != null)
		{

			currentTime = Time.time - startTime;

			source.volume = Mathf.Lerp(startVolume, 0f, currentTime / fadeTime);
			yield return null;
		}

		if (source != null)
		{
			source.Stop();
			Destroy(source.gameObject);
		}
	}

	public string SoundManagerToSO()
	{
		StyleHUD hud = FindObjectOfType<StyleHUD>();

		string SOstring = "5";
		SOstring += "," + Array.IndexOf<SoundBank>(hud.availableSounds, redBank);
		SOstring += "," + Array.IndexOf<SoundBank>(hud.availableSounds, yellowBank);
		SOstring += "," + Array.IndexOf<SoundBank>(hud.availableSounds, blueBank);
		SOstring += "," + Array.IndexOf<SoundBank>(hud.availableSounds, greenBank);
		
		return SOstring;
	}

	public void SoundManagerFromSO(string SoundManagerSO)
	{
		StyleHUD hud = FindObjectOfType<StyleHUD>();
		string[] SOstring = SoundManagerSO.Split(new[] { "," }, System.StringSplitOptions.None);

		redBank = hud.availableSounds[int.Parse(SOstring[1])];
		yellowBank = hud.availableSounds[int.Parse(SOstring[2])];
		blueBank = hud.availableSounds[int.Parse(SOstring[3])];
		greenBank = hud.availableSounds[int.Parse(SOstring[4])];

		hud.ResetTextNames();
	}
}