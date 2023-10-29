using System.Collections.Generic;
using Models;
using UniTaskPubSub;

namespace Views
{
    public class GraphPresenter
    {
        private readonly GraphView _view;
        private readonly GraphModel _model;
        
        private readonly List<NodePresenter> _nodePresenters = new();
        private readonly List<ChipPresenter> _chipPresenters = new();

        public GraphPresenter(GraphView view, GraphModel model)
        {
            _view = view;
            _model = model;
        }
        public void ClearView()
        {
            _view.ClearGraph();
        }
        
        public void Show()
        {
            _view.ShowGraph(_model);
        }

        public void CreateNodePresenters(AsyncMessageBus messageBus)
        {
            foreach (var keyValuePair in _view.NodeViewByModel)
            {
                var nodePresenter = new NodePresenter(keyValuePair.Key, keyValuePair.Value, messageBus);
                _nodePresenters.Add(nodePresenter);
            }
        }
        
        public void CreateChipPresenters()
        {
            foreach (var keyValuePair in _view.ChipViewByModel)
            {
                var chipPresenter = new ChipPresenter(keyValuePair.Key, keyValuePair.Value);
                _chipPresenters.Add(chipPresenter);
            }
        }
    }
}