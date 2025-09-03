/// <summary>
/// Interface for Scene Component. Component is a basic unit of game logic presented in a scene.
/// </summary>
public interface ISceneComponent<TController> where TController : IController
{
    /// <summary>
    /// Assign controller to the actor. Must be called from composition root.
    /// </summary>
    TController CreateController();
}