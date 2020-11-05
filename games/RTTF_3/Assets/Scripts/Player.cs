using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D myBody;
    SpriteRenderer mySprite;
    public float movespeed;
    public float jumpForce;
    public float dashSpeed;
    bool grounded;
    public int maxAirJumps;
    int jumps;
    int facing = 1;

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
            myBody.AddForce(new Vector2(-movespeed, 0));
            mySprite.flipX = true;
            facing = 0;
        }
        if (Input.GetKey(KeyCode.D))
        {
            myBody.AddForce(new Vector2(movespeed, 0));
            mySprite.flipX = false;
            facing = 1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.LeftShift))
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
