using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomScript : MonoBehaviour
{
    public float mushroomForce;
    bool readyToBounce = false;
    Vector2 BounceDirection;

    public Animator anim;

    private void Start()
    {
        BounceDirection = new Vector2(gameObject.transform.up.x, gameObject.transform.up.y);
        //BounceDirection.Normalize();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, BounceDirection * mushroomForce, Color.red);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && readyToBounce == true)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(BounceDirection * mushroomForce);
            //collision.gameObject.GetComponent<CharacterMain>().EmitSoundBounce();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            readyToBounce = true;
            anim.SetTrigger("MushBounce");
            collision.gameObject.GetComponent<CharacterMain>().EmitSoundBounce();
            collision.gameObject.GetComponent<CharacterMain>().canBounce = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
