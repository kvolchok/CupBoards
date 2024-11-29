using Models;

namespace GameStates
{
    public struct SelectTargetNodeStateContext
    {
        public NodeModel StartNode { get; }
        
        public SelectTargetNodeStateContext(NodeModel startNode)
        {
            StartNode = startNode;
        }
    }
}