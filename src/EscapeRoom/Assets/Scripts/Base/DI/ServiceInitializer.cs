/// <summary>
/// Default service initializer. It is example.
/// </summary>
public class ServiceInitializer : IServiceInitializer
{
    /// <inheritdoc/>
    public void InitializeServices(ServiceContainer serviceContainer)
    {
        serviceContainer.Register<ILoadSceneService>(new LoadSceneService());
        serviceContainer.Register<IEventBusService>(new EventBusService());
    }
}
