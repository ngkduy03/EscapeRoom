using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls the enemy's behavior when chasing the player.
/// </summary>
public class EnemyController : ControllerBase
{
    private readonly Transform playerTransform;
    private readonly NavMeshAgent agent;
    private readonly Animator animator;
    private readonly IEventBusService eventBusService;

    private IBehavior chasedBehavior;

    public EnemyController(
        Transform playerTransform,
        NavMeshAgent agent,
        Animator animator,
        IEventBusService eventBusService
    )
    {
        this.playerTransform = playerTransform;
        this.agent = agent;
        this.animator = animator;
        this.eventBusService = eventBusService;
    }

    /// <summary>
    /// Initializes the player controller.
    /// </summary>
    public void Initialize()
    {
        var movementController = new ChaseController(playerTransform, agent, animator, eventBusService);

        chasedBehavior = new EnemyChasedBehavior(movementController);
    }

    public void Update()
    {
        chasedBehavior?.Update();
    }

    protected override void Dispose(bool disposing)
    {
        chasedBehavior?.Dispose();
    }
}
