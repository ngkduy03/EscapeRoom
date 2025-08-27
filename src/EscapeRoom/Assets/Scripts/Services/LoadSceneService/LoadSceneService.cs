using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneService : ILoadSceneService
{
    /// <inheritdoc/>
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <inheritdoc/>
    public void LoadSceneAsync(string sceneName)
    {
        AsyncOperation loadSceneAsyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        loadSceneAsyncOperation.allowSceneActivation = true;

        loadSceneAsyncOperation.completed += (asyncOp) =>
        {
            Scene newScene = SceneManager.GetSceneByName(sceneName);
            if (newScene.IsValid())
            {
                SceneManager.SetActiveScene(newScene);
            }
        };
    }

    /// <inheritdoc/>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Debug.Log("Quit Game");
        Application.Quit();
#endif
    }

}
