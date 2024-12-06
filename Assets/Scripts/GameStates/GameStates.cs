using StateMachine;

namespace GameStates
{
    public class GameStates
    {
        public IExitableState[] AllStates => new IExitableState[]
        {
            _levelLoadState,
            _selectStartNodeState,
            _selectTargetNodeState,
            _chipMovingState,
            _finishGameState
        };
        
        private readonly LevelLoadState _levelLoadState;
        private readonly SelectStartNodeState _selectStartNodeState;
        private readonly SelectTargetNodeState _selectTargetNodeState;
        private readonly ChipMovingState _chipMovingState;
        private readonly FinishGameState _finishGameState;

        public GameStates(
            LevelLoadState levelLoadState,
            SelectStartNodeState selectStartNodeState,
            SelectTargetNodeState selectTargetNodeState,
            ChipMovingState chipMovingState,
            FinishGameState finishGameState)
        {
            _levelLoadState = levelLoadState;
            _selectStartNodeState = selectStartNodeState;
            _selectTargetNodeState = selectTargetNodeState;
            _chipMovingState = chipMovingState;
            _finishGameState = finishGameState;
        }
    }
}