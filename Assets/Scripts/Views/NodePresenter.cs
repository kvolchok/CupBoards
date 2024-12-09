using Events;
using Models;
using UniTaskPubSub;

namespace Views
{
    public class NodePresenter : HighlightablePresenter
    {
        public NodeView NodeView { get; }

        private readonly NodeModel _nodeModel;

        public NodePresenter(NodeModel nodeModel, NodeView nodeView, AsyncMessageBus messageBus, bool isInteractable)
            : base(nodeModel, nodeView, messageBus, isInteractable)
        {
            _nodeModel = nodeModel;
            NodeView = nodeView;

            if (!isInteractable)
            {
                return;
            }

            NodeView.NodeSelected += OnNodeSelected;
        }

        private async void OnNodeSelected()
        {
            await MessageBus.PublishAsync(new NodeSelectedEvent(_nodeModel));
        }

        public override void Dispose()
        {
            base.Dispose();

            NodeView.NodeSelected -= OnNodeSelected;
        }
    }
}