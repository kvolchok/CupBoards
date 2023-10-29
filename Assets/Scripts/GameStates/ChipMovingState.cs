using Cysharp.Threading.Tasks;
using Services;
using StateMachine;

namespace GameStates
{
    public class ChipMovingState : IStateWithContext<ChipMovingStateContext>
    {
        private readonly PathFinderService _pathFinderService;
        private readonly ChipMovingService _chipMovingService;
        private readonly GraphManager _graphManager;

        private StateMachine.StateMachine _stateMachine;

        public ChipMovingState(
            PathFinderService pathFinderService,
            ChipMovingService chipMovingService,
            GraphManager graphManager)
        {
            _pathFinderService = pathFinderService;
            _chipMovingService = chipMovingService;
            _graphManager = graphManager;
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
            await _chipMovingService.MoveChip(currentChip, route);
            
            var areGraphsEqual = _graphManager.CompareGraphs();
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