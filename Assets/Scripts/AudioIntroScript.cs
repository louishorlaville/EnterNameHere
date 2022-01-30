using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class AudioIntroScript : MonoBehaviour
{
    public VideoPlayer video;
    public string toScene;

    bool canCheck = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("CheckIfDone");
        if (canCheck && !video.isPlaying)
        {
            print(video.isPlaying);
            SceneManager.LoadScene(toScene);
        }
    }

    public IEnumerator CheckIfDone()
    {
        yield return new WaitForSeconds(5f);
        canCheck = true;
    }
}
