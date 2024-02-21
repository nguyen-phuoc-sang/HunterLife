using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.Play();
    }

    private void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene("Start"); // Thay "MainGameScene" bằng tên Scene chứa trò chơi chính của bạn
    }
}