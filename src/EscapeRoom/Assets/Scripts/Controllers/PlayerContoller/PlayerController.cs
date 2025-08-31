using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controller for player.
/// </summary>
public class PlayerController : ControllerBase
{
    private readonly Transform transform;
    private readonly Animator animator;
    private readonly AudioSource movementAudioSource;
    private readonly InputActionReference[] inputActions;
    private readonly CharacterController characterController;
    private readonly PlayerSetting playerSetting;
    private IBehavior playerBehavior;

    public PlayerController(
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
    /// Initializes the player controller.
    /// </summary>
    public void Initialize()
    {
        var movementController = new PlayerMovementController(transform, animator, inputActions, characterController, movementAudioSource, playerSetting);

        playerBehavior = new PlayerDefaultBehavior(movementController);
    }

    /// <summary>
    /// Updates the player behavior, which includes movement and attack actions.
    /// </summary>
    public void Update()
    {
        playerBehavior?.Update();
    }

    protected override void Dispose(bool disposing)
    {
        playerBehavior?.Dispose();
    }
}
