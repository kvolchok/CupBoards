using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Events;
using Models;
using Services;
using StateMachine;
using UniTaskPubSub;

namespace GameStates
{
    public class SelectTargetNodeState : IStateWithContext<SelectTargetNodeStateContext>
    {
        private readonly PathFinderService _pathFinderService;
        private readonly HighlightService _highlightService;
        private readonly AsyncMessageBus _messageBus;

        private StateMachine.StateMachine _stateMachine;
        private IDisposable _subscription;
        private NodeModel _startNode;
        private NodeModel[] _reachableNodes;

        public SelectTargetNodeState(
            PathFinderService pathFinderService,
            HighlightService highlightService,
            AsyncMessageBus messageBus)
        {
            _pathFinderService = pathFinderService;
            _highlightService = highlightService;
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
            await _messageBus.PublishAsync(new ChipHighlightedEvent(_startNode.Chip, true));
            // _highlightService.TurnOnHighlight(_startNode.Chip);

            _reachableNodes = _pathFinderService.FindReachableNodes(_startNode).ToArray();
            await _messageBus.PublishAsync(new NodesHighlightedEvent(_reachableNodes, true));
            // _highlightService.TurnOnHighlight(_reachableNodes);

            // return UniTask.CompletedTask;
        }

        private async UniTask OnNodeSelected(NodeSelectedEvent eventData)
        {
            var targetNode = eventData.NodeModel;
            var isReachableNode = targetNode.Highlighted;

            if (targetNode == _startNode)
            {
                await _messageBus.PublishAsync(new ChipHighlightedEvent(_startNode.Chip, false));
                await _messageBus.PublishAsync(new NodesHighlightedEvent(_reachableNodes, false));
                // _highlightService.TurnOffHighlight();
                
                await _stateMachine.Enter<SelectStartNodeState>();
            }
            else if (!isReachableNode)
            {
                await _messageBus.PublishAsync(new ChipHighlightedEvent(_startNode.Chip, false));
                await _messageBus.PublishAsync(new NodesHighlightedEvent(_reachableNodes, false));
                // _highlightService.TurnOffHighlight();
                
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
                await _messageBus.PublishAsync(new ChipHighlightedEvent(_startNode.Chip, false));
                await _messageBus.PublishAsync(new NodesHighlightedEvent(_reachableNodes, false));
                // _highlightService.TurnOffHighlight();
                
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