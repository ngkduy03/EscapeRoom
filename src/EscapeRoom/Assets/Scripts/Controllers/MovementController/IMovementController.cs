using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for movement controllers. 
/// </summary>
public interface IMovementController : IController
{
    /// <summary>
    /// Define movement method.
    /// </summary>
    void Move();
}
