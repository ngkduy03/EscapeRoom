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

    private const float Rad = 6f;
    private IEventBusService eventBusService;
    public PlayerController playerController { get; private set; }

    protected override PlayerController CreateControllerImpl()
    {
        playerController = new PlayerController(transform, animator, inputActions, characterController, movementAudioSource, playerSetting, eventBusService);
        playerController?.Initialize();
        return playerController;
    }

    public void Initialize(IEventBusService eventBusService)
    {
        this.eventBusService = eventBusService;
    }

    private void Update()
    {
        DrawZone();
        playerController?.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerController?.OnTriggerEnter(other);
    }

    private void DrawZone()
    {
        Debug.DrawRay(transform.position + Vector3.up * 0.5f, transform.forward * Rad, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * 0.5f, -transform.forward * Rad, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * 0.5f, transform.right * Rad, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * 0.5f, -transform.right * Rad, Color.red);
    }

    private void OnDestroy()
    {
        playerController?.Dispose();
    }
}
