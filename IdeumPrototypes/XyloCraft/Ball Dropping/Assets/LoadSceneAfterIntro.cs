using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadSceneAfterIntro : MonoBehaviour
{
    public VideoPlayer VideoPlayer; 
    void Start()
    {
        VideoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "XylocodetitlecenteredMusic.mp4");
        VideoPlayer.loopPointReached += LoadScene;
    }
    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene("Web Prototype");
    }
}
