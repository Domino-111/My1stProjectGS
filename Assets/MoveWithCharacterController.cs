using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCharacterController : MonoBehaviour
{
    public float walkSpeed;
    public float sprintSpeed;
    public float crouchSpeed;
    public float gravity;
    public float jumpPower;
    


    public Vector3 moveDirection;
    public Vector2 input;

    // Update is called once per frame
    void Update()
    {
        //Get inputs for this frame
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        //Apply those inputs to our horizontal plane
        moveDirection.x = input.x * walkSpeed;
        moveDirection.z = input.y * walkSpeed;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection.x = input.x * sprintSpeed;
            moveDirection.z = input.y * sprintSpeed;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            moveDirection.x = input.x * crouchSpeed;
            moveDirection.z = input.y * crouchSpeed;
        }

        //Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        //If our character controller detects ground below it
        if(GetComponent<CharacterController>().isGrounded)
        {
            //Clamp our vertical movement so we're not constantly "falling"
            moveDirection.y = Mathf.Clamp(moveDirection.y, -gravity, float.PositiveInfinity);

            //If the player presses jump... they should jump
            if(Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpPower;
            }
        }

        //Move based on moveDirection using time
        GetComponent<CharacterController>().Move(moveDirection * Time.deltaTime);
    }
}
