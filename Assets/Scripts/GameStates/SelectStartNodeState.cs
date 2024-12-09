using System;
using Cysharp.Threading.Tasks;
using Events;
using StateMachine;
using UniTaskPubSub;

namespace GameStates
{
    /// <summary>
    /// Select Start Node State:
    /// - Select start node that it's chip will be moved
    /// - If there is no chip in node, select another node
    /// </summary>
    public class SelectStartNodeState : IState
    {
        private readonly IAsyncSubscriber _subscriber;

        private StateMachine.StateMachine _stateMachine;
        private IDisposable _subscription;

        public SelectStartNodeState(IAsyncSubscriber subscriber)
        {
            _subscriber = subscriber;
        }

        public void Initialize(StateMachine.StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public UniTask Enter()
        {
            _subscription = _subscriber.Subscribe<NodeSelectedEvent>(OnNodeSelected);

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