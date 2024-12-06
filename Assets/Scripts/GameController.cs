using System;
using GameStates;
using VContainer.Unity;

public class GameController : IStartable, IDisposable
{
    private readonly GameStateMachine _stateMachine;

    public GameController(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public async void Start()
    {
        await _stateMachine.Enter<LevelLoadState>();
    }

    public void Dispose()
    {
        _stateMachine?.Dispose();
    }
}