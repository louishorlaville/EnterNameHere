using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public bool isCircle;
    public AK.Wwise.Event MyEvent;

    // Start is called before the first frame update
    void Start()
    {
        isCircle = true;
    }

    // Update is called once per frame
    void Update()
    {
        MyEvent.Post(gameObject);
    }
}
