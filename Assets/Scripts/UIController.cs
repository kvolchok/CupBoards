using System;
using Cysharp.Threading.Tasks;
using Events;
using Factories;
using UniTaskPubSub;
using VContainer.Unity;
using Views;

public class UIController : IStartable, IDisposable
{
    private readonly GameOverPresenterFactory _gameOverPresenterFactory;
    private readonly AsyncMessageBus _messageBus;
        
    private GameOverPresenter _gameOverPresenter;
    private IDisposable _subscription;

    public UIController(GameOverPresenterFactory gameOverPresenterFactory, AsyncMessageBus messageBus)
    {
        _gameOverPresenterFactory = gameOverPresenterFactory;
        _messageBus = messageBus;
    }

    public void Start()
    {
        _gameOverPresenter = _gameOverPresenterFactory.Create();

        _subscription = _messageBus.Subscribe<GameOverEvent>(ShowGameOverScreen);
    }

    private UniTask ShowGameOverScreen(GameOverEvent eventData)
    {
        _gameOverPresenter.Show();

        return UniTask.CompletedTask;
    }

    public void Dispose()
    {
        _subscription?.Dispose();
    }
}