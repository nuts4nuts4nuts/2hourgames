using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
<<<<<<< HEAD
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
=======
    public float speed;
    public Transform ground_check;
    public LayerMask ground;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveVelocity;
    private Animator _animator;

    const float _gravity = 1;
    const float _groundCheckRadius = 0.3f;
>>>>>>> badeb906e35d948295bdcb9374f32a2f39f2f508

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        myBody = gameObject.GetComponent<Rigidbody2D>();
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        jumps = maxAirJumps;
=======
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
>>>>>>> badeb906e35d948295bdcb9374f32a2f39f2f508
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
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
=======
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _moveVelocity = moveInput.normalized * speed;
        _animator.SetFloat("X_Velo", Mathf.Abs(_moveVelocity.x));
    }

    void FixedUpdate()
    {
        Vector3 movePosition = _rigidbody.position + _moveVelocity * Time.fixedDeltaTime;
        movePosition.y -= _gravity; //gravity

        Collider2D[] colliders = Physics2D.OverlapCircleAll(ground_check.position, _groundCheckRadius, ground);
        for(int i in )
        _rigidbody.MovePosition(movePosition);
>>>>>>> badeb906e35d948295bdcb9374f32a2f39f2f508
    }
}
