using Cysharp.Threading.Tasks;
using Factories;
using Services;
using Settings;
using StateMachine;
using Views;

namespace GameStates
{
    public class LevelLoadState : IState
    {
        private readonly LevelSettingsProvider _levelSettingsProvider;
        private readonly GameSettings _gameSettings;
        private readonly GraphService _graphService;
        private readonly GraphPresenterFactory _graphPresenterFactory;

        private StateMachine.StateMachine _stateMachine;
        private GraphPresenter _startGraphPresenter;
        private GraphPresenter _targetGraphPresenter;

        public LevelLoadState(
            LevelSettingsProvider levelSettingsProvider,
            GameSettings gameSettings,
            GraphService graphService,
            GraphPresenterFactory graphPresenterFactory)
        {
            _levelSettingsProvider = levelSettingsProvider;
            _gameSettings = gameSettings;
            _graphService = graphService;
            _graphPresenterFactory = graphPresenterFactory;
        }

        public void Initialize(StateMachine.StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async UniTask Enter()
        {
            _startGraphPresenter?.ClearView();
            _targetGraphPresenter?.ClearView();

            var levelSettings = _levelSettingsProvider.GetCurrentLevel();

            var startGraph = _graphService.CreateStartGraph(levelSettings, _gameSettings);
            _startGraphPresenter = _graphPresenterFactory.CreateStartPresenter(startGraph);
            _startGraphPresenter.Show();
        
            var targetGraph = _graphService.CreateTargetGraph(levelSettings, _gameSettings);
            _targetGraphPresenter = _graphPresenterFactory.CreateTargetPresenter(targetGraph);
            _targetGraphPresenter.Show(isInteractable: false);

            await _stateMachine.Enter<SelectStartNodeState>();
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}