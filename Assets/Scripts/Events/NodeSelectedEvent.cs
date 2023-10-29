using Models;

namespace Events
{
    public struct NodeSelectedEvent
    {
        public NodeModel NodeModel { get; private set; }
        
        public NodeSelectedEvent(NodeModel nodeModel)
        {
            NodeModel = nodeModel;
        }
    }
}