using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which contains services of application.
/// </summary>
public class ServiceContainer : IServiceContainer
{
    private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

    /// <inheritdoc/>
    public void Register<T>(T instance)
    {
        if (!services.TryAdd(typeof(T), instance))
        {
            Debug.LogWarning($"{typeof(T).Name} is already registered");
        }
    }

    /// <inheritdoc/>
    public void Unregister<T>(T instance)
    {
        if (services.TryGetValue(typeof(T), out var service))
        {
            if (!ReferenceEquals(service, instance))
            {
                throw new ArgumentException("Instance to unregister is not equal to the registered one.");
            }

            if (service is IDisposable disposable)
            {
                disposable.Dispose();
            }

            services.Remove(typeof(T));
        }
        else
        {
            Debug.LogWarning($"{typeof(T).Name} is not registered");
        }
    }

    /// <inheritdoc/>
    public T Resolve<T>()
    {
        if (services.TryGetValue(typeof(T), out var service))
        {
            return (T)service;
        }
        else
        {
            throw new Exception($"Service {typeof(T)} not found in Game Services.");
        }
    }

    /// <inheritdoc/>
    public T Resolve<T>(Type instanceType)
    {
        if (services.TryGetValue(typeof(T), out var service) && service.GetType() == instanceType)
        {
            return (T)service;
        }
        else
        {
            throw new Exception($"Service {typeof(T)} not found in Game Services.");
        }
    }
}
