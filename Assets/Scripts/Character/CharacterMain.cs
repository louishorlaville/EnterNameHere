using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMain : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    BoxCollider2D boxCollider;
    SpriteRenderer renderer;

    bool toSquare;
    bool canBounce=true;
    bool toCircle;
    float zAxis;
    float bounciness;
    int maxNbBounces = 3;
    int currentBounces = 0;

    public Sprite[] sprites;
    public bool isCircle = true;
    public float movementSpeed;
    public float bounceHeight;
    public float rollSpeed;
    public float rollRotationSpeed;
    public float collisionBounceHeight;

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
            
        }

        if (Input.GetKeyUp("space"))
        {
            //BounceAlongNormal();
            SwitchToCircle();
            
        }
    }

    void FixedUpdate()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3.down + Vector3.right) * 2.2f, Color.blue);

        float _movementH = Input.GetAxis("Horizontal");

        if (_movementH >= 0.2 || _movementH <= -0.2)
        {
            if (isGrounded() && isCircle)
            {
                rb.AddForce(new Vector2(_movementH * movementSpeed, 0f), ForceMode2D.Impulse);
            }
            else
            {
                zAxis += (Time.deltaTime * _movementH * rollRotationSpeed);
                transform.rotation = Quaternion.Euler(0, 0, -zAxis);

                if (isGrounded())
                {
                    //Prevent momentum loss on single space bar press
                    if (rb.velocity.x < -rollSpeed || rb.velocity.x > rollSpeed)
                    {
                        rb.velocity = new Vector2(rb.velocity.x * 0.99f, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(rollSpeed * _movementH, rb.velocity.y);
                    }
                }
            }
        }
        else
        {
            rb.AddForce(new Vector2(0f, 0f), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if (currentBounces<maxNbBounces && isCircle && canBounce)
        {
            currentBounces++;
            bounciness = collisionBounceHeight / currentBounces;
            collision.contacts[0].normal.Normalize();
            rb.velocity = collision.contacts[0].normal* (collisionBounceHeight / Mathf.Pow(2, currentBounces-1));
        }
        else
        {
            bounciness = collisionBounceHeight;
            currentBounces = 0;
            canBounce = false;
        }
    }

    //Check if player is touching a surface
    public bool isGrounded()
    {
        bool _isGrounded;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 4);
        _isGrounded = hit.collider != null;

        return _isGrounded;
    }

    private void SwitchToSquare()
    {
        boxCollider.enabled=true;
        circleCollider.enabled = false;
        isCircle = false;
        renderer.sprite = sprites[1];
        canBounce = false;
    }

    private void SwitchToCircle()
    {
        boxCollider.enabled = false;
        circleCollider.enabled = true;
        isCircle = true;
        renderer.sprite = sprites[0];
        currentBounces = 0;
        canBounce = true;
    }

    //Casts a circleCast to detect which side is on the ground and launch player in surface normal direction
    /*public void BounceAlongNormal()
    {
        RaycastHit2D circleCast = Physics2D.CircleCast(transform.position, 3f, new Vector2(0,0));
        if (circleCast.collider != null)
        {
            Debug.DrawRay(new Vector3(circleCast.normal.x, circleCast.normal.y, 0), circleCast.collider.transform.TransformDirection(circleCast.normal) * 20, Color.red);

            //convert normal direction to force
            circleCast.normal.Normalize();
            rb.AddForce(circleCast.normal * bounceHeight, ForceMode2D.Impulse);
        }
    }*/
}
