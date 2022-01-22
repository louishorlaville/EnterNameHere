using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMain : MonoBehaviour
{
    Rigidbody2D rb;

    public string currentShape = "circle";
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float movementH = Input.GetAxis("Horizontal");
        rb.AddForce(new Vector2(movementH * movementSpeed, 0f), ForceMode2D.Force);
    }
}
