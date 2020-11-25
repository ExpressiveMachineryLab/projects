using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stores all the sounds produced by the lines

public class SoundManager : MonoBehaviour
{
	public SoundBank redBank;
	public SoundBank yellowBank;
	public SoundBank blueBank;
	public SoundBank greenBank;

	private GameObject[] audioSourceArray = new GameObject[16];
	private int audioSourceIndex = 0;

	private float fadeTime = 0.2f;

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
		SOstring += "," + Array.IndexOf(hud.availableSounds, redBank).ToString("00");
		SOstring += Array.IndexOf(hud.availableSounds, yellowBank).ToString("00");
		SOstring += Array.IndexOf(hud.availableSounds, blueBank).ToString("00");
		SOstring += Array.IndexOf(hud.availableSounds, greenBank).ToString("00");
		SOstring += FindObjectOfType<GameManager>().GetSpeedMultiplier().ToString("00");


		return SOstring;
	}

	public void SoundManagerFromSO(string SoundManagerSO)
	{
		StyleHUD hud = FindObjectOfType<StyleHUD>();
		string[] SOstring = SoundManagerSO.Split(new[] { "," }, StringSplitOptions.None);

		redBank = hud.availableSounds[int.Parse(SOstring[1][0].ToString())*10 + int.Parse(SOstring[1][1].ToString())];
		yellowBank = hud.availableSounds[int.Parse(SOstring[1][2].ToString()) * 10 + int.Parse(SOstring[1][3].ToString())];
		blueBank = hud.availableSounds[int.Parse(SOstring[1][4].ToString()) * 10 + int.Parse(SOstring[1][5].ToString())];
		greenBank = hud.availableSounds[int.Parse(SOstring[1][6].ToString()) * 10 + int.Parse(SOstring[1][7].ToString())];
		FindObjectOfType<GameManager>().SetSpeedMultiplier(int.Parse(SOstring[1][8].ToString()) * 10 + int.Parse(SOstring[1][9].ToString()));

		hud.ResetTextNames();
	}
}