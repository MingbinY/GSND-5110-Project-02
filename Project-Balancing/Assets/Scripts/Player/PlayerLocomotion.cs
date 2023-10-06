using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playerVelocity;
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public bool isSprinting = false;

    private bool isGrounded;
    public float gravity = -9.8f;

    public float jumpHeight = 3f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = characterController.isGrounded;
    }

    public void HandleMove(Vector2 input)
    {
        Vector3 moveDir = Vector3.zero;

        float targetSpeed = isSprinting ? sprintSpeed : walkSpeed;

        moveDir.x = input.x;
        moveDir.z = input.y;

        characterController.Move(transform.TransformDirection(moveDir) * targetSpeed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void HandleJump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
    }
}
