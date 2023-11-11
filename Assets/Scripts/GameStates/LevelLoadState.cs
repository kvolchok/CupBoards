using Cysharp.Threading.Tasks;
using Factories;
using Services;
using Settings;
using StateMachine;
using UniTaskPubSub;
using Views;

namespace GameStates
{
    public class LevelLoadState : IStateWithContext<LevelLoaderStateContext>
    {
        private readonly LevelSettingsProvider _levelSettingsProvider;
        private readonly GameSettings _gameSettings;
        private readonly GraphFactory _graphFactory;
        private readonly GraphPresenterFactory _graphPresenterFactory;
        private readonly AsyncMessageBus _messageBus;
        private readonly GraphManager _graphManager;

        private StateMachine.StateMachine _stateMachine;
        private GraphPresenter _startGraphPresenter;
        private GraphPresenter _targetGraphPresenter;

        public LevelLoadState(
            LevelSettingsProvider levelSettingsProvider,
            GameSettings gameSettings,
            GraphFactory graphFactory,
            GraphPresenterFactory graphPresenterFactory,
            AsyncMessageBus messageBus,
            GraphManager graphManager)
        {
            _levelSettingsProvider = levelSettingsProvider;
            _gameSettings = gameSettings;
            _graphFactory = graphFactory;
            _graphPresenterFactory = graphPresenterFactory;
            _messageBus = messageBus;
            _graphManager = graphManager;
        }

        public void Initialize(StateMachine.StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async UniTask Enter(LevelLoaderStateContext context)
        {
            _startGraphPresenter?.ClearView();
            _targetGraphPresenter?.ClearView();
            
            var isLevelCompleted = context.IsLevelCompleted;

            var levelSettings = _levelSettingsProvider.GetLevel(shouldLoadNextLevel:isLevelCompleted);

            var startGraph = _graphFactory.CreateStartGraph(levelSettings, _gameSettings);
            _startGraphPresenter = _graphPresenterFactory.CreateStartPresenter(startGraph);
            _startGraphPresenter.Show();
            
            _startGraphPresenter.CreateNodePresenters(_messageBus);
            _startGraphPresenter.CreateChipPresenters();
        
            var targetGraph = _graphFactory.CreateTargetGraph(levelSettings, _gameSettings);
            _targetGraphPresenter = _graphPresenterFactory.CreateTargetPresenter(targetGraph);
            _targetGraphPresenter.Show();

            _graphManager.SaveGraphs(startGraph, targetGraph);

            await _stateMachine.Enter<SelectStartNodeState>();
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}