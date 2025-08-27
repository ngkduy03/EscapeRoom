using UnityEngine;

/// <summary>
/// Base class of scene context.
/// </summary>
/// <typeparam name="T">Service initializer.</typeparam>
public abstract class BaseContext<T> : MonoBehaviour where T : IServiceInitializer, new()
{
    private static ServiceContainer serviceContainer;

    /// <summary>
    /// Initialize context with services and controllers.
    /// </summary>
    protected abstract void Initialize(IServiceContainer serviceResolver);

    /// <summary>
    /// Deinitialize.
    /// </summary>
    protected abstract void Deinitialize();

    private void Awake()
    {
        if (serviceContainer == null)
        {
            serviceContainer = new ServiceContainer();
            var serviceInitializer = new T();
            serviceInitializer.InitializeServices(serviceContainer);
        }

        Initialize(serviceContainer);
    }

    private void OnDestroy()
    {
        Deinitialize();
    }
}