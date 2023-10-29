using Models;

namespace GameStates
{
    public struct ChipMovingStateContext
    {
        public NodeModel StartNode { get; private set; }
        public NodeModel TargetNode { get; private set; }
        
        public ChipMovingStateContext(NodeModel startNode, NodeModel targetNode)
        {
            StartNode = startNode;
            TargetNode = targetNode;
        }
    }
}