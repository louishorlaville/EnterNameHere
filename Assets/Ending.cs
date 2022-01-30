using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public AK.Wwise.Event MyEvent;
    // Use this for initialization..
    public void Ending()
    {
        MyEvent.Post(gameObject);
    }
}
