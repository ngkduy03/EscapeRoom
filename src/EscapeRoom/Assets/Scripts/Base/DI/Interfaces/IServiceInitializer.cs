/// <summary>
/// Implement this interface to initialize services.
/// </summary>
public interface IServiceInitializer
{
    /// <summary>
    /// Initialize services.
    /// </summary>
    void InitializeServices(ServiceContainer serviceContainer);
}