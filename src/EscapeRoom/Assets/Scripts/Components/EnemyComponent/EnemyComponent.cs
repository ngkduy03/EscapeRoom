using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// The component responsible for enemy behavior and interactions.
/// </summary>
public class EnemyComponent : SceneComponent<EnemyController>
{
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private Animator animator;

    private Transform playerTransform;
    private KeyComponent keyComponent;
    private IEventBusService eventBusService;
    public EnemyController enemyController { get; private set; }

    protected override EnemyController CreateControllerImpl()
    {
        enemyController = new EnemyController(
            playerTransform,
            agent,
            animator,
            eventBusService,
            keyComponent);

        enemyController?.Initialize();
        return enemyController;
    }

    /// <summary>
    /// Initializes the enemy component with the player's transform and the event bus service.
    /// </summary>
    /// <param name="playerTransform"></param>
    /// <param name="eventBusService"></param>
    public void Initialize(
        Transform playerTransform,
        IEventBusService eventBusService,
        KeyComponent keyComponent)
    {
        this.playerTransform = playerTransform;
        this.eventBusService = eventBusService;
        this.keyComponent = keyComponent;
    }

    private void Update()
    {
        enemyController?.Update();
    }

    /// <summary>
    /// Called when the enemy dies.
    /// </summary>
    public void OnEnemyDied()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        enemyController?.Dispose();
    }
}
