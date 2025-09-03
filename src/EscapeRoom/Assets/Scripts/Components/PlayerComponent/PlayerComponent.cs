using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
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
    private AudioSource tabletAudioSource;

    [SerializeField]
    [Expandable]
    private PlayerSetting playerSetting;
    
    [SerializeField]
    private TMP_Text overText;

    private const float Rad = 6f;
    private IEventBusService eventBusService;
    public PlayerController playerController { get; private set; }

    protected override PlayerController CreateControllerImpl()
    {
        playerController = new PlayerController(
            transform,
            animator,
            inputActions,
            characterController,
            movementAudioSource,
            tabletAudioSource,
            playerSetting,
            eventBusService,
            overText);

        playerController?.Initialize();
        characterController.enabled = false;
        eventBusService?.RegisterListener<StartGameParam>(OnGameStart);
        return playerController;
    }

    public void Initialize(IEventBusService eventBusService)
    {
        this.eventBusService = eventBusService;
    }

    private void Update()
    {
        playerController?.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerController?.OnTriggerEnter(other);
    }

    private void OnGameStart(StartGameParam param)
    {
        characterController.enabled = true;
    }

    private void OnDestroy()
    {
        eventBusService?.UnregisterListener<StartGameParam>(OnGameStart);
        playerController?.Dispose();
    }
}
