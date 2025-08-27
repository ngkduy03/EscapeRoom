using System;

/// <summary>
/// An interface for event bus system.
/// </summary>
public interface IEventBusService
{
    /// <summary>
    /// Register a listener for a specific event type.
    /// </summary>
    public void RegisterListener<T>(Action<T> listener) where T : IEventParameter;

    /// <summary>
    /// Unregister a listener for a specific event type.
    /// </summary>
    public void UnregisterListener<T>(Action<T> listener) where T : IEventParameter;

    /// <summary>
    /// Trigger an event with the specified parameters.
    /// </summary>
    public void TriggerEvent<T>(T eventParams) where T : IEventParameter;
}