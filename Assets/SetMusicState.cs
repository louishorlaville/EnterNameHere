using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusicState : MonoBehaviour
{
    public AK.Wwise.State OnTriggerEnterState;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerEnterState.SetValue();
        }
    }
}
