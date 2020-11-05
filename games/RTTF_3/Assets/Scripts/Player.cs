using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public Transform ground_check;
    public LayerMask ground;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveVelocity;
    private Animator _animator;
    private bool _jump;

    const float _gravity = 0.05f;
    const float _jumpPower = 0.5f;
    const float _groundCheckRadius = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), 0);
        _moveVelocity = moveInput.normalized * speed;
        _animator.SetFloat("X_Velo", Mathf.Abs(_moveVelocity.x));

        if(Input.GetAxisRaw("Vertical") > 0.1f)
        {
            _jump = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 movePosition = _rigidbody.position + _moveVelocity * Time.fixedDeltaTime;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(ground_check.position, _groundCheckRadius, ground);
        movePosition.y -= _gravity; //gravity
        foreach (Collider2D collider in colliders)
        {
            if(collider.gameObject != gameObject)
            {
                movePosition.y += _gravity;
                if(_jump)
                {
                    movePosition.y += _jumpPower;
                    _jump = false;
                }
            }
        }
        _rigidbody.MovePosition(movePosition);
    }
}
