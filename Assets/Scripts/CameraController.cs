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
    
    public float turnSpeed = 8.0f;		// Speed of camera turning when mouse moves in along an axis
    
    private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
    private bool isRotating;	// Is the camera being rotated?
    
    public void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        wheelInput = Input.GetAxis("Mouse ScrollWheel");
    
        if(Input.GetMouseButtonDown(1))
        {
	        // Get mouse origin
	        mouseOrigin = Input.mousePosition;
	        isRotating = true;
        }

        // Disable movements on button release
        if (!Input.GetMouseButton(1)) 
	        isRotating=false;
		
        // Rotate camera along X and Y axis
        if (isRotating)
        {
	        var pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

	        transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
	        transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
        }
    }
    
    public void FixedUpdate()
    {
        if (Math.Abs(horizontalInput) > 0 || Math.Abs(verticalInput) > 0)
        {
            transform.position += moveSpeed * new Vector3(horizontalInput, 0, verticalInput) *
                                  Time.deltaTime;
        }
    
        if (Math.Abs(wheelInput) > 0)
        {
            transform.position += scrollSpeed * new Vector3(0, -wheelInput, 0) * Time.deltaTime;
        }
    }
    
	
	
}
