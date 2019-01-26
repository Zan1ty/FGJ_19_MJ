using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _controller;
    private Vector3 _mousePreviousPositon;
    private float _facing;

    void Start()
    {
        Cursor.visible = false;
        _controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        if (_controller.isGrounded)
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            _moveDirection = transform.TransformDirection(_moveDirection);
            _moveDirection *= speed;

            if (Input.GetButton("Jump"))
                _moveDirection.y = jumpSpeed;
        }
                
        _moveDirection.y -= (gravity * Time.deltaTime);
        _controller.Move(_moveDirection * Time.deltaTime);

        _facing += (Input.mousePosition.x - _mousePreviousPositon.x) * 2;
        _mousePreviousPositon = Input.mousePosition;
        transform.rotation = Quaternion.AngleAxis(_facing, Vector3.up);
    }
}
