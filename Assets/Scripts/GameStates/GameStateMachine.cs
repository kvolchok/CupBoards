namespace GameStates
{
    public class GameStateMachine : StateMachine.StateMachine
    {
        public GameStateMachine(GameStates gameStates) : base(gameStates.AllStates)
        {
        }
    }
}