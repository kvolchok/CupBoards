using System;
using Cysharp.Threading.Tasks;
using Events;
using Extensions;
using Factories;
using Settings;
using UniTaskPubSub;
using VContainer.Unity;
using Views;

public class UIController : IStartable, IDisposable
{
    private readonly IGameSettings _gameSettings;
    private readonly GraphPresenterFactory _graphPresenterFactory;
    private readonly GameOverScreen _gameOverScreen;
    private readonly AsyncMessageBus _messageBus;

    private GraphPresenter _startGraphPresenter;
    private GraphPresenter _targetGraphPresenter;
    private GameOverPresenter _gameOverPresenter;
    private CompositeDisposable _subscriptions;

    public UIController(IGameSettings gameSettings, GraphPresenterFactory graphPresenterFactory,
        GameOverScreen gameOverScreen, AsyncMessageBus messageBus)
    {
        _gameSettings = gameSettings;
        _graphPresenterFactory = graphPresenterFactory;
        _gameOverScreen = gameOverScreen;
        _messageBus = messageBus;
    }

    public void Start()
    {
        _subscriptions = new CompositeDisposable
        {
            _messageBus.Subscribe<ShowGraphEvent>(ShowGraph),
            _messageBus.Subscribe<GameOverEvent>(ShowGameOverScreen)
        };

        _gameOverPresenter = new GameOverPresenter(_gameOverScreen, _messageBus);
    }

    private UniTask ShowGraph(ShowGraphEvent eventData)
    {
        var isGraphInteractable = eventData.IsInteractable;
        if (isGraphInteractable)
        {
            _startGraphPresenter?.ClearView();
            _startGraphPresenter =
                _graphPresenterFactory.CreateGraphPresenter(eventData.GraphModel, _gameSettings.StartGraphRoot);
            _startGraphPresenter.ShowGraph(true);
        }
        else
        {
            _targetGraphPresenter?.ClearView();
            _targetGraphPresenter = 
                _graphPresenterFactory.CreateGraphPresenter(eventData.GraphModel, _gameSettings.TargetGraphRoot);
            _targetGraphPresenter.ShowGraph(false);
        }

        return UniTask.CompletedTask;
    }

    private UniTask ShowGameOverScreen(GameOverEvent eventData)
    {
        _gameOverPresenter.Show();

        return UniTask.CompletedTask;
    }

    public void Dispose()
    {
        _subscriptions?.Dispose();
    }
}