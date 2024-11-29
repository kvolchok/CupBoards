using StateMachine;

namespace GameStates
{
    public class GameStates
    {
        public IExitableState[] AllStates => new IExitableState[]
        {
            _bootstrapState,
            _levelLoadState,
            _selectStartNodeState,
            _selectTargetNodeState,
            _chipMovingState,
            _finishGameState
        };
        
        private readonly BootstrapState _bootstrapState;
        private readonly LevelLoadState _levelLoadState;
        private readonly SelectStartNodeState _selectStartNodeState;
        private readonly SelectTargetNodeState _selectTargetNodeState;
        private readonly ChipMovingState _chipMovingState;
        private readonly FinishGameState _finishGameState;

        public GameStates(
            BootstrapState bootstrapState,
            LevelLoadState levelLoadState,
            SelectStartNodeState selectStartNodeState,
            SelectTargetNodeState selectTargetNodeState,
            ChipMovingState chipMovingState,
            FinishGameState finishGameState)
        {
            _bootstrapState = bootstrapState;
            _levelLoadState = levelLoadState;
            _selectStartNodeState = selectStartNodeState;
            _selectTargetNodeState = selectTargetNodeState;
            _chipMovingState = chipMovingState;
            _finishGameState = finishGameState;
        }
    }
}