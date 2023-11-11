using Cysharp.Threading.Tasks;
using Events;
using Services;
using Settings;
using StateMachine;
using UniTaskPubSub;

namespace GameStates
{
    public class LevelLoadState : IState
    {
        private readonly LevelSettingsProvider _levelSettingsProvider;
        private readonly GameSettings _gameSettings;
        private readonly GraphService _graphService;
        private readonly AsyncMessageBus _messageBus;

        private StateMachine.StateMachine _stateMachine;

        public LevelLoadState(
            LevelSettingsProvider levelSettingsProvider,
            GameSettings gameSettings,
            GraphService graphService,
            AsyncMessageBus messageBus)
        {
            _levelSettingsProvider = levelSettingsProvider;
            _gameSettings = gameSettings;
            _graphService = graphService;
            _messageBus = messageBus;
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

            await _messageBus.PublishAsync(new ShowGraphEvent(startGraph));
            await _messageBus.PublishAsync(new ShowGraphEvent(targetGraph, isInteractable: false));

            await _stateMachine.Enter<SelectStartNodeState>();
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}