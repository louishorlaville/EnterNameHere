using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    public GameObject LaserBeam;
    public float firingDuration;
    public float cooldownDuration;
    public AK.Wwise.Event LaserSound;

    // Start is called before the first frame update
    void Start()
    {
        beginFiring();
    }

    void beginFiring()
    {
        LaserBeam.SetActive(true);
        Invoke("beginCooldown", firingDuration);
        LaserSound.Post(gameObject);
    }

    void beginCooldown()
    {
        LaserBeam.SetActive(false);
        Invoke("beginFiring", cooldownDuration);
    }
}