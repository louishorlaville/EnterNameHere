using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowBabyGlow : MonoBehaviour
{
    public float glowSpeed;
    public float minOpacityFactor;
    public float maxOpacityFactor;
    public float minScaleFactor;
    public float maxScaleFactor;

    private SpriteRenderer SR;

    private float timeCounter = 0;

    private float minOpacity;
    private float maxOpacity;

    private float lerpToOpacity;
    private float lerpFromOpacity;

    private Vector3 minScale;
    private Vector3 maxScale;

    private Vector3 lerpToScale;
    private Vector3 lerpFromScale;
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();

        minScale = transform.localScale * minScaleFactor;
        lerpFromScale = minScale;
        maxScale = transform.localScale * maxScaleFactor;
        lerpToScale = maxScale;

        minOpacity = SR.color.a * minOpacityFactor;
        lerpToOpacity = minOpacity;
        maxOpacity = SR.color.a * maxOpacityFactor;
        lerpFromOpacity = maxOpacity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(lerpFromScale, lerpToScale, timeCounter);
        Color colorBuffer = SR.color;
        colorBuffer.a = Mathf.Lerp(lerpFromOpacity, lerpToOpacity, timeCounter);
        SR.color = colorBuffer;
        timeCounter += Time.deltaTime * glowSpeed/10;
        if(timeCounter >= 1)
        {
            timeCounter = 0;

            Vector3 scaleBuffer = lerpFromScale;
            lerpFromScale = lerpToScale;
            lerpToScale = scaleBuffer;

            float opacityBuffer = lerpFromOpacity;
            lerpFromOpacity = lerpToOpacity;
            lerpToOpacity = opacityBuffer;
        }


    }
}
