using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls the enemy spawning behavior.
/// </summary>
public class EnemySpawnerController : ControllerBase
{
    private readonly EnemyComponent enemyComponent;
    private readonly PlayerComponent playerComponent;
    private readonly NavMeshSurface navMeshSurface;
    private float spawnRange = 6f;
    private float limitRange = 3f;
    private float spawnTime = 3f;
    private bool isSpawning = false;
    private bool continuousSpawning = false;
    private CancellationTokenSource cts = new();
    private const int MaxAttempts = 30;
    private IEventBusService eventBusService;

    public EnemySpawnerController(
        EnemyComponent enemyComponent,
        PlayerComponent playerComponent,
        NavMeshSurface navMeshSurface,
        IEventBusService eventBusService)
    {
        this.enemyComponent = enemyComponent;
        this.playerComponent = playerComponent;
        this.navMeshSurface = navMeshSurface;
        this.eventBusService = eventBusService;

        eventBusService?.RegisterListener<GameOverParam>(OnGameOver);
    }

    /// <summary>
    /// Start continuous enemy spawning every 3 seconds
    /// </summary>
    public async void StartContinuousSpawning()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        continuousSpawning = true;

        while (continuousSpawning)
        {
            await SpawnEnemy(cts.Token);
        }
    }

    private async UniTask SpawnEnemy(CancellationToken cancellationToken)
    {
        if (isSpawning) return;

        isSpawning = true;
        await UniTask.Delay(TimeSpan.FromSeconds(spawnTime), cancellationToken: cancellationToken);

        var playerPosition = playerComponent.transform.position;
        var spawnPosition = FindValidSpawnPosition(playerPosition);

        if (spawnPosition != Vector3.zero)
        {
            var newEnemy = UnityEngine.Object.Instantiate(enemyComponent.gameObject, spawnPosition, Quaternion.identity);
            var spawnedEnemyComponent = newEnemy.GetComponent<EnemyComponent>();

            if (spawnedEnemyComponent != null)
            {
                spawnedEnemyComponent.Initialize(playerComponent.transform, eventBusService);
                spawnedEnemyComponent.CreateController();
            }
        }

        isSpawning = false;
    }

    private Vector3 FindValidSpawnPosition(Vector3 playerPosition)
    {
        for (int i = 0; i < MaxAttempts; i++)
        {
            // Generate random position within spawn range around player
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere;
            randomDirection.y = 0;
            randomDirection.Normalize();

            // Check if position is on NavMesh
            Vector3 randomPosition = playerPosition + randomDirection * UnityEngine.Random.Range(limitRange, spawnRange);
            if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return Vector3.zero;
    }

    private void OnGameOver(GameOverParam param)
    {
        continuousSpawning = false;
        isSpawning = false;
    }

    protected override void Dispose(bool disposing)
    {
        isSpawning = false;
        continuousSpawning = false;
        eventBusService?.UnregisterListener<GameOverParam>(OnGameOver);
    }
}
