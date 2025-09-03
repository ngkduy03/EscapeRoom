using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Component that represents the key in the scene.
/// </summary>
public class KeyComponent : SceneComponent<KeyController>
{
    [SerializeField]
    private Animator doorAnimator;
    private KeyController keyController;
    private const string State = "State";
    private const int OpenDoorState = 1;

    protected override KeyController CreateControllerImpl()
    {
        keyController = new KeyController(transform);
        return keyController;
    }

    private void OnEnable()
    {
        keyController = CreateController();
        keyController.SwitchPositionPeriodically().Forget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerComponent player))
        {
            doorAnimator.SetInteger(State, OpenDoorState);
            gameObject.SetActive(false);
            keyController.Dispose();
        }
    }

    private void OnDestroy()
    {
        keyController?.Dispose();
    }
}
