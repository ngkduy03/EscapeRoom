using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the game canvas UI elements.
/// </summary>
public class GameCanvasController : ControllerBase
{
    private readonly Button playButton;
    private readonly Button quitButton1;
    private readonly Button quitButton2;
    private readonly Button againButton;
    private readonly Canvas introCanvas;
    private readonly Canvas gameOverCanvas;
    private IEventBusService eventBusService;
    private ILoadSceneService loadSceneService;

    public GameCanvasController(
        Button playButton,
        Button quitButton1,
        Button quitButton2,
        Button againButton,
        Canvas introCanvas,
        Canvas gameOverCanvas,
        IEventBusService eventBusService,
        ILoadSceneService loadSceneService
    )
    {
        this.eventBusService = eventBusService;
        this.loadSceneService = loadSceneService;
        this.playButton = playButton;
        this.quitButton1 = quitButton1;
        this.quitButton2 = quitButton2;
        this.againButton = againButton;
        this.introCanvas = introCanvas;
        this.gameOverCanvas = gameOverCanvas;

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        quitButton1.onClick.AddListener(OnQuitButtonClicked);
        quitButton2.onClick.AddListener(OnQuitButtonClicked);
        againButton.onClick.AddListener(OnAgainButtonClicked);
        eventBusService.RegisterListener<GameOverParam>(OnGameOver);
    }

    private void UnsubscribeFromEvents()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClicked);
        quitButton1.onClick.RemoveListener(OnQuitButtonClicked);
        quitButton2.onClick.RemoveListener(OnQuitButtonClicked);
        againButton.onClick.RemoveListener(OnAgainButtonClicked);
        eventBusService.UnregisterListener<GameOverParam>(OnGameOver);
    }

    private void OnPlayButtonClicked()
    {
        introCanvas.enabled = false;
        eventBusService?.TriggerEvent(new StartGameParam());
    }

    private void OnQuitButtonClicked()
    {
        loadSceneService?.QuitGame();
    }

    private void OnAgainButtonClicked()
    {
        loadSceneService?.ReloadCurrentScene();
    }

    private void OnGameOver(GameOverParam param)
    {
        gameOverCanvas.enabled = true;
    }

    protected override void Dispose(bool disposing)
    {
        UnsubscribeFromEvents();
    }
}
