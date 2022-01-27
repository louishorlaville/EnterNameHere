using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomScript : MonoBehaviour
{
    public float mushroomForce;
    bool readyToBounce = false;
    Vector2 BounceDirection;

    private void Start()
    {
        Debug.Log(gameObject.transform.up);
        BounceDirection = new Vector2(gameObject.transform.up.x, gameObject.transform.up.y);
        //BounceDirection.Normalize();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && readyToBounce == true)
        {
            Debug.Log("hmm");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(BounceDirection * mushroomForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            readyToBounce = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            readyToBounce = false;
        }
    }
}
