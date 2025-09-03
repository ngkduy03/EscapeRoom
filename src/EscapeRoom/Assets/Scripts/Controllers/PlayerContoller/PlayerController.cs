using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private readonly AudioSource tabletAudioSource;
    private readonly InputActionReference[] inputActions;
    private readonly CharacterController characterController;
    private readonly PlayerSetting playerSetting;
    private readonly IEventBusService eventBusService;
    private readonly TMP_Text overText;
    private const string StateParam = "State";
    private const string Terminated = "Terminated";
    private const string Escaped = "Escaped";
    private IBehavior playerBehavior;

    public PlayerController(
        Transform transform,
        Animator animator,
        InputActionReference[] inputActions,
        CharacterController characterController,
        AudioSource movementAudioSource,
        AudioSource tabletAudioSource,
        PlayerSetting playerSetting,
        IEventBusService eventBusService,
        TMP_Text overText)
    {
        this.transform = transform;
        this.animator = animator;
        this.inputActions = inputActions;
        this.characterController = characterController;
        this.movementAudioSource = movementAudioSource;
        this.tabletAudioSource = tabletAudioSource;
        this.playerSetting = playerSetting;
        this.eventBusService = eventBusService;
        this.overText = overText;
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
            characterController.enabled = false;
            overText.text = Terminated;
            overText.color = Color.red;
            eventBusService?.TriggerEvent(new GameOverParam());
            animator.SetInteger(StateParam, (int)PlayerAnimationEnum.Death);
        }
        else if (other.TryGetComponent(out WinZone win))
        {
            characterController.enabled = false;
            overText.text = Escaped;
            overText.color = Color.yellow;
            eventBusService?.TriggerEvent(new GameOverParam());
        }
        else if (other.TryGetComponent(out KeyComponent tablet))
        {
            tabletAudioSource.Play();
        }
    }

    protected override void Dispose(bool disposing)
    {
        playerBehavior?.Dispose();
    }
}
