using Events;
using Models;
using UniTaskPubSub;

namespace Views
{
    public class NodePresenter : HighlightablePresenter
    {
        private readonly NodeModel _nodeModel;
        private readonly NodeView _nodeView;

        public NodePresenter(NodeModel nodeModel, NodeView nodeView, AsyncMessageBus messageBus, bool isInteractable)
            : base(nodeModel, nodeView, messageBus, isInteractable)
        {
            _nodeModel = nodeModel;
            _nodeView = nodeView;
            
            if (!isInteractable)
            {
                return;
            }
            
            _nodeView.NodeSelected += OnNodeSelected;
        }

        private async void OnNodeSelected()
        {
            await _messageBus.PublishAsync(new NodeSelectedEvent(_nodeModel));
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _nodeView.NodeSelected -= OnNodeSelected;
        }
    }
}