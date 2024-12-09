using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Events;
using Models;
using Services;
using StateMachine;
using UniTaskPubSub;
using UnityEngine.Pool;

namespace GameStates
{
    /// <summary>
    /// Select Target Node State:
    /// - Select target node where start chip will be moved
    /// - If there is no chip in target node or start chip is target chip, select another start node
    /// - If target node is unreachable, select another target node
    /// </summary>
    public class SelectTargetNodeState : IStateWithContext<SelectTargetNodeStateContext>
    {
        private readonly PathFinderService _pathFinderService;
        private readonly AsyncMessageBus _messageBus;

        private StateMachine.StateMachine _stateMachine;
        private NodeModel _startNode;
        private HashSet<NodeModel> _reachableNodes;
        private IDisposable _subscription;

        public SelectTargetNodeState(PathFinderService pathFinderService, AsyncMessageBus messageBus)
        {
            _pathFinderService = pathFinderService;
            _messageBus = messageBus;
        }

        public void Initialize(StateMachine.StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async UniTask Enter(SelectTargetNodeStateContext context)
        {
            _subscription = _messageBus.Subscribe<NodeSelectedEvent>(OnNodeSelected);

            _startNode = context.StartNode;
            ListPool<IHighlightable>.Get(out var highlightableElements);
            highlightableElements.Add(_startNode.Chip);

            _reachableNodes = _pathFinderService.FindReachableNodes(_startNode);
            highlightableElements.AddRange(_reachableNodes);
            await _messageBus.PublishAsync(new TurnOnHighlightEvent(highlightableElements));
            ListPool<IHighlightable>.Release(highlightableElements);
        }

        private async UniTask OnNodeSelected(NodeSelectedEvent eventData)
        {
            await _messageBus.PublishAsync(new TurnOffHighlightsEvent());

            var targetNode = eventData.NodeModel;
            var isReachableNode = _reachableNodes.Contains(targetNode);

            if (Equals(targetNode, _startNode))
            {
                await _stateMachine.Enter<SelectStartNodeState>();
            }
            else if (!isReachableNode)
            {
                if (targetNode.Chip == null)
                {
                    await _stateMachine.Enter<SelectStartNodeState>();
                }
                else
                {
                    await _stateMachine.Enter<SelectTargetNodeState, SelectTargetNodeStateContext>(
                        new SelectTargetNodeStateContext(targetNode));
                }
            }
            else
            {
                await _stateMachine.Enter<ChipMovingState, ChipMovingStateContext>(
                    new ChipMovingStateContext(_startNode, targetNode));
            }
        }

        public UniTask Exit()
        {
            _subscription?.Dispose();

            return UniTask.CompletedTask;
        }
    }
}