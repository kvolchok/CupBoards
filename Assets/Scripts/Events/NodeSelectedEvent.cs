using Models;

namespace Events
{
    public struct NodeSelectedEvent
    {
        public NodeModel NodeModel { get; }

        public NodeSelectedEvent(NodeModel nodeModel)
        {
            NodeModel = nodeModel;
        }
    }
}