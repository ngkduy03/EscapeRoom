using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Component that represents the key in the scene.
/// </summary>
public class KeyComponent : SceneComponent<KeyController>
{
    [SerializeField]
    private Animator doorAnimator;

    [SerializeField]
    private float respawnDuration;

    /// <summary>
    /// Event triggered when the key is collected.
    /// </summary>
    public event Action KeyCollected;

    private KeyController keyController;
    private const string State = "State";
    private const int OpenDoorState = 1;
    private IEventBusService eventBusService;

    protected override KeyController CreateControllerImpl()
    {
        keyController = new KeyController(transform, respawnDuration);
        eventBusService?.RegisterListener<StartGameParam>(OnGameStart);
        return keyController;
    }

    public void Initialize(IEventBusService eventBusService)
    {
        this.eventBusService = eventBusService;
    }

    private void OnGameStart(StartGameParam param)
    {
        keyController.SwitchPositionPeriodically().Forget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerComponent player))
        {

            KeyCollected?.Invoke();
            doorAnimator.SetInteger(State, OpenDoorState);
            gameObject.SetActive(false);
            keyController.Dispose();
        }
    }

    private void OnDestroy()
    {
        eventBusService?.UnregisterListener<StartGameParam>(OnGameStart);
        keyController?.Dispose();
    }
}
