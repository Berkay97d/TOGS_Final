using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float rayDistance;
    [HideInInspector] public Rigidbody rigidbody;

    [SerializeField] private float horizontalMovementSpeed, verticalMovementSpeed;

    public FloatingJoystick _floatingJoystick;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase is TouchPhase.Began or TouchPhase.Moved)
            {
                Rotate();
            }
        }
        else
            rigidbody.velocity = Vector3.zero;
                
    }

    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
            Movement();
        else
        {
            rigidbody.angularVelocity = Vector3.zero;
        }
    }

    private void Movement()
    {
        rigidbody.velocity = new Vector3(_floatingJoystick.Horizontal*horizontalMovementSpeed, 0, _floatingJoystick.Vertical*verticalMovementSpeed);
    }

    private void Rotate()
    {
        var angle = Mathf.Atan2(_floatingJoystick.Horizontal, _floatingJoystick.Vertical) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }
}
