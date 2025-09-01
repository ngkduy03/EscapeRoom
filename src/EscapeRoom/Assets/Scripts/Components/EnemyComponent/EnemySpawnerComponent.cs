using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

/// <summary>
/// The component responsible for spawning enemies.
/// </summary>
public class EnemySpawnerComponent : SceneComponent<EnemySpawnerController>
{
    [SerializeField]
    private EnemyComponent enemyComponent;

    [SerializeField]
    private PlayerComponent playerComponent;

    [SerializeField]
    private NavMeshSurface navMeshSurface;

    private IEventBusService eventBusService;
    private EnemySpawnerController enemySpawnerController;

    protected override EnemySpawnerController CreateControllerImpl()
    {
        enemySpawnerController = new EnemySpawnerController(
            enemyComponent,
            playerComponent,
            navMeshSurface,
            eventBusService
        );
        return enemySpawnerController;
    }

    public void Initialize(IEventBusService eventBusService)
    {
        this.eventBusService = eventBusService;
    }

    private void Start()
    {
        enemySpawnerController.StartContinuousSpawning();
    }
}
