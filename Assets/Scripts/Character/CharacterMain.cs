using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMain : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    BoxCollider2D boxCollider;
    SpriteRenderer renderer;
    MenusScript menusScriptRef;
    List<CircleCollider2D> cubeCorners = new List<CircleCollider2D>();

    bool canStick;
    bool toSquare;
    bool toCircle;
    bool firstMagnetContact = true;
    float zAxis;
    float bounciness;
    int maxNbBounces = 3;
    int currentBounces = 0;
    bool magnetTouchContactPoint;
    Vector2 magnetDirection;
    Vector3 magnetContactPoint;

    public GameObject pauseMenu;
    public Sprite[] sprites;
    public bool isCircle = true;
    public bool isMagnet = false;
    public bool bPauseMenu = false;
    public bool bEndLevel = false;
    public bool canBounce = true;
    public float movementSpeed;
    public float bounceHeight;
    public float rollSpeed;
    public float rollRotationSpeed;
    public float collisionBounceHeight;
    public float magnetPower;
    
    public AK.Wwise.Event SlimeMovement;
    public AK.Wwise.Event CubeMovement;
    public AK.Wwise.Event SlimeJump;
    public AK.Wwise.RTPC SpeedSlime;
    public AK.Wwise.Event SlimetoCube;
    public AK.Wwise.Event CubetoSlime;

    //Wwise
    private bool movementIsPlaying = false;
    private float lastSlimeMovementTime = 0;
    private float lastCubeMovementTime = 0;

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

        menusScriptRef = pauseMenu.GetComponent<MenusScript>();

        slimeTrailEffect = GameObject.Find("SlimeTrailEffect").GetComponent<ParticleSystem>();
        slimeLandEffect = GameObject.Find("SlimeLandEffect").GetComponent<ParticleSystem>();

        bEndLevel = false;

        lastCubeMovementTime = Time.time;
        lastSlimeMovementTime = Time.time;

        SpeedSlime.SetGlobalValue(0);

        foreach (Transform child in transform)
        {
            cubeCorners.Add(child.gameObject.GetComponent<CircleCollider2D>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        //Check inputs for shape switch
        toSquare = Input.GetKeyDown("space");
        toCircle = Input.GetKeyUp("space");

        if(bPauseMenu == false)
        {
            if(bEndLevel == false)
            {
                if(isCircle == true)
                {
                    if(Input.GetKey("space"))
                    {
                        SwitchToSquare();
                    }
                }
                else
                {
                    if(Input.GetKeyUp("space"))
                    {
                        //BounceAlongNormal();
                        SwitchToCircle();
                    }
                }
            }
        }

        if(Input.GetKeyDown("escape"))
        {
			if(bPauseMenu == false)
			{
                bPauseMenu = true;
                menusScriptRef.OpenPauseMenu();
			}
			else
			{
                bPauseMenu = false;
                menusScriptRef.ResumeGame();
            }
        }

        HandleSlimeTrail();
    }

    void FixedUpdate()
    {
        float _movementH = Input.GetAxis("Horizontal");
        if(bPauseMenu == false)
        {
            if(bEndLevel == false)
            {
                if((_movementH >= 0.2 || _movementH <= -0.2) && !isMagnet)
                {
                    if(isGrounded() && isCircle)
                    {
                        rb.AddForce(new Vector2(_movementH * movementSpeed, 0f), ForceMode2D.Impulse);

                        if(movementIsPlaying == false)
                        {
                            if(isCircle == true)
                            {
                                SlimeMovement.Post(gameObject);
                                lastSlimeMovementTime = Time.time;
                            }
                            else
                            {
                                CubeMovement.Post(gameObject);
                                lastCubeMovementTime = Time.time;
                            }

                            movementIsPlaying = true;
                        }
                        else
                        {
                            if(_movementH > 0.2f)
                            {
                                if(Time.time - lastSlimeMovementTime > 50 / _movementH * Time.deltaTime)
                                {
                                    movementIsPlaying = false;
                                }
                            }
                            else
                            {
                                if(Time.time - lastSlimeMovementTime > 50 / -_movementH * Time.deltaTime)
                                {
                                    movementIsPlaying = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        zAxis += (Time.deltaTime * _movementH * rollRotationSpeed);
                        transform.rotation = Quaternion.Euler(0, 0, -zAxis);

                        if (isGrounded())
                        {
                            //Prevent momentum loss on single space bar press
                            if(rb.velocity.x < -rollSpeed || rb.velocity.x > rollSpeed)
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

                if(_movementH > 0.2f)
                {
                    SpeedSlime.SetGlobalValue(2f * _movementH);
                }
                else
                {
                    SpeedSlime.SetGlobalValue(2f * -_movementH);
                }

                // Save velocity before fixed update for correct pre-collision velocity
                // Used for effects placement
                velocityBeforeFixedUpdate = rb.velocity;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (isCircle && canBounce)
        {
            EmitSoundBounce();

            if (currentBounces < maxNbBounces)
            {
                Vector2 _from = new Vector2(velocityBeforeFixedUpdate.x, velocityBeforeFixedUpdate.y);
                Vector2 normal = _collision.contacts[0].normal;
                currentBounces++;
                if (_collision.collider.gameObject.layer == 6)
                {
                    rb.velocity = normal * (collisionBounceHeight / Mathf.Pow(2, currentBounces - 1));
                }
                else
                {
                    rb.velocity = new Vector2(velocityBeforeFixedUpdate.x, velocityBeforeFixedUpdate.y) + _collision.contacts[0].normal * (collisionBounceHeight / Mathf.Pow(2, currentBounces - 1));
                }
            }
            else
            {
                currentBounces = 0;
                canBounce = false;
            }

            //Debug.DrawLine(_collision.transform.position, _collision.transform.position* _collision.contacts[0].normal.magnitude, Color.red);
           
        }

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCircle)
        {
            magnetContactPoint = (Vector2) collision.gameObject.GetComponent<BoxCollider2D>().ClosestPoint(transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Negative"))
        {
            if (!isCircle)
            {             
                isMagnet = true;

                if (firstMagnetContact)
                {
                    firstMagnetContact = false;
                    rb.gravityScale = 0;
                    magnetContactPoint = (Vector2)collision.gameObject.GetComponent<BoxCollider2D>().ClosestPoint(transform.position);
                }
                if(!magnetTouchContactPoint)
                {
                    magnetDirection = new Vector2(magnetContactPoint.x - transform.position.x, magnetContactPoint.y - transform.position.y);
                }
                //Debug.DrawRay(transform.position, collision.gameObject.GetComponent<BoxCollider2D>().ClosestPoint(transform.position), Color.red);

                AttachPlayer(magnetDirection);
            }
            else if(isMagnet)
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

    public void SwitchToSquare()
    {
        boxCollider.enabled=true;
        circleCollider.enabled = false;
        isCircle = false;
        renderer.sprite = sprites[1];
        canBounce = false;

        SlimetoCube.Post(gameObject);
    }

    private void SwitchToCircle()
    {
        boxCollider.enabled = false;
        circleCollider.enabled = true;
        isCircle = true;
        renderer.sprite = sprites[0];
        currentBounces = 0;
        canBounce = true;

        isMagnet = false;
        firstMagnetContact = true;
        magnetTouchContactPoint = false;
        rb.gravityScale = gravityValue;

        CubetoSlime.Post(gameObject);
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

    public float getAngle(Vector2 normal, Vector2 incident)
    {
        float angle;
        incident = -incident;
        angle = Mathf.Acos((normal.x * incident.x + normal.y * incident.y) / Mathf.Sqrt((Mathf.Pow(normal.x, 2) + Mathf.Pow(normal.y, 2)) * (Mathf.Pow(incident.x, 2) + Mathf.Pow(incident.y, 2))));
        angle = angle * 180 / Mathf.PI;

        return angle;
    }

    public void EmitSoundBounce()
    {
        SlimeJump.Post(gameObject);
    }

    public void EmitSoundCubeMovement()
    {
        CubeMovement.Post(gameObject);
    }
}
