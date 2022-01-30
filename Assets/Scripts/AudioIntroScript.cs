using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class AudioIntroScript : MonoBehaviour
{
    public AK.Wwise.Event IntroVideo;
    public VideoPlayer video;

    bool canCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        IntroVideo.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("CheckIfDone");
        if (canCheck && !video.isPlaying)
        {
            print(video.isPlaying);
            SceneManager.LoadScene("Level-1");
        }
    }

    public IEnumerator CheckIfDone()
    {
        yield return new WaitForSeconds(5f);
        canCheck = true;
    }
}
