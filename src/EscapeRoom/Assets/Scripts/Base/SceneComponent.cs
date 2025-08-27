using UnityEngine;

/// <summary>
/// Base class for Scene Component. Derived from MonoBehaviour.
/// </summary>
public abstract class SceneComponent<TController> : MonoBehaviour, ISceneComponent<TController>
    where TController : IController
{
    /// <summary>
    /// Controller for component.
    /// </summary>
    protected TController Controller { get; private set; }

    /// <summary>
    /// Protected part of controller creation.
    /// </summary>
    /// <returns>New controller.</returns>
    protected abstract TController CreateControllerImpl();

    /// <inheritdoc/>
    public TController CreateController()
    {
        Controller = CreateControllerImpl();

        return Controller;
    }
}