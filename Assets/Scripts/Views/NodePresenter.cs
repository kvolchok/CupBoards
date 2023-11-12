using Events;
using Models;
using UniTaskPubSub;

namespace Views
{
    public class NodePresenter : HighlightableObjectPresenter
    {
        private readonly AsyncMessageBus _messageBus;

        public NodePresenter(NodeModel nodeModel, NodeView nodeView, AsyncMessageBus messageBus, bool isInteractable) :
            base(nodeModel, nodeView)
        {
            _messageBus = messageBus;

            if (isInteractable)
            {
                ((NodeView)View).NodeSelected += OnNodeSelected;
            }
        }

        private async void OnNodeSelected()
        {
            await _messageBus.PublishAsync(new NodeSelectedEvent((NodeModel)Model));
        }

        public override void Dispose()
        {
            base.Dispose();
            ((NodeView)View).NodeSelected -= OnNodeSelected;
        }
    }
}