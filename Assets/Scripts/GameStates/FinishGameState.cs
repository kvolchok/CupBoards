using System;
using Cysharp.Threading.Tasks;
using Events;
using Settings;
using StateMachine;
using UniTaskPubSub;

namespace GameStates
{
    /// <summary>
    /// Finish Game State:
    /// 1. Show GameOver Screen
    /// 2. Select of reloading the level or loading the next one
    /// </summary>
    public class FinishGameState : IState
    {
        private readonly AsyncMessageBus _messageBus;
        private readonly UserProgressController _userProgressController;

        private StateMachine.StateMachine _stateMachine;
        private IDisposable _subscription;

        public FinishGameState(AsyncMessageBus messageBus, UserProgressController userProgressController)
        {
            _messageBus = messageBus;
            _userProgressController = userProgressController;
        }

        public void Initialize(StateMachine.StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async UniTask Enter()
        {
            _subscription = _messageBus.Subscribe<ButtonClickedEvent>(OnButtonClicked);

            await _messageBus.PublishAsync(new GameOverEvent());
        }

        private async UniTask OnButtonClicked(ButtonClickedEvent eventData)
        {
            if (eventData.IsNextLevelButtonClicked)
            {
                var currentLevelIndex = _userProgressController.LoadCurrentLevelIndex();
                currentLevelIndex++;
                _userProgressController.SaveCurrentLevelIndex(currentLevelIndex);
            }

            await _stateMachine.Enter<LevelLoadState>();
        }

        public UniTask Exit()
        {
            _subscription?.Dispose();

            return UniTask.CompletedTask;
        }
    }
}