using Events;
using Models;
using UniTaskPubSub;

namespace Views
{
    public class NodePresenter : HighlightableObjectPresenter
    {
        private readonly AsyncMessageBus _messageBus;

        public NodePresenter(NodeModel nodeModel, NodeView nodeView, AsyncMessageBus messageBus) :
            base(nodeModel, nodeView)
        {
            _messageBus = messageBus;
            
            ((NodeView)_view).NodeSelected += OnNodeSelected;
        }

        private async void OnNodeSelected()
        {
            await _messageBus.PublishAsync(new NodeSelectedEvent((NodeModel)_model));
        }

        public override void Dispose()
        {
            base.Dispose();
            ((NodeView)_view).NodeSelected -= OnNodeSelected;
        }
    }
}