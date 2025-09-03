using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Component responsible for managing the game canvas UI.
/// </summary>
public class GameCanvasComponent : SceneComponent<GameCanvasController>
{
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button quitButton1;

    [SerializeField]
    private Button quitButton2;

    [SerializeField]
    private Button againButton;

    [SerializeField]
    private Canvas introCanvas;

    [SerializeField]
    private Canvas gameOverCanvas;

    private IEventBusService eventBusService;
    private ILoadSceneService loadSceneService;
    private GameCanvasController gameCanvasController;

    protected override GameCanvasController CreateControllerImpl()
    {
        gameCanvasController = new GameCanvasController(
            playButton,
            quitButton1,
            quitButton2,
            againButton,
            introCanvas,
            gameOverCanvas,
            eventBusService,
            loadSceneService
        );

        return gameCanvasController;
    }

    public void Initialize(IEventBusService eventBusService, ILoadSceneService loadSceneService)
    {
        this.eventBusService = eventBusService;
        this.loadSceneService = loadSceneService;
    }

    private void OnDestroy()
    {
        gameCanvasController?.Dispose();
    }
}
