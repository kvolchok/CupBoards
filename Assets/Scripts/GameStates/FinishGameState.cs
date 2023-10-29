using Cysharp.Threading.Tasks;
using Events;
using Extensions;
using StateMachine;
using UniTaskPubSub;

namespace GameStates
{
    public class FinishGameState : IState
    {
        private readonly AsyncMessageBus _messageBus;
        
        private StateMachine.StateMachine _stateMachine;
        private CompositeDisposable _subscriptions;

        public FinishGameState(AsyncMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Initialize(StateMachine.StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async UniTask Enter()
        {
            _subscriptions = new CompositeDisposable
            {
                _messageBus.Subscribe<RestartLevelButtonClickedEvent>(OnRestartLevelButtonClicked),
                _messageBus.Subscribe<NextLevelButtonClickedEvent>(OnNextLevelButtonClicked)
            };
            
            await _messageBus.PublishAsync(new EnterFinishGameStateEvent());
        }

        private async UniTask OnRestartLevelButtonClicked(RestartLevelButtonClickedEvent eventData)
        {
            await _stateMachine.Enter<LevelLoaderState, LevelLoaderStateContext>(
                new LevelLoaderStateContext(false));
        }
        
        private async UniTask OnNextLevelButtonClicked(NextLevelButtonClickedEvent eventData)
        {
            await _stateMachine.Enter<LevelLoaderState, LevelLoaderStateContext>(
                new LevelLoaderStateContext(true));
        }

        public UniTask Exit()
        {
            _subscriptions?.Dispose();
            
            return UniTask.CompletedTask;
        }
    }
}