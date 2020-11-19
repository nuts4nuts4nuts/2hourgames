using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D myBody;
    SpriteRenderer mySprite;
    public float movespeed;
    public float jumpForce;
    public float wallJumpForce;
    public float dashSpeed;
    bool grounded;
    bool againstWall;
    public int maxAirJumps;
    int jumps;
    int facing = 1;
    Dictionary<string, bool> actions = new Dictionary<string, bool>()
    {
        {"moveRight", false },
        {"moveLeft", false },
        {"jump", false },
        {"dash", false },
    };

    // Start is called before the first frame update
    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        jumps = maxAirJumps;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            actions["moveLeft"] = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            actions["moveLeft"] = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            actions["moveRight"] = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            actions["moveRight"] = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
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

    }

    void FixedUpdate()
    {
        foreach(KeyValuePair<string, bool> kvp in actions)
        {
            if(kvp.Key == "moveRight" && kvp.Value == true)
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
