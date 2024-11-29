using System;
using Cysharp.Threading.Tasks;
using GameStates;
using VContainer.Unity;

namespace Tests.SharedTestUtilities
{
    public class TestGameController : IStartable, IDisposable
    {
        private readonly GameStateMachine _stateMachine;
        private readonly TestGraphService _testGraphService;

        public TestGameController(GameStateMachine stateMachine, TestGraphService testGraphService)
        {
            _stateMachine = stateMachine;
            _testGraphService = testGraphService;
        }

        public async void Start()
        {
            await _stateMachine.Enter<BootstrapState>();
        }

        public void SelectNode()
        {
            _testGraphService.SelectRandomNodeModel().ToCoroutine();
        }

        public void Dispose()
        {
            _stateMachine?.Dispose();
        }
    }
}