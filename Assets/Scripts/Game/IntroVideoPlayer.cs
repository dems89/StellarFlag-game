using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroVideoPlayer : MonoBehaviour
{
    private VideoPlayer intro;
    private bool videoSkipped = false;

    private void Awake()
    {
        intro = GetComponent<VideoPlayer>();
        HUDManager.Instance.SetHUD(HUDType.skip);
    }

    private void Start()
    {
        intro.targetCamera = Camera.main;
        intro.loopPointReached += OnVideoEnd;
        intro.Play();        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && intro.isPlaying)
        {
            SkipVideo();
        }
    }

    private void SkipVideo()
    {
        videoSkipped = true;
        intro.Stop();
        SceneManager.LoadScene("Level1");
        gameObject.SetActive(false);
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        if (!videoSkipped)
        {
            intro.Stop();
            SceneManager.LoadScene("Level1");
            gameObject.SetActive(false);
        }
    }
}
