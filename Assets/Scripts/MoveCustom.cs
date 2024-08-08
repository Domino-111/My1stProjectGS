using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;



public class MoveCustom : MonoBehaviour
{
    //How fast we should move when walking
    public float walkSpeed;
    //How fast we should move when running
    public float runSpeed;
    //How fast we should move while crouching
    public float crouchSpeed;
    //How strong our jump should be
    public float jumpPower;
    //How strong gravity should be
    public float gravity;

    //To store how fast we should move this frame
    public float speedThisFrame;

    //To store what our left-right-up-down inputs are this frame
    public Vector2 inputThisFrame;

    //To store our overall movement this frame
    public Vector3 movementThisFrame;

    //to store a reference to the object's rigidbody
    public Rigidbody rb;

    //Define which layers are considered solid ground
    public LayerMask groundedMask;

    void Start()
    {
        //Get the rigidbody component and save it in the variable
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Get out inputs this frame
        inputThisFrame.x = Input.GetAxis("Horizontal");
        inputThisFrame.y = Input.GetAxis("Vertical");

        //Reset our potential movement to 0, 0, 0
        movementThisFrame = Vector3.zero;

        //Apply our new input direction right/left and forward/back
        movementThisFrame.x = inputThisFrame.x;
        movementThisFrame.z = inputThisFrame.y;

        //Figure out what our speed should be this frame
        speedThisFrame = walkSpeed;

        //If the "Sprint" button is being held
        if (Input.GetButton("Sprint"))
        {
            speedThisFrame = runSpeed;
        }

        //If the "Crouch" button is being held
        if (Input.GetButton("Crouch"))
        {
            speedThisFrame = crouchSpeed;
        }

        //Multiply movement this frame by speed this frame
        movementThisFrame *= speedThisFrame;

        //Recall the up/down speed we were at from the rigidbody and apply gravity
        movementThisFrame.y  = rb.velocity.y - gravity * Time.deltaTime;

        //Check if we're on the ground
        if (IsGrounded())
        {
            //If we press the "Jump" button
            if (Input.GetButton("Jump"))
            {
                movementThisFrame.y = jumpPower;
            }
        }

        //Call our Move function
        Move(movementThisFrame);
    }

    private void Move(Vector3 movement)
    {
        //Set our rigidbody's velocity using the incoming movement value
        rb.velocity = movement;
    }

    private bool IsGrounded()
    {
        //Return the result of a raycast (true or false)
        return Physics.Raycast(transform.position, Vector3.down, 1.05f, groundedMask);
    }
}
