using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMain : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    BoxCollider2D boxCollider;
    SpriteRenderer renderer;

    bool canStick;
    bool toSquare;
    bool toCircle;
    bool firstMagnetContact = true;
    bool canBounce = true;
    float zAxis;
    float bounciness;
    int maxNbBounces = 3;
    int currentBounces = 0;
    bool magnetTouchContactPoint;
    Vector2 magnetDirection;
    Vector3 magnetContactPoint;

    public Sprite[] sprites;
    public bool isCircle = true;
    public bool isMagnet = false;
    public float movementSpeed;
    public float bounceHeight;
    public float rollSpeed;
    public float rollRotationSpeed;
    public float collisionBounceHeight;
    public float magnetPower;

    const float gravityValue = 10;

    // Effects variables
    ParticleSystem slimeTrailEffect;
    ParticleSystem slimeLandEffect;
    Vector3 effectOffset = new Vector3(-0.5f, -3.5f, 0);
    Vector3 velocityBeforeFixedUpdate;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        renderer = gameObject.GetComponent<SpriteRenderer>();

        slimeTrailEffect = GameObject.Find("SlimeTrailEffect").GetComponent<ParticleSystem>();
        slimeLandEffect = GameObject.Find("SlimeLandEffect").GetComponent<ParticleSystem>();
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

        HandleSlimeTrail();
    }

    void FixedUpdate()
    {
        print(gravityValue);
        Debug.DrawLine(transform.position, transform.position + (Vector3.down + Vector3.right) * 2.2f, Color.blue);

        float _movementH = Input.GetAxis("Horizontal");

        if ((_movementH >= 0.2 || _movementH <= -0.2) && !isMagnet)
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

        // Save velocity before fixed update for correct pre-collision velocity
        // Used for effects placement
        velocityBeforeFixedUpdate = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (currentBounces < maxNbBounces && isCircle && canBounce)
        {
            currentBounces++;
            if (_collision.gameObject.layer == 6)
            {
                rb.velocity = _collision.contacts[0].normal * (collisionBounceHeight / Mathf.Pow(2, currentBounces - 1));
            }
            else
            {
                rb.velocity = new Vector2(velocityBeforeFixedUpdate.x, velocityBeforeFixedUpdate.y) + _collision.contacts[0].normal * (collisionBounceHeight / Mathf.Pow(2, currentBounces - 1));
            }
        }
        else
        {
            bounciness = collisionBounceHeight;
            currentBounces = 0;
            canBounce = false;
        }

        //Debug.DrawLine(_collision.transform.position, _collision.transform.position* _collision.contacts[0].normal.magnitude, Color.red);
        if (isMagnet)
        {
            magnetDirection = (Vector2)transform.position - (Vector2)transform.position + _collision.contacts[0].normal * -magnetPower; 
            magnetTouchContactPoint = true;
        }

        HandleSlimeLandEffect(_collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.DrawLine((Vector2) transform.position, (Vector2) transform.position + collision.contacts[0].normal*-3, Color.red);

    }

    private void OnTriggerStay2D(Collider2D _collision)
    {
        if (_collision.tag.Equals("Negative"))
        {

            if (!isCircle)
            {
                if (firstMagnetContact)
                {
                    rb.gravityScale = 0;
                    magnetContactPoint = _collision.gameObject.GetComponent<BoxCollider2D>().ClosestPoint(transform.position);
                    magnetDirection = new Vector3(transform.position.x + magnetContactPoint.x, transform.position.y + magnetContactPoint.y, 0);
                    firstMagnetContact = false;
                }
                if(!magnetTouchContactPoint)
                {
                    magnetDirection = new Vector3(transform.position.x + magnetContactPoint.x, transform.position.y + magnetContactPoint.y, 0);
                }

                isMagnet = true;
                AttachPlayer(magnetDirection);
            }
            else
            {
                isMagnet = false;
                firstMagnetContact = true;
                magnetTouchContactPoint = false;
                rb.gravityScale = gravityValue;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Negative") && isMagnet)
        {
            isMagnet = false;
            firstMagnetContact = true;
            magnetTouchContactPoint = false;
            rb.gravityScale = gravityValue;
        }
    }

    private void AttachPlayer(Vector3 magnetDirection)
    {
        rb.AddForce(magnetDirection, ForceMode2D.Impulse);
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

    private void HandleSlimeTrail()
    {
        if (rb.velocity.x >= 5 && isCircle)
        {
            slimeTrailEffect.transform.eulerAngles = Vector3.zero;
            slimeTrailEffect.transform.localScale = Vector3.one;
            slimeTrailEffect.transform.position = transform.position + effectOffset;
            slimeTrailEffect.Play();
        }
        else if (rb.velocity.x <= -5 && isCircle)
        {
            slimeTrailEffect.transform.eulerAngles = new Vector3(0, 0, 180);
            slimeTrailEffect.transform.localScale = new Vector3(1, -1, 1);
            slimeTrailEffect.transform.position = transform.position + new Vector3(-effectOffset.x, effectOffset.y, 0);
            slimeTrailEffect.Play();
        }
        else
        {
            slimeTrailEffect.Stop();
        }
    }

    private void HandleSlimeLandEffect(Collision2D collision)
    {
        if (velocityBeforeFixedUpdate.magnitude >= 5)
        {
            if (collision.contacts[0].normal.normalized.x == 1 || collision.contacts[0].normal.normalized.x == -1)
            {
                if (velocityBeforeFixedUpdate.x >= 25 || velocityBeforeFixedUpdate.x <= -25)
                {
                    switch (collision.contacts[0].normal.x)
                    {
                        case 1:
                            slimeLandEffect.transform.eulerAngles = new Vector3(0, 0, -90);
                            break;
                        case -1:
                            slimeLandEffect.transform.eulerAngles = new Vector3(0, 0, 90);
                            break;
                    }
                    slimeLandEffect.transform.position = new Vector3(transform.position.x + effectOffset.y * collision.contacts[0].normal.x, transform.position.y, 0);
                    slimeLandEffect.Play();
                }
            }
            if (collision.contacts[0].normal.normalized.y == 1 || collision.contacts[0].normal.normalized.y == -1)
            {
                if (velocityBeforeFixedUpdate.y >= 25 || velocityBeforeFixedUpdate.y <= -25)
                {
                    switch (collision.contacts[0].normal.y)
                    {
                        case 1:
                            slimeLandEffect.transform.eulerAngles = new Vector3(0, 0, 0);
                            break;
                        case -1:
                            slimeLandEffect.transform.eulerAngles = new Vector3(0, 0, 180);
                            break;
                    }
                    slimeLandEffect.transform.position = new Vector3(transform.position.x, transform.position.y + effectOffset.y * collision.contacts[0].normal.y, 0);
                    slimeLandEffect.Play();
                }
            }
        }
    }
}
