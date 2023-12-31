using Cysharp.Threading.Tasks;
using Events;
using Services;
using Settings;
using StateMachine;
using UniTaskPubSub;

namespace GameStates
{
    /// <summary>
    /// Level Load State:
    /// - Load current level
    /// - Create start and target graphs
    /// </summary>
    public class LevelLoadState : IState
    {
        private readonly ILevelSettingsProvider _levelSettingsProvider;
        private readonly GameSettings _gameSettings;
        private readonly GraphService _graphService;
        private readonly IAsyncPublisher _publisher;

        private StateMachine.StateMachine _stateMachine;

        public LevelLoadState(
            ILevelSettingsProvider levelSettingsProvider,
            GameSettings gameSettings,
            GraphService graphService,
            IAsyncPublisher publisher)
        {
            _levelSettingsProvider = levelSettingsProvider;
            _gameSettings = gameSettings;
            _graphService = graphService;
            _publisher = publisher;
        }

        public void Initialize(StateMachine.StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async UniTask Enter()
        {
            var levelSettings = _levelSettingsProvider.GetCurrentLevel();

            var startGraph = _graphService.CreateStartGraph(levelSettings, _gameSettings);
            var targetGraph = _graphService.CreateTargetGraph(levelSettings, _gameSettings);

            await _publisher.PublishAsync(new ShowGraphEvent(startGraph));
            await _publisher.PublishAsync(new ShowGraphEvent(targetGraph, isInteractable: false));

            await _stateMachine.Enter<SelectStartNodeState>();
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}