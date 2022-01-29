using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableScript : MonoBehaviour
{
    public float minimumSpeedToBreak;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && collision.GetComponent<CharacterMain>().isCircle == false && collision.GetComponent<Rigidbody2D>().velocity.magnitude > minimumSpeedToBreak)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
            Invoke("endCareer", 5);        
        }
        
    }

    private void endCareer()
    {
        Destroy(gameObject);
    }
}
