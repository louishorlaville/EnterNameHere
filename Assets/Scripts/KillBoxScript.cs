using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBoxScript : MonoBehaviour
{
    public AK.Wwise.Event DeathLaser;
    private void OnTriggerEnter2D(Collider2D _target)
    {
        if(_target.gameObject.tag == "Player")
        {
            DeathLaser.Post(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}