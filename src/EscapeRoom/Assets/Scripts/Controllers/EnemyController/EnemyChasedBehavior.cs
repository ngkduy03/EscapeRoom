using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// Behavior for the enemy when it is chasing the player.
/// </summary>
public class EnemyChasedBehavior : ControllerBase, IBehavior
{
    private CancellationTokenSource cts = new();
    private IEnemyMovementController movementController;

    public EnemyChasedBehavior(IEnemyMovementController movementController)
    {
        this.movementController = movementController;
    }

    public void OnDisable()
    {
        cts?.Cancel();
    }

    public void OnEnable()
    {
        cts = new();
    }

    public void OnTriggerEnter(Collider other)
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        movementController?.Move(cts.Token);
    }

    protected override void Dispose(bool disposing)
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = null;
        movementController?.Dispose();
    }
}
