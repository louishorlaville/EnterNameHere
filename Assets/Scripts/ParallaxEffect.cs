using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public UnityEngine.Camera m_cam;
    public float m_parallaxFactorX;
    public float m_parallaxFactorY;

    public float m_startTime = 2;
    public float m_counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_cam = UnityEngine.Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_counter < m_startTime) m_counter += Time.deltaTime;
        else transform.position += new Vector3(m_cam.velocity.x * m_parallaxFactorX, m_cam.velocity.y * m_parallaxFactorY, 0) * Time.deltaTime;
    }
}
