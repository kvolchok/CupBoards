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
    private readonly GameOverPresenterFactory _gameOverPresenterFactory;
    private readonly GraphPresenterFactory _graphPresenterFactory;
    private readonly AsyncMessageBus _messageBus;
        
    private GameOverPresenter _gameOverPresenter;
    private GraphPresenter _startGraphPresenter;
    private GraphPresenter _targetGraphPresenter;
    private CompositeDisposable _subscriptions;

    public UIController(
        GameOverPresenterFactory gameOverPresenterFactory,
        GraphPresenterFactory graphPresenterFactory,
        AsyncMessageBus messageBus)
    {
        _gameOverPresenterFactory = gameOverPresenterFactory;
        _graphPresenterFactory = graphPresenterFactory;
        _messageBus = messageBus;
    }

    public void Start()
    {
        _gameOverPresenter = _gameOverPresenterFactory.Create();

        _subscriptions = new CompositeDisposable
        {
            _messageBus.Subscribe<GameOverEvent>(ShowGameOverScreen),
            _messageBus.Subscribe<ShowGraphEvent>(ShowGraph)
        };
    }

    private UniTask ShowGameOverScreen(GameOverEvent eventData)
    {
        _gameOverPresenter.Show();

        return UniTask.CompletedTask;
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

    public void Dispose()
    {
        _subscriptions?.Dispose();
    }
}