using Cysharp.Threading.Tasks;
using Settings;
using StateMachine;

namespace GameStates
{
    public class BootstrapState : IState
    {
        private readonly ILevelSettingsProvider _levelSettingsProvider;
        
        private StateMachine.StateMachine _stateMachine;

        public BootstrapState(ILevelSettingsProvider levelSettingsProvider)
        {
            _levelSettingsProvider = levelSettingsProvider;
        }

        public void Initialize(StateMachine.StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public async UniTask Enter()
        {
            _levelSettingsProvider.LoadConfigs();

            await _stateMachine.Enter<LevelLoadState>();
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}