using Models;

namespace GameStates
{
    public struct ChipMovingStateContext
    {
        public NodeModel StartNode { get; }
        public NodeModel TargetNode { get; }

        public ChipMovingStateContext(NodeModel startNode, NodeModel targetNode)
        {
            StartNode = startNode;
            TargetNode = targetNode;
        }
    }
}