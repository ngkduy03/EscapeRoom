using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameLoopContext is a context that initializes the game loop components for the play scene. 
/// </summary>
[DefaultExecutionOrder(-1)]
public class GameLoopContext : BaseContext<ServiceInitializer>
{
    [SerializeField]
    private PlayerComponent playerComponent;
    private PlayerController playerController;

    [SerializeField]
    private EnemySpawnerComponent enemySpawnerComponent;
    private EnemySpawnerController enemySpawnerController;
    private IEventBusService eventBusService;
    private ILoadSceneService loadSceneService;

    /// <inheritdoc />
    protected override void Initialize(IServiceContainer serviceResolver)
    {
        loadSceneService = serviceResolver.Resolve<ILoadSceneService>();
        eventBusService = serviceResolver.Resolve<IEventBusService>();

        playerComponent.Initialize(eventBusService);
        playerController = playerComponent.CreateController();

        enemySpawnerComponent.Initialize(eventBusService);
        enemySpawnerController = enemySpawnerComponent.CreateController();
    }

    /// <inheritdoc />
    protected override void Deinitialize()
    {
        playerController?.Dispose();
        enemySpawnerController?.Dispose();
    }
}
