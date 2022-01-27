using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomScript : MonoBehaviour
{
    float mushHeight;
    public float mushroomForce;

    // Start is called before the first frame update
    void Start()
    {
        mushHeight = gameObject.GetComponent<Transform>().position.y + 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && mushHeight < collision.gameObject.GetComponent<Transform>().position.y)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, mushroomForce));
        }
    }
}
