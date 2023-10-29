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
        private IDisposable _subscribtion;
        private NodeModel _startNode;

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
        
        public UniTask Enter(SelectTargetNodeStateContext context)
        {
            _subscribtion = _messageBus.Subscribe<NodeSelectedEvent>(OnNodeSelected);
            
            _startNode = context.StartNode;
            _highlightService.TurnOnHighlight(_startNode.Chip);

            var reachableNodes = _pathFinderService.FindReachableNodes(_startNode).ToArray();
            _highlightService.TurnOnHighlight(reachableNodes);

            return UniTask.CompletedTask;
        }

        private async UniTask OnNodeSelected(NodeSelectedEvent eventData)
        {
            var targetNode = eventData.NodeModel;
            var isReachableNode = targetNode.Highlighted;

            if (targetNode == _startNode)
            {
                _highlightService.TurnOffHighlight();
                
                await _stateMachine.Enter<SelectStartNodeState>();
            }
            else if (!isReachableNode)
            {
                _highlightService.TurnOffHighlight();
                
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
                _highlightService.TurnOffHighlight();
                
                await _stateMachine.Enter<ChipMovingState, ChipMovingStateContext>(
                    new ChipMovingStateContext(_startNode, targetNode));    
            }
        }

        public UniTask Exit()
        {
            _subscribtion?.Dispose();
            
            return UniTask.CompletedTask;
        }
    }
}