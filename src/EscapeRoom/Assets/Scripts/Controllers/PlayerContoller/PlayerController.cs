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
    private readonly IEventBusService eventBusService;
    private const string StateParam = "State";
    private IBehavior playerBehavior;

    public PlayerController(
        Transform transform,
        Animator animator,
        InputActionReference[] inputActions,
        CharacterController characterController,
        AudioSource movementAudioSource,
        PlayerSetting playerSetting,
        IEventBusService eventBusService)
    {
        this.transform = transform;
        this.animator = animator;
        this.inputActions = inputActions;
        this.characterController = characterController;
        this.movementAudioSource = movementAudioSource;
        this.playerSetting = playerSetting;
        this.eventBusService = eventBusService;
    }

    /// <summary>
    /// Initializes the player controller.
    /// </summary>
    public void Initialize()
    {
        var movementController = new PlayerMovementController(transform, animator, inputActions, characterController, movementAudioSource, playerSetting, eventBusService);
        movementController.Initialize();

        playerBehavior = new PlayerDefaultBehavior(movementController);
    }

    /// <summary>
    /// Updates the player behavior, which includes movement and attack actions.
    /// </summary>
    public void Update()
    {
        playerBehavior?.Update();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyComponent enemy))
        {
            eventBusService?.TriggerEvent(new GameOverParam());
            animator.SetInteger(StateParam, (int)PlayerAnimationEnum.Death);
        }
    }

    protected override void Dispose(bool disposing)
    {
        playerBehavior?.Dispose();
    }
}
