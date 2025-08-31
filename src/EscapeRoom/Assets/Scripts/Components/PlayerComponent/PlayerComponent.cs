using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// PlayerComponent is a component that manages the player controller in the game.
/// </summary>
public class PlayerComponent : SceneComponent<PlayerController>
{
    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private InputActionReference[] inputActions;

    [SerializeField]
    private AudioSource movementAudioSource;

    [SerializeField]
    [Expandable]
    private PlayerSetting playerSetting;

    public PlayerController playerController { get; private set; }

    protected override PlayerController CreateControllerImpl()
    {
        playerController = new PlayerController(transform, animator, inputActions, characterController, movementAudioSource, playerSetting);
        return playerController;
    }

    private void Awake()
    {
        playerController = CreateController();
        playerController?.Initialize();
    }

    private void Update()
    {
        playerController?.Update();
    }

    private void OnDestroy()
    {
        playerController?.Dispose();
    }
}
