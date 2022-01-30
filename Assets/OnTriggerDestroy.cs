using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDestroy : MonoBehaviour

{
    public UnityEngine.GameObject triggerObject = null;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        
    }
    
    
        
    }

