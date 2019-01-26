using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera head;

    public float sensitivityX = 15f;
    public float sensitivityY = 15f;

    public float minimumX = -360f;
    public float maximumX = 360F;

    public float minimumY = -60f;
    public float maximumY = 60f;

    private List<float> rotArrayX = new List<float>();
    float rotAverageX = 0f;

    private List<float> rotArrayY = new List<float>();
    float rotAverageY = 0f;

    public float frameCounter = 20;

    Vector3 _moveDirection;
    float _rotationX;
    float _rotationY;
    private CharacterController _controller;

    void Start()
    {
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

        rotAverageY = 0f;
        rotAverageX = 0f;

        _rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        _rotationX += Input.GetAxis("Mouse X") * sensitivityX;

        rotArrayY.Add(_rotationY);
        rotArrayX.Add(_rotationX);

        if (rotArrayY.Count >= frameCounter)
            rotArrayY.RemoveAt(0);
        if (rotArrayX.Count >= frameCounter)
            rotArrayX.RemoveAt(0);

        for (int j = 0; j < rotArrayY.Count; j++)
            rotAverageY += rotArrayY[j];
        for (int i = 0; i < rotArrayX.Count; i++)
            rotAverageX += rotArrayX[i];

        rotAverageY /= rotArrayY.Count;
        rotAverageX /= rotArrayX.Count;

        rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
        rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

        Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);

        transform.localRotation = xQuaternion;
        head.transform.localRotation = yQuaternion;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
