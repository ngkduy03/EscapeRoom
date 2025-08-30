using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controller for player movement.
/// </summary>
public class PlayerMovementController : ControllerBase, IMovementController
{
    private readonly Transform transform;
    private readonly Animator animator;
    private readonly InputActionReference[] inputActions;
    private readonly CharacterController characterController;
    private readonly AudioSource movementAudioSource;
    private readonly PlayerSetting playerSetting;
    private float walkSpeed = 0f;
    private float walkAnimSpeed = 0f;
    private Vector3 moveDirection;
    private Vector3 cachedDirection;
    private bool isJumping = false;
    private Vector3 playerVelocity = new();
    private const string VelocityParam = "Velocity";
    private const string StateParam = "State";

    public PlayerMovementController(
        Transform transform,
        Animator animator,
        InputActionReference[] inputActions,
        CharacterController characterController,
        AudioSource movementAudioSource,
        PlayerSetting playerSetting)
    {
        this.transform = transform;
        this.animator = animator;
        this.inputActions = inputActions;
        this.characterController = characterController;
        this.movementAudioSource = movementAudioSource;
        this.playerSetting = playerSetting;
    }

    /// <summary>
    /// Initializes the player movement controller set up.
    /// </summary>
    public void Initialize()
    {
        movementAudioSource.Stop();
        movementAudioSource.loop = true;
    }

    ///<inheritdoc />
    public void Move()
    {
        // Get input direction and jump status.
        isJumping = inputActions[(int)PlayerInputActionEnum.Jump].action.triggered;

        var isSprinting = inputActions[(int)PlayerInputActionEnum.Sprint].action.ReadValue<float>() > 0;

        var direction = inputActions[(int)PlayerInputActionEnum.Move].action.ReadValue<Vector2>().normalized;
        moveDirection = new Vector3(direction.x, 0, direction.y);

        if (direction.magnitude > 0)
        {
            if (isSprinting)
            {
                // Accelerate movement.
                walkSpeed += playerSetting.SpeedAccel;
                walkAnimSpeed += playerSetting.AnimSpeedAccel;

                walkSpeed = Mathf.Clamp(walkSpeed, 0, playerSetting.MaxSprintSpeed);
                walkAnimSpeed = Mathf.Clamp(walkAnimSpeed, 0, playerSetting.SprintAnimValue);
            }
            else if (!isSprinting)
            {
                if (walkSpeed > playerSetting.MaxWalkSpeed || walkAnimSpeed > playerSetting.WalkAnimValue)
                {
                    walkSpeed -= playerSetting.SpeedAccel;
                    walkAnimSpeed -= playerSetting.AnimSpeedAccel;

                    walkSpeed = Mathf.Clamp(walkSpeed, playerSetting.MaxWalkSpeed, playerSetting.MaxSprintSpeed);
                    walkAnimSpeed = Mathf.Clamp(walkAnimSpeed, playerSetting.WalkAnimValue, playerSetting.SprintAnimValue);
                }
                else
                {
                    walkSpeed += playerSetting.SpeedAccel;
                    walkAnimSpeed += playerSetting.AnimSpeedAccel;

                    walkAnimSpeed = Mathf.Clamp(walkAnimSpeed, 0, playerSetting.WalkAnimValue);
                    walkSpeed = Mathf.Clamp(walkSpeed, 0, playerSetting.MaxWalkSpeed);
                }
            }

            // Rotate player.
            var rotation = Quaternion.Euler(0, Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * playerSetting.RotateSpeed);
        }
        else
        {
            // Decelerate movement.
            walkSpeed -= playerSetting.SpeedAccel * playerSetting.BreakForce;
            walkAnimSpeed -= playerSetting.AnimSpeedAccel / 10;

            walkAnimSpeed = Mathf.Clamp(walkAnimSpeed, 0, playerSetting.WalkAnimValue);
            walkSpeed = Mathf.Clamp(walkSpeed, 0, playerSetting.MaxWalkSpeed);
            movementAudioSource.Stop();
        }

        if (characterController.isGrounded)
        {
            animator.SetInteger(StateParam, (int)PlayerAnimationEnum.Idle);
            if (isJumping)
            {
                // Add jump force.
                JumpMomentum();
                cachedDirection = moveDirection;
            }
        }
        else
        {
            moveDirection = cachedDirection;
        }

        // Apply gravity.
        playerVelocity.y += playerSetting.GravityForce * Time.deltaTime;

        // Calculate final movement vector.
        var finalMove = (moveDirection * walkSpeed) + (playerVelocity.y * Vector3.up);
        characterController.Move(finalMove * Time.deltaTime);
        animator.SetFloat(VelocityParam, walkAnimSpeed);
        movementAudioSource.Play();
    }

    private void JumpMomentum()
    {
        playerVelocity.y = Mathf.Sqrt(playerSetting.JumpForce * -playerSetting.GravityForce);
        animator.SetInteger(StateParam, (int)PlayerAnimationEnum.Jump);
    }

    protected override void Dispose(bool disposing)
    {
        characterController.enabled = false;
        Array.Clear(inputActions, 0, inputActions.Length);
    }
}