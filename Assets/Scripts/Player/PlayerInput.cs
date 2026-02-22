using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] 
    private float speed = 5.0f;

    [SerializeField] 
    private float lookSensitivity = 5.0f;

    [SerializeField]
    private float thrusterForce = 20.0f;

    [SerializeField] 
    private PlayerController playerController;

    private ConfigurableJoint joint;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        // 水平
        float xMov = Input.GetAxisRaw("Horizontal");
        // 垂直
        float yMov = Input.GetAxisRaw("Vertical");

        Vector3 velocity = (transform.forward * yMov + transform.right * xMov).normalized * speed;
        playerController.SetVelocity(velocity);

        // x 轴
        float xMouse = Input.GetAxisRaw("Mouse X");
        // y 轴
        float yMouse = Input.GetAxisRaw("Mouse Y");

        Vector3 xRotation = new Vector3(-yMouse * lookSensitivity, 0.0f, 0.0f) * lookSensitivity;
        Vector3 yRotation = new Vector3(0.0f, xMouse * lookSensitivity, 0.0f) * lookSensitivity;

        playerController.SetRotation(xRotation, yRotation);

        Vector3 force = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            force = Vector3.up * thrusterForce;
            joint.yDrive = new JointDrive
            {
                positionSpring = 0f,
                positionDamper = 0f,
                maximumForce = 0f
            };
        } else
        {
            joint.yDrive = new JointDrive
            {
                positionSpring = 20f,
                positionDamper = 0f,
                maximumForce = 40f
            };
        }

            playerController.Thrust(force);
    }
}
