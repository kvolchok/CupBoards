using Cysharp.Threading.Tasks;
using Events;
using Services;
using StateMachine;
using UniTaskPubSub;

namespace GameStates
{
    public class ChipMovingState : IStateWithContext<ChipMovingStateContext>
    {
        private readonly PathFinderService _pathFinderService;
        private readonly AsyncMessageBus _messageBus;
        private readonly GraphService _graphService;

        private StateMachine.StateMachine _stateMachine;

        public ChipMovingState(
            PathFinderService pathFinderService,
            AsyncMessageBus messageBus,
            GraphService graphService)
        {
            _pathFinderService = pathFinderService;
            _messageBus = messageBus;
            _graphService = graphService;
        }

        public void Initialize(StateMachine.StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async UniTask Enter(ChipMovingStateContext context)
        {
            var startNode = context.StartNode;
            var targetNode = context.TargetNode;

            var route = _pathFinderService.FindRoute(startNode, targetNode);

            var currentChip = startNode.Chip;
            await _messageBus.PublishAsync(new MoveChipEvent(currentChip, route));
            
            startNode.SetChip(null);
            targetNode.SetChip(currentChip);
            
            var areGraphsEqual = _graphService.CompareGraphs();
            if (areGraphsEqual)
            {
                await _stateMachine.Enter<FinishGameState>();
            }
            else
            {
                await _stateMachine.Enter<SelectStartNodeState>();
            }
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}