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
    float zAxis;

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

        float _movementH = Input.GetAxis("Horizontal");
        if(_movementH >= 0.1 || _movementH <= 0.1)
        {
            if (isGrounded())
            {
                rb.AddForce(new Vector2(_movementH * movementSpeed, 0f), ForceMode2D.Impulse);
            }
            else
            {
                zAxis += (Time.deltaTime * _movementH * 150);
                transform.rotation = Quaternion.Euler(0, 0, -zAxis);
            }
        }

    }

    //Check if player is touching a surface
    public bool isGrounded()
    {
        bool _isGrounded;

        RaycastHit2D circleCast = Physics2D.CircleCast(transform.position, 4, new Vector2(0, 0));
        _isGrounded = circleCast.collider != null;

        return _isGrounded;
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

    //Casts a circleCast to detect which side is on the ground and launch player in surface normal direction
    public void BounceAlongNormal()
    {
        RaycastHit2D circleCast = Physics2D.CircleCast(transform.position, 3f, new Vector2(0,0));
        if (circleCast.collider != null)
        {
            Debug.DrawRay(new Vector3(circleCast.normal.x, circleCast.normal.y, 0), circleCast.collider.transform.TransformDirection(circleCast.normal) * 20, Color.red);

            //convert normal direction to force
            circleCast.normal.Normalize();
            rb.AddForce(circleCast.normal * bounceHeight, ForceMode2D.Impulse);
        }

    }

}
