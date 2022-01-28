using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public UnityEngine.Camera m_cam;
    public float m_parallaxFactor;
    // Start is called before the first frame update
    void Start()
    {
        m_cam = UnityEngine.Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += m_cam.velocity * Time.deltaTime * -m_parallaxFactor;
    }
}
