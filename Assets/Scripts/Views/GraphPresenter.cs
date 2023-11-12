using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Events;
using Extensions;
using Models;
using Settings;
using UniTaskPubSub;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Views
{
    public class GraphPresenter : IDisposable
    {
        private readonly GraphModel _model;
        private readonly GameObject _view;

        private readonly NodeView _nodePrefab;
        private readonly ChipView _chipPrefab;
        private readonly EdgeView _edgePrefab;

        private readonly IObjectResolver _container;
        private readonly AsyncMessageBus _messageBus;

        private readonly Dictionary<NodeModel, NodePresenter> _nodePresenterByModel = new();
        private readonly Dictionary<ChipModel, ChipPresenter> _chipPresenterByModel = new();

        private readonly Dictionary<NodeModel, NodeView> _nodeViewByModel = new();
        private readonly Dictionary<ChipModel, ChipView> _chipViewByModel = new();

        private readonly List<EdgeView> _edges = new();
        private readonly HashSet<NodeView> _renderedEdgesFromView = new();
        private readonly CompositeDisposable _subscriptions;

        public GraphPresenter(
            GraphModel model,
            GameObject view,
            GraphViewPrefabs graphPrefabs,
            IObjectResolver container,
            AsyncMessageBus messageBus)
        {
            _model = model;
            _view = view;

            _nodePrefab = graphPrefabs.NodePrefab;
            _chipPrefab = graphPrefabs.ChipPrefab;
            _edgePrefab = graphPrefabs.EdgePrefab;

            _container = container;
            _messageBus = messageBus;

            _subscriptions = new CompositeDisposable
            {
                _messageBus.Subscribe<ChipHighlightedEvent>(OnChipHighlighted),
                _messageBus.Subscribe<NodesHighlightedEvent>(OnNodesHighlighted)
            };
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

        public void ShowGraph(bool isInteractable)
        {
            ShowNodesAndChips(_model);

            ShowEdges(_model);

            if (isInteractable)
            {
                CreateNodePresenters();
                CreateChipPresenters();
            }
        }
        
        private UniTask OnChipHighlighted(ChipHighlightedEvent eventData)
        {
            var chipModel = eventData.ChipModel;
            var chipPresenter = _chipPresenterByModel[chipModel];
            chipPresenter.Highlight(eventData.IsActive);

            return UniTask.CompletedTask;
        }
        
        private UniTask OnNodesHighlighted(NodesHighlightedEvent eventData)
        {
            var nodeModels = eventData.NodeModels;
            foreach (var nodeModel in nodeModels)
            {
                var nodePresenter = _nodePresenterByModel[nodeModel];
                nodePresenter.Highlight(eventData.IsActive);
            }
            
            return UniTask.CompletedTask;
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
                var nodePresenter = new NodePresenter(modeModel, nodeView, _messageBus);
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

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
    }
}