using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isPlayerOne;
    private MultInput _input;
    // Start is called before the first frame update
    Rigidbody2D myBody;
    SpriteRenderer mySprite;
    public float movespeed;
    public float jumpForce;
    public float fastfallForce;
    public float wallJumpForce;
    public float dashSpeed;
    public float dashCooldown;
    float curDashCooldown;
    bool grounded;
    bool againstWall;
    public int maxAirJumps;
    int jumps;
    int facing = 1;
    Dictionary<string, bool> actions = new Dictionary<string, bool>()
    {
        { "moveRight", false },
        { "moveLeft", false },
        { "jump", false },
        { "dash", false },
        { "fastfall", false }
    };

    private void Awake()
    {
        _input = isPlayerOne ? MultInput.One : MultInput.Two;
    }

    private void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        jumps = maxAirJumps;
    }

    private void Update()
    {
        actions["moveLeft"] = _input.GetKey(InputKey.Left);
        actions["moveRight"] = _input.GetKey(InputKey.Right);

        if (_input.GetKeyDown(InputKey.Jump))
        {
            Jump();
        }

        if (!grounded && _input.GetKeyDown(InputKey.FastFall))
        {
            actions["fastfall"] = true;
        }
        else if (_input.GetKeyUp(InputKey.FastFall))
        {
            actions["fastfall"] = false;
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
    }

    void FixedUpdate()
    {
        foreach (KeyValuePair<string, bool> kvp in actions)
        {
            if (kvp.Key == "moveRight" && kvp.Value == true)
            {
                myBody.AddForce(new Vector2(movespeed, 0));
                mySprite.flipX = false;
                facing = 1;
            }

            if (kvp.Key == "moveLeft" && kvp.Value == true)
            {
                myBody.AddForce(new Vector2(-movespeed, 0));
                mySprite.flipX = true;
                facing = 0;
            }

            if (kvp.Key == "fastfall" && kvp.Value == true)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, 0);
                myBody.AddForce(new Vector2(0, -fastfallForce), ForceMode2D.Impulse);
            }
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
        else if (againstWall)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0);
            myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
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
            grounded = true;
            jumps = maxAirJumps;
        }
    }
}
