using Cysharp.Threading.Tasks;
using Settings;
using StateMachine;

namespace GameStates
{
    public class BootstrapState : IState
    {
        private readonly LevelSettingsProvider _levelSettingsProvider;
        
        private StateMachine.StateMachine _stateMachine;

        public BootstrapState(LevelSettingsProvider levelSettingsProvider)
        {
            _levelSettingsProvider = levelSettingsProvider;
        }

        public void Initialize(StateMachine.StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public async UniTask Enter()
        {
            _levelSettingsProvider.LoadLevels();

            await _stateMachine.Enter<LevelLoadState, LevelLoaderStateContext>(new LevelLoaderStateContext(false));
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}