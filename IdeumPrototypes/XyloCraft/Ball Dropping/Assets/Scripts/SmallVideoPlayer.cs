using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SmallVideoPlayer : MonoBehaviour
{
	public string videoFileName;
	public bool playOnAwake = true;

	private VideoPlayer videoPlayer;

	private void Awake() {
		if (videoPlayer != null) {
			videoPlayer.time = 0;
			videoPlayer.Play();
		}
	}

	private void Start() {
		videoPlayer = gameObject.GetComponent<VideoPlayer>();
		videoPlayer.source = VideoSource.Url;
		videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);

		if (playOnAwake) videoPlayer.Play();
	}

	public void PlayVideo() {
		videoPlayer.Play();
	}

	public void PauseVideo() {
		videoPlayer.Pause();
	}

	public void StopVideo() {
		videoPlayer.Pause();
		videoPlayer.time = 0;
	}

	public void SkipVideo() {
		StopVideo();
		gameObject.SetActive(false);
	}
}
