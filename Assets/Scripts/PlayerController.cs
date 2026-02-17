using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rd;
    [SerializeField] private Camera cam;
    
    private Vector3 velocity = Vector3.zero;
    private Vector3 xRotation = Vector3.zero;
    private Vector3 yRotation = Vector3.zero;

    private void FixedUpdate()
    {
        PlayerMovement();
        PlayerRotation();
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

    private void PlayerMovement()
    {
        if (velocity != Vector3.zero)
        {
            rd.MovePosition(rd.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void PlayerRotation()
    {
        if (xRotation != Vector3.zero)
        {
            cam.transform.Rotate(xRotation);
        }

        if (yRotation != Vector3.zero)
        {
            rd.transform.Rotate(yRotation);
        }
    }
}
