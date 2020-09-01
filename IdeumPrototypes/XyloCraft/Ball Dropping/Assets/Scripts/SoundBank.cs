using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundBank", menuName = "SoundBank")]
public class SoundBank : ScriptableObject {
	public string bankName = "New Sound Bank";
	public Phonic voice;
	public float fadeTime = 0.25f;

	public float[] volumes = new float[5];
	public AudioClip[] clips = new AudioClip[5];
	

	public void playAudioClip(AudioSource source, int index) 
	{
		source.PlayOneShot(clips[index], volumes[index]);
	}

	public GameObject playAudioClip(GameObject source, int index)
	{

		GameObject playClip = new GameObject("SoundSource");
		playClip.transform.SetParent(source.transform.parent.transform);
		AudioSource sound = playClip.AddComponent<AudioSource>();
		playClip.GetComponent<AudioSource>().spatialBlend = 1;
		playClip.GetComponent<AudioSource>().rolloffMode = AudioRolloffMode.Linear;
		playClip.GetComponent<AudioSource>().dopplerLevel = 0;

		sound.clip = clips[index];
		sound.volume = volumes[index];
		sound.Play();

		Destroy(playClip, sound.clip.length);

		return playClip;
	}

	public AudioClip getAudioClip(int index) 
	{
		return clips[index];
	}

	public float getVolume(int index) 
	{
		return volumes[index];
	}
}

public enum Phonic
{
	Poly,
	Mono
}