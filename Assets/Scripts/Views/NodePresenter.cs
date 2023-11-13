using Events;
using Models;
using UniTaskPubSub;

namespace Views
{
    public class NodePresenter : HighlightableObjectPresenter
    {
        public NodePresenter(NodeModel nodeModel, NodeView nodeView, AsyncMessageBus messageBus, bool isInteractable) :
            base(nodeModel, nodeView, messageBus, isInteractable)
        {
            if (!isInteractable)
            {
                return;
            }
            
            ((NodeView)View).NodeSelected += OnNodeSelected;
        }

        private async void OnNodeSelected()
        {
            await _messageBus.PublishAsync(new NodeSelectedEvent((NodeModel)_model));
        }

        public override void Dispose()
        {
            base.Dispose();
            
            ((NodeView)View).NodeSelected -= OnNodeSelected;
        }
    }
}