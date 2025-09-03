using System;
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

    private KeyComponent keyComponent;
    private IEventBusService eventBusService;
    private EnemySpawnerController enemySpawnerController;

    protected override EnemySpawnerController CreateControllerImpl()
    {
        enemySpawnerController = new EnemySpawnerController(
            enemyComponent,
            playerComponent,
            navMeshSurface,
            eventBusService,
            keyComponent
        );

        eventBusService?.RegisterListener<StartGameParam>(OnGameStart);
        return enemySpawnerController;
    }

    public void Initialize(IEventBusService eventBusService, KeyComponent keyComponent)
    {
        this.eventBusService = eventBusService;
        this.keyComponent = keyComponent;
    }

    private void OnGameStart(StartGameParam param)
    {
        enemySpawnerController.StartContinuousSpawning();
    }

    private void OnDestroy()
    {
        eventBusService?.UnregisterListener<StartGameParam>(OnGameStart);
        enemySpawnerController?.Dispose();
    }
}
