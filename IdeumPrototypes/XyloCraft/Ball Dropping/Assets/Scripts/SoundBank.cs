using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundBank", menuName = "SoundBank")]
public class SoundBank : ScriptableObject {
	public string bankName = "New Sound Bank";

	public float[] volumes = new float[5];
	public AudioClip[] clips = new AudioClip[5];

	public void playAudioClip(AudioSource source, int index) 
	{
		source.PlayOneShot(clips[index], volumes[index]);
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
