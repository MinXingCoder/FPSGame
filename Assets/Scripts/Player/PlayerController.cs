using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera cam;
    
    private Vector3 velocity = Vector3.zero;                // 速度, 每秒移动的距离
    private Vector3 xRotation = Vector3.zero;             // 旋转角色
    private Vector3 yRotation = Vector3.zero;             // 旋转视角

    private float cameraRotationTotal = 0f;                 // 累计转了多少度
    [SerializeField]
    private float cameraRotationLimit = 85f;

    private Vector3 thrusterForce = Vector3.zero;          // 向上的推力

    private float lastY = 0f;

    private void FixedUpdate()
    {
        PlayerMovement();
        PlayerRotation();

        lastY = transform.position.y;
    }

    public void SetVelocity(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void SetRotation(Vector3 _xRotation, Vector3 _yRotation)
    {
        xRotation = _xRotation;
        yRotation = _yRotation;
    }

    public void Thrust(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }

    private void PlayerMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if(thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce);             // 作用 Time.fixedDeltaTime 秒: 0.02s, F = ma
            thrusterForce = Vector3.zero;
        }
    }

    private void PlayerRotation()
    {
        if (xRotation != Vector3.zero)
        {
            cameraRotationTotal += xRotation.x;
            cameraRotationTotal = Mathf.Clamp(cameraRotationTotal, -cameraRotationLimit, cameraRotationLimit);
            cam.transform.localEulerAngles = new Vector3(cameraRotationTotal, 0f, 0f);
        }

        if (yRotation != Vector3.zero)
        {
            rb.transform.Rotate(yRotation);
        }
    }
}
