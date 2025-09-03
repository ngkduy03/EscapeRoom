using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Cysharp.Threading.Tasks;
using System;

/// <summary>
/// Controls the key behavior.
/// </summary>
public class KeyController : ControllerBase
{
    private readonly Transform keyTransform;
    private const int MaxAttempts = 30;
    private const float MaxRadius = 20f;
    private const float MinRadius = 10f;
    private const float YOffset = 0.55f;
    private CancellationTokenSource cts = new();

    public KeyController(Transform keyTransform)
    {
        this.keyTransform = keyTransform;
    }

    /// <summary>
    /// Async method that switches the key position every 10 seconds within NavMesh walkable area
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to stop the operation</param>
    public async UniTask SwitchPositionPeriodically()
    {
        while (!cts.IsCancellationRequested)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(10), cancellationToken: cts.Token);
                Vector3 newPosition = FindRandomNavMeshPosition();

                if (newPosition != Vector3.zero)
                {
                    // Add Y offset of 0.55 to lift the key above the ground
                    newPosition.y += YOffset;
                    keyTransform.position = newPosition;
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
    }

    /// <summary>
    /// Finds a random position within NavMesh walkable area that is at least 10 units away from current position
    /// </summary>
    private Vector3 FindRandomNavMeshPosition()
    {
        Vector3 currentPosition = keyTransform.position;

        for (int i = 0; i < MaxAttempts; i++)
        {
            // Generate random position within search radius
            Vector3 randomDirection = Random.insideUnitSphere;
            randomDirection = currentPosition + randomDirection * Random.Range(MinRadius, MaxRadius);

            // Sample position on NavMesh
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, MaxRadius, NavMesh.AllAreas))
            {
                // Check if the new position is at least 10 units away from current position
                float distance = Vector3.Distance(currentPosition, hit.position);
                if (distance >= MinRadius)
                {
                    return hit.position;
                }
            }
        }
        return Vector3.zero;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            cts?.Cancel();
            cts?.Dispose();
        }
        base.Dispose(disposing);
    }
}
