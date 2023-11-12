using Models;

namespace Events
{
    public struct NodesHighlightedEvent
    {
        public NodeModel[] NodeModels { get; }
        public bool IsActive { get; }

        public NodesHighlightedEvent(NodeModel[] nodeModels, bool isActive)
        {
            NodeModels = nodeModels;
            IsActive = isActive;
        }
    }
}