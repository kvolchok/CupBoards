using GameStates;
using VContainer.Unity;

public class GameController : IStartable
{
    private readonly GameStateMachine _stateMachine;

    public GameController(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public async void Start()
    {
        await _stateMachine.Enter<BootstrapState>();
    }
}