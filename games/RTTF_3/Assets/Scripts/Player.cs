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

    const float _gravity = 1;
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
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _moveVelocity = moveInput.normalized * speed;
        _animator.SetFloat("X_Velo", Mathf.Abs(_moveVelocity.x));
    }

    void FixedUpdate()
    {
        Vector3 movePosition = _rigidbody.position + _moveVelocity * Time.fixedDeltaTime;
        movePosition.y -= _gravity; //gravity

        _rigidbody.MovePosition(movePosition);
    }
}
