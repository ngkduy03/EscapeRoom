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
    private IEventBusService eventBusService;
    public EnemyController enemyController { get; private set; }

    protected override EnemyController CreateControllerImpl()
    {
        enemyController = new EnemyController(playerTransform, agent, animator, eventBusService);

        enemyController?.Initialize();
        return enemyController;
    }

    /// <summary>
    /// Initializes the enemy component with the player's transform and the event bus service.
    /// </summary>
    /// <param name="playerTransform"></param>
    /// <param name="eventBusService"></param>
    public void Initialize(Transform playerTransform, IEventBusService eventBusService)
    {
        this.playerTransform = playerTransform;
        this.eventBusService = eventBusService;
    }

    private void OnEnable()
    {
        enemyController = CreateController();
    }

    private void Update()
    {
        enemyController?.Update();
    }

    private void OnDestroy()
    {
        enemyController?.Dispose();
    }
}
