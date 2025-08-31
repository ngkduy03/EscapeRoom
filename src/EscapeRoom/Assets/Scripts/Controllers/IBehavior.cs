using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for player behavior.
/// </summary>
public interface IBehavior : IController
{
    /// <summary>
    /// Starts the player behavior.
    /// </summary>
    void OnEnable();

    /// <summary>
    /// Starts the player behavior.
    /// </summary>
    void OnDisable();

    /// <summary>
    /// Updates the player behavior.
    /// </summary>
    void Update();

    /// <summary>
    /// Handles the trigger enter event for the player, allowing it to interact with other objects in the game world.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other);
}
