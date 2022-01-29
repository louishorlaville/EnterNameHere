using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    public GameObject LaserBeam;
    public float firingDuration;
    public float cooldownDuration;

    // Start is called before the first frame update
    void Start()
    {
        beginFiring();
    }

    void beginFiring()
    {
        LaserBeam.SetActive(true);
        Invoke("beginCooldown", firingDuration);
    }

    void beginCooldown()
    {
        LaserBeam.SetActive(false);
        Invoke("beginFiring", cooldownDuration);
    }
}