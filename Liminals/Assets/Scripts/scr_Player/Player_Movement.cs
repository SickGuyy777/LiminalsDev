using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private GameObject checkSphere;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private CharacterController controller;
    [SerializeField] private LayerMask groundMask;

    //private variables
    private bool isFlashlightOn;
    private bool isGrounded;
    private bool isSprinting;
    private float currentSpeed;
    private Vector3 velocity;

    private void Start()
    {
        currentSpeed = walkSpeed;
        flashlight.GetComponent<Light>().enabled = false;
    }

    private void Update()
    {
        //check if player is grounded
        if (Physics.CheckSphere(checkSphere.transform.position, 0.4f, groundMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        //gravity based off of ground check
        if (velocity.y < 0 && isGrounded)
        {
            velocity.y = -2f;
        }

        //movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move = Vector3.ClampMagnitude(move, 1);

        //first movement update based on speed and input
        controller.Move(currentSpeed * Time.deltaTime * move);
        //gravity update
        if (!isGrounded)
        {
            velocity.y += -9.81f * Time.deltaTime * 4f;
        }
        //final movement update based on velocity
        controller.Move(velocity * Time.deltaTime);
        //get all velocity of the controller
        Vector3 horizontalVelocity = transform.right * x + transform.forward * z;

        //sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }
        if (isSprinting && horizontalVelocity.magnitude > 0.3f)
        {
            currentSpeed = sprintSpeed;
        }
        //force-disables sprinting if the player is no longer moving but still holding down sprint key
        else if (isSprinting && horizontalVelocity.magnitude < 0.3f)
        {
            isSprinting = false;
        }
        else if (!isSprinting)
        {
            currentSpeed = walkSpeed;
        }

        //toggle flashlight on/off
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isFlashlightOn && flashlight.GetComponent<Light>().enabled)
            {
                flashlight.GetComponent<Light>().enabled = false;
                isFlashlightOn = false;
            }
            else if (!isFlashlightOn && !flashlight.GetComponent<Light>().enabled)
            {
                flashlight.GetComponent<Light>().enabled = true;
                isFlashlightOn = true;
            }
        }
    }
}