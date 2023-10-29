namespace GameStates
{
    public class GameStateMachine : StateMachine.StateMachine
    {
        public GameStateMachine(
            LevelLoaderState levelLoaderState,
            SelectStartNodeState selectFirstNodeState,
            SelectTargetNodeState selectTargetNodeState,
            ChipMovingState chipMovingState,
            FinishGameState finishGameState)
            : base(levelLoaderState,
                selectFirstNodeState,
                selectTargetNodeState,
                chipMovingState,
                finishGameState)
        {
        }
    }
}