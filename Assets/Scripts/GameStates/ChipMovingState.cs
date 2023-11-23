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
        private readonly GraphService _graphService;
        private readonly IAsyncPublisher _publisher;

        private StateMachine.StateMachine _stateMachine;

        public ChipMovingState(
            PathFinderService pathFinderService,
            GraphService graphService,
            IAsyncPublisher publisher)
        {
            _pathFinderService = pathFinderService;
            _graphService = graphService;
            _publisher = publisher;
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
            await _publisher.PublishAsync(new MoveChipEvent(currentChip, route));
            
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