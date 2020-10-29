using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    Rigidbody2D myBody;
    public float movespeed;
    public float jumpForce;
    bool grounded;
    public int maxAirJumps;
    int jumps;
    int facing = 1;

    // Start is called before the first frame update
    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        jumps = maxAirJumps;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            myBody.AddForce(new Vector2(-movespeed, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            myBody.AddForce(new Vector2(movespeed, 0));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(myBody.velocity.)
            {

            }
            myBody.AddForce(new Vector2(movespeed, 0));
        }

        Facing();
    }

    private void Facing()
    {
        
    }

    void Jump()
    {
        if (grounded)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0);
            myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            grounded = false;
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
