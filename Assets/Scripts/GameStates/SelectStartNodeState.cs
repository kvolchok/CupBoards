using System;
using Cysharp.Threading.Tasks;
using Events;
using StateMachine;
using UniTaskPubSub;

namespace GameStates
{
    public class SelectStartNodeState : IState
    {
        private readonly AsyncMessageBus _messageBus;

        private StateMachine.StateMachine _stateMachine;
        private IDisposable _subscription;

        public SelectStartNodeState(AsyncMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Initialize(StateMachine.StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public UniTask Enter()
        {
            _subscription = _messageBus.Subscribe<NodeSelectedEvent>(OnNodeSelected);

            return UniTask.CompletedTask;
        }

        private async UniTask OnNodeSelected(NodeSelectedEvent eventData)
        {
            var selectedNode = eventData.NodeModel;
            
            if (selectedNode.Chip == null)
            {
                await _stateMachine.Enter<SelectStartNodeState>();
            }
            else
            {
                await _stateMachine.Enter<SelectTargetNodeState, SelectTargetNodeStateContext>(
                    new SelectTargetNodeStateContext(selectedNode));
            }
        }

        public UniTask Exit()
        {
            _subscription?.Dispose();
            
            return UniTask.CompletedTask;
        }
    }
}