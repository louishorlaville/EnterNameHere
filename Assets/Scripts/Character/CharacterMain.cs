using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMain : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    BoxCollider2D boxCollider;
    SpriteRenderer renderer;
    Vector3 launchVector;

    bool toSquare;
    bool toCircle;

    public Sprite[] sprites;
    public bool isCircle = true;
    public float movementSpeed;
    public float bounceHeight;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        renderer = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //Check inputs for shape switch
        toSquare = Input.GetKeyDown("space");
        toCircle = Input.GetKeyUp("space");

        if (Input.GetKey("space"))
        {
            SwitchToSquare();
            print("to square: " + toSquare);

        }

        if (Input.GetKeyUp("space"))
        {
            BounceAlongNormal();
            SwitchToCircle();
            print("to circle: " + toCircle);
        }
    }

    void FixedUpdate()
    {
        float movementH = Input.GetAxis("Horizontal");
        rb.AddForce(new Vector2(movementH * movementSpeed, 0f), ForceMode2D.Impulse);
    }

    private void SwitchToSquare()
    {
        boxCollider.enabled=true;
        circleCollider.enabled = false;

        renderer.sprite = sprites[1];
    }

    private void SwitchToCircle()
    {
        boxCollider.enabled = false;
        circleCollider.enabled = true;

        renderer.sprite = sprites[0];  
    }

    public void BounceAlongNormal()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.up), 2.8f);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.down), 2.8f);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.left), 2.8f);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.right), 2.8f);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 2.8f, Color.red);

        if (hitUp.collider!=null)
        {
            Debug.Log("Up Hit: "+ hitUp.collider.gameObject.name);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up)*5, Color.red);
            
            Debug.DrawRay(new Vector3(hitUp.normal.x, hitUp.normal.y, 0), hitUp.collider.transform.TransformDirection(hitUp.normal)*20, Color.red);

            //convert normal direction to force
            hitUp.normal.Normalize();
            rb.AddForce(hitUp.normal*bounceHeight, ForceMode2D.Impulse);
        }
        if (hitDown.collider != null)
        {
            Debug.Log("Up Hit: " + hitDown.collider.gameObject.name);
            Debug.DrawRay(new Vector3(hitDown.normal.x, hitDown.normal.y, 0), hitDown.collider.transform.TransformDirection(hitDown.normal) * 20, Color.red);

            //convert normal direction to force
            hitDown.normal.Normalize();
            rb.AddForce(hitDown.normal * bounceHeight, ForceMode2D.Impulse);
        }
        if (hitLeft.collider != null)
        {
            Debug.Log("Up Hit: " + hitLeft.collider.gameObject.name);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 5, Color.red);

            Debug.DrawRay(new Vector3(hitLeft.normal.x, hitLeft.normal.y, 0), hitLeft.collider.transform.TransformDirection(hitLeft.normal) * 20, Color.red);

            //convert normal direction to force
            hitLeft.normal.Normalize();
            rb.AddForce(hitLeft.normal * bounceHeight, ForceMode2D.Impulse);
        }
        if (hitRight.collider != null)
        {
            Debug.Log("Up Hit: " + hitRight.collider.gameObject.name);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 5, Color.red);

            Debug.DrawRay(new Vector3(hitRight.normal.x, hitRight.normal.y, 0), hitRight.collider.transform.TransformDirection(hitRight.normal) * 20, Color.red);

            //convert normal direction to force
            hitRight.normal.Normalize();
            rb.AddForce(hitRight.normal * bounceHeight, ForceMode2D.Impulse);
        }

    }

}
