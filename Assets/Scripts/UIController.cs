using System;
using Cysharp.Threading.Tasks;
using Events;
using Extensions;
using Factories;
using UniTaskPubSub;
using VContainer.Unity;
using Views;

public class UIController : IStartable, IDisposable
{
    private readonly GraphPresenterFactory _graphPresenterFactory;
    private readonly GameOverPresenterFactory _gameOverPresenterFactory;
    private readonly AsyncMessageBus _messageBus;

    private GraphPresenter _startGraphPresenter;
    private GraphPresenter _targetGraphPresenter;
    private GameOverPresenter _gameOverPresenter;
    private CompositeDisposable _subscriptions;

    public UIController(
        GraphPresenterFactory graphPresenterFactory,
        GameOverPresenterFactory gameOverPresenterFactory,
        AsyncMessageBus messageBus)
    {
        _graphPresenterFactory = graphPresenterFactory;
        _gameOverPresenterFactory = gameOverPresenterFactory;
        _messageBus = messageBus;
    }

    public void Start()
    {
        _subscriptions = new CompositeDisposable
        {
            _messageBus.Subscribe<ShowGraphEvent>(ShowGraph),
            _messageBus.Subscribe<GameOverEvent>(ShowGameOverScreen)
        };
        
        _gameOverPresenter = _gameOverPresenterFactory.Create();
    }

    private UniTask ShowGraph(ShowGraphEvent eventData)
    {
        if (eventData.IsInteractable)
        {
            _startGraphPresenter?.ClearView();
            _startGraphPresenter = _graphPresenterFactory.CreateStartPresenter(eventData.GraphModel);
            _startGraphPresenter.ShowGraph(eventData.IsInteractable);
        }
        else
        {
            _targetGraphPresenter?.ClearView();
            _targetGraphPresenter = _graphPresenterFactory.CreateTargetPresenter(eventData.GraphModel);
            _targetGraphPresenter.ShowGraph(eventData.IsInteractable);
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