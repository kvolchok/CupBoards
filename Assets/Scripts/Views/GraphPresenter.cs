using System.Collections.Generic;
using Models;
using Settings;
using UniTaskPubSub;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Views
{
    public class GraphPresenter
    {
        private readonly GraphModel _model;
        private readonly GameObject _view;

        private readonly NodeView _nodePrefab;
        private readonly ChipView _chipPrefab;
        private readonly EdgeView _edgePrefab;

        private readonly IObjectResolver _container;
        private readonly AsyncMessageBus _asyncMessageBus;

        private readonly Dictionary<NodeModel, NodePresenter> _nodePresenterByModel = new();
        private readonly Dictionary<ChipModel, ChipPresenter> _chipPresenterByModel = new();

        private readonly Dictionary<NodeModel, NodeView> _nodeViewByModel = new();
        private readonly Dictionary<ChipModel, ChipView> _chipViewByModel = new();

        private readonly List<EdgeView> _edges = new();
        private readonly HashSet<NodeView> _renderedEdgesFromView = new();

        public GraphPresenter(GraphModel model,
            GameObject view,
            GraphViewPrefabs graphPrefabs,
            IObjectResolver container, AsyncMessageBus asyncMessageBus)
        {
            _model = model;
            _view = view;

            _nodePrefab = graphPrefabs.NodePrefab;
            _chipPrefab = graphPrefabs.ChipPrefab;
            _edgePrefab = graphPrefabs.EdgePrefab;

            _container = container;
            _asyncMessageBus = asyncMessageBus;
        }

        public void ClearView()
        {
            DestroyViews(_nodeViewByModel);
            DestroyViews(_chipViewByModel);

            foreach (var edgeView in _edges)
            {
                edgeView.Destroy();
            }

            _edges.Clear();

            _renderedEdgesFromView.Clear();
        }

        public void Show(bool isInteractable = true)
        {
            ShowNodesAndChips(_model);

            ShowEdges(_model);

            if (isInteractable)
            {
                CreateNodePresenters();
                CreateChipPresenters();
            }
        }

        private void DestroyViews<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
            where TValue : HighlightableObjectView
        {
            foreach (var (_, view) in dictionary)
            {
                view.Destroy();
            }

            dictionary.Clear();
        }

        private void ShowNodesAndChips(GraphModel graphModel)
        {
            foreach (var nodeModel in graphModel.Nodes)
            {
                var nodeView = _container.Instantiate(_nodePrefab, _view.transform);
                nodeView.Initialize(nodeModel.Position);
                _nodeViewByModel[nodeModel] = nodeView;

                if (nodeModel.Chip == null)
                {
                    continue;
                }

                var chipView = _container.Instantiate(_chipPrefab, _view.transform);
                chipView.Initialize(nodeModel.Position, nodeModel.Chip);
                _chipViewByModel[nodeModel.Chip] = chipView;
            }
        }

        private void ShowEdges(GraphModel graphModel)
        {
            foreach (var nodeModel in graphModel.Nodes)
            {
                var currentNodeView = _nodeViewByModel[nodeModel];

                foreach (var neighbourModel in nodeModel.Neighbours)
                {
                    var neighbourView = _nodeViewByModel[neighbourModel];

                    if (_renderedEdgesFromView.Contains(neighbourView))
                    {
                        continue;
                    }

                    var edge = _container.Instantiate(_edgePrefab, _view.transform);
                    edge.Show(currentNodeView, neighbourView);
                    _edges.Add(edge);
                }

                _renderedEdgesFromView.Add(currentNodeView);
            }
        }

        private void CreateNodePresenters()
        {
            foreach (var (modeModel, nodeView) in _nodeViewByModel)
            {
                var nodePresenter = new NodePresenter(modeModel, nodeView, _asyncMessageBus);
                _nodePresenterByModel[modeModel] = nodePresenter;
            }
        }

        private void CreateChipPresenters()
        {
            foreach (var (chipModel, chipView) in _chipViewByModel)
            {
                var chipPresenter = new ChipPresenter(chipModel, chipView);
                _chipPresenterByModel[chipModel] = chipPresenter;
            }
        }
    }
}