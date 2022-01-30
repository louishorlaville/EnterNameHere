using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioIntroScript : MonoBehaviour
{
    public AK.Wwise.Event IntroVideo;

    // Start is called before the first frame update
    void Start()
    {
        IntroVideo.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
