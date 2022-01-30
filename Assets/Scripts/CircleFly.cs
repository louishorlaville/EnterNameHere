using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFly : MonoBehaviour
{
    public float flySpeed;
    public float flyRadius;

    private Vector2 m_startPosition;
    private float circleFlyAngle = 360;

    // Start is called before the first frame update
    void Start()
    {
        m_startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_startPosition + new Vector2(Mathf.Sin(circleFlyAngle), Mathf.Cos(circleFlyAngle)) * flyRadius;
        circleFlyAngle -= Time.deltaTime * flySpeed;
        if (circleFlyAngle <= 0) circleFlyAngle = 360;
    }
}
