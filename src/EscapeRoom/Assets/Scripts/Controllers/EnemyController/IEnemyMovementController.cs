using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

/// <summary>
/// Interface for enemy movement controllers.
/// </summary>
public interface IEnemyMovementController : IController
{
    /// <summary>
    /// Moves the enemy towards the player.
    /// </summary>
    UniTask Move(CancellationToken cancellationToken);
}
