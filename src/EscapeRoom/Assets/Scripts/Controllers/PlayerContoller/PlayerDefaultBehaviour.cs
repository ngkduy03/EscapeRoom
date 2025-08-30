using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Standard player behavior controller that handles player other controllers.
/// </summary>
public class PlayerDefaultBehaviour : ControllerBase, IBehaviour
{
    private IMovementController movementController;

    public PlayerDefaultBehaviour(IMovementController movementController)
    {
        this.movementController = movementController;
    }

    /// <inheritdoc />
    public void OnDisable()
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public void OnEnable()
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public void OnTriggerEnter(Collider other)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc />
    public void Update()
    {
        movementController?.Move();
    }

    /// <inheritdoc />
    protected override void Dispose(bool disposing)
    {
        movementController?.Dispose();
    }
}
