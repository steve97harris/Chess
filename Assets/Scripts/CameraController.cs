using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float scrollSpeed = 100f;

    private float horizontalInput;
    private float verticalInput;
    private float wheelInput;

    public void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        wheelInput = Input.GetAxis("Mouse ScrollWheel");
    }

    public void FixedUpdate()
    {
        if (Math.Abs(horizontalInput) > 0 || Math.Abs(verticalInput) > 0)
        {
            transform.position +=
                moveSpeed * new Vector3(horizontalInput, 0, verticalInput) *
                Time.deltaTime;
        }

        if (Math.Abs(wheelInput) > 0)
        {
            transform.position += scrollSpeed * new Vector3(0, -wheelInput, 0) * Time.deltaTime;
        }
    }
}
