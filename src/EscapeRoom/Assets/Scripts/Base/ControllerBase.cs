using System;
using System.Runtime.ConstrainedExecution;

/// <summary>
/// Base class for controllers.
/// </summary>
public abstract class ControllerBase : IController
{
    private bool isDisposed;

    /// <summary>
    /// Custom dispose.
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
    }

    /// <summary>
    /// IDisposable.
    /// </summary>
    public void Dispose()
    {
        if (!isDisposed)
        {
            Dispose(true);
            isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}