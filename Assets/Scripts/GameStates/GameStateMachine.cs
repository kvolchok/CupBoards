namespace GameStates
{
    public class GameStateMachine : StateMachine.StateMachine
    {
        public GameStateMachine(
            BootstrapState bootstrapState,
            LevelLoadState levelLoadState,
            SelectStartNodeState selectFirstNodeState,
            SelectTargetNodeState selectTargetNodeState,
            ChipMovingState chipMovingState,
            FinishGameState finishGameState)
            : base(bootstrapState,
                levelLoadState,
                selectFirstNodeState,
                selectTargetNodeState,
                chipMovingState,
                finishGameState)
        {
        }
    }
}