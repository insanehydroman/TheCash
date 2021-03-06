using System;
using UnityEngine;

public class ThirdPersonMover : MonoBehaviour
{
    [SerializeField] float _turnSpeed = 1000f;
    [SerializeField] private float _moveSpeed = 5;
    private Rigidbody _rb;
    Animator _animator;
    float _mouseMovement;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }


    void Update() => _mouseMovement += Input.GetAxis("Mouse X");

    void FixedUpdate()
    {
        transform.Rotate(0, _mouseMovement * Time.deltaTime * _turnSpeed, 0);
        _mouseMovement = 0f;
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            vertical *= 2;
            horizontal *= 2;
        }

        var velocity = new Vector3(horizontal, 0, vertical);
        velocity.Normalize();
        velocity *= _moveSpeed * Time.fixedDeltaTime;
        Vector3 offset = transform.rotation * velocity;
        _rb.MovePosition(transform.position + offset);

        _animator.SetFloat("Vertical", vertical, 0.1f, Time.fixedDeltaTime);
        _animator.SetFloat("Horizontal", horizontal, 0.1f, Time.fixedDeltaTime);
    }
}