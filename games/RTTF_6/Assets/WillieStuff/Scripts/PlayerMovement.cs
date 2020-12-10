using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask floorMask;
    public bool isPlayerOne;
    private MultInput _input;
    private Animator _animator;

    // Start is called before the first frame update
    Rigidbody2D myBody;
    SpriteRenderer mySprite;
    public float movespeed;
    public float jumpForce;
    public float fastfallForce;
    public float wallJumpUpForce;
    public float wallJumpSideMultiplier;
    public float dashSpeed;
    public float dashCooldown;
    float playerWidth = 1.2f;
    float curDashCooldown;
    bool grounded;
    bool againstWallR;
    bool againstWallL;
    public int maxAirJumps;
    int jumps;
    int facing = 1;

    bool aMoveRight;
    bool aMoveLeft;
    bool aJump;
    bool aDash;
    bool aFastfall;

    private void Awake()
    {
        floorMask = LayerMask.NameToLayer("Floor");
        _input = isPlayerOne ? MultInput.One : MultInput.Two;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        jumps = maxAirJumps;
    }

    private void Update()
    {
        aMoveLeft = _input.GetKey(InputKey.Left);
        aMoveRight = _input.GetKey(InputKey.Right);

        if (_input.GetKeyDown(InputKey.Jump))
        {
            Jump();
        }

        if (!grounded && _input.GetKeyDown(InputKey.FastFall))
        {
            aFastfall = true;
        }
        else if (_input.GetKeyUp(InputKey.FastFall))
        {
            aFastfall = false;
        }

        if (curDashCooldown <= 0 && _input.GetKeyDown(InputKey.Dash))
        {
            curDashCooldown = dashCooldown;
            if (facing > 0)
            {
                myBody.velocity = new Vector2(0, myBody.velocity.y);
                myBody.AddForce(new Vector2(dashSpeed, 0), ForceMode2D.Impulse);
            }
            else
            {
                myBody.velocity = new Vector2(0, myBody.velocity.y);
                myBody.AddForce(new Vector2(-dashSpeed, 0), ForceMode2D.Impulse);
            }
        }

        if (curDashCooldown > 0)
        {
            curDashCooldown -= Time.deltaTime;
        }


        //RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 1.2f, 1 << floorMask);
        //Debug.DrawRay(transform.position, Vector2.down * 1.2f, Color.red, 1f);
        //if (hit.collider != null)
        //{
        //    Debug.Log(hit.collider.gameObject);
        //    grounded = true;
        //    jumps = maxAirJumps;
        //}
        //else
        //{
        //    RaycastHit2D hitRight = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 5f, 1 << floorMask);
        //    RaycastHit2D hitLeft = Physics2D.Raycast(gameObject.transform.position, Vector2.left, 5f, 1 << floorMask);
        //    if (hitRight.collider != null)
        //        againstWallR = true;
        //    if (hitLeft.collider != null)
        //        againstWallL = true;

        //}

        //_animator.SetBool("isRunning", aMoveLeft || aMoveRight);
    }

    void FixedUpdate()
    {
        if (aMoveRight)
        {
            myBody.AddForce(new Vector2(movespeed, 0));
            mySprite.flipX = false;
            facing = 1;
        }
        if (aMoveLeft)
        {
            myBody.AddForce(new Vector2(-movespeed, 0));
            mySprite.flipX = true;
            facing = 0;
        }
        if (aFastfall)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0);
            myBody.AddForce(new Vector2(0, -fastfallForce), ForceMode2D.Impulse);
        }
    }

    void Jump()
    {
        if (grounded)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0);
            myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            grounded = false;
        }
        else if (againstWallR || againstWallL)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0);
            if (againstWallR)
            {
                myBody.AddForce((Vector2.left * wallJumpSideMultiplier + Vector2.up) * wallJumpUpForce, ForceMode2D.Impulse);
                againstWallR = false;
            }
            else
            {
                myBody.AddForce((Vector2.right * wallJumpSideMultiplier + Vector2.up) * wallJumpUpForce, ForceMode2D.Impulse);
                againstWallL = false;
            }
        }
        else if (jumps > 0)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0);
            myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            grounded = false;
            jumps--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, playerWidth, 1 << floorMask);
            Debug.DrawRay(transform.position, Vector2.down * playerWidth, Color.red, 1f);
            if (hit.collider != null)
            {
                grounded = true;
                jumps = maxAirJumps;
            }
            else
            {
                RaycastHit2D hitRight = Physics2D.Raycast(gameObject.transform.position, Vector2.right, 5f, 1 << floorMask);
                RaycastHit2D hitLeft = Physics2D.Raycast(gameObject.transform.position, Vector2.left, 5f, 1 << floorMask);
                if (hitRight.collider != null)
                    againstWallR = true;
                if (hitLeft.collider != null)
                    againstWallL = true;

            }
        }
    }
}
