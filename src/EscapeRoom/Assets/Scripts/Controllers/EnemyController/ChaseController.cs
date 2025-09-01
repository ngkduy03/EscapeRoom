using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls the enemy's movement behavior when chasing the player.
/// </summary>
public class ChaseController : ControllerBase, IEnemyMovementController
{
    private readonly Transform playerTransform;
    private readonly NavMeshAgent agent;
    private readonly Animator animator;
    private readonly IEventBusService eventBusService;
    private bool isResetUpdatePath = false;
    private const float UpdatePathCoolDown = 0.3f;
    private const string State = "State";

    public ChaseController(
        Transform playerTransform,
        NavMeshAgent agent,
        Animator animator,
        IEventBusService eventBusService)
    {
        this.playerTransform = playerTransform;
        this.agent = agent;
        this.animator = animator;
        this.eventBusService = eventBusService;

        eventBusService?.RegisterListener<GameOverParam>(OnGameOver);
    }

    ///<inheritdoc />
    public async UniTask Move(CancellationToken cancellationToken)
    {
        if (!isResetUpdatePath && agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            isResetUpdatePath = true;
            agent.SetDestination(playerTransform.position);
            await UniTask.WaitForSeconds(UpdatePathCoolDown, cancellationToken: cancellationToken);
            isResetUpdatePath = false;
        }
    }

    private void OnGameOver(GameOverParam param)
    {
        agent.speed = 0;
        agent.angularSpeed = 0;
        agent.isStopped = true;
        animator.SetInteger(State, (int)EnemyAnimationEnum.Idle);
    }

    protected override void Dispose(bool disposing)
    {
        isResetUpdatePath = false;
        agent.enabled = false;
        eventBusService?.UnregisterListener<GameOverParam>(OnGameOver);
    }
}
