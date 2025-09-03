using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ILoadSceneService is an interface that defines methods for loading scenes in the game.
/// </summary>
public interface ILoadSceneService
{
    /// <summary>
    /// Reloads the current scene.
    /// </summary>
    void ReloadCurrentScene();

    /// <summary>
    /// Loads a scene asynchronously by its name.
    /// </summary>
    /// <param name="sceneName"></param>
    void LoadSceneAsync(string sceneName);

    /// <summary>
    /// Quits the game application.
    /// </summary>
    void QuitGame();
}
