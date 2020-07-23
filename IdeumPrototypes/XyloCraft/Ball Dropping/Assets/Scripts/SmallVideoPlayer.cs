using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SmallVideoPlayer : MonoBehaviour
{
	public string videoFileName;

	private VideoPlayer videoPlayer;

	private void Start() {
		videoPlayer = gameObject.GetComponent<VideoPlayer>();
		videoPlayer.source = VideoSource.Url;
		videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);

		videoPlayer.Play();
	}

	private void Awake() {
		if (videoPlayer != null) videoPlayer.Pause();

	}

	private void OnMouseDown() {
		gameObject.SetActive(false);
	}


}
