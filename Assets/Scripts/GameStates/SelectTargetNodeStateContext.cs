using Models;

namespace GameStates
{
    public struct SelectTargetNodeStateContext
    {
        public NodeModel StartNode { get; private set; }
        
        public SelectTargetNodeStateContext(NodeModel startNode)
        {
            StartNode = startNode;
        }
    }
}