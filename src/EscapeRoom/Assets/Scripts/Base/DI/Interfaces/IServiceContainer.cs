using System;

/// <summary>
/// Implement this interface to resolve and register/unregister services.
/// </summary>
public interface IServiceContainer
{
    /// <summary>
    /// Method to get existing Service instance.
    /// </summary>
    T Resolve<T>();
    
    /// <summary>
    /// Method to get existing Service instance with correct instance.
    /// </summary>
    T Resolve<T>(Type instanceType);

    /// <summary>
    /// This method creates an instance of the class.
    /// </summary>
    void Register<T>(T instance);

    /// <summary>
    /// This method removes an instance. 
    /// </summary>
    void Unregister<T>(T instance);
}