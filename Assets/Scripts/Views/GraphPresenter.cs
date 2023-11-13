using System.Collections.Generic;
using Factories;
using Models;
using UnityEngine;

namespace Views
{
    public class GraphPresenter
    {
        private readonly GraphModel _model;
        private readonly GameObject _root;
        private readonly GraphElementsFactory _graphElementsFactory;

        private readonly Dictionary<NodeModel, NodePresenter> _nodePresenterByModel = new();
        private readonly Dictionary<ChipModel, ChipPresenter> _chipPresenterByModel = new();

        private readonly List<EdgeView> _edges = new();
        private readonly HashSet<NodeView> _renderedEdgesFromView = new();
        
        public GraphPresenter(GraphModel graphModel, GameObject graphRoot, GraphElementsFactory graphElementsFactory)
        {
            _model = graphModel;
            _root = graphRoot;
            
            _graphElementsFactory = graphElementsFactory;
        }

        public void ClearView()
        {
            DestroyViews(_nodePresenterByModel);
            DestroyViews(_chipPresenterByModel);

            foreach (var edgeView in _edges)
            {
                edgeView.Destroy();
            }

            _edges.Clear();

            _renderedEdgesFromView.Clear();
        }

        public void ShowGraph(bool isInteractable)
        {
            ShowNodesAndChips(_model, isInteractable);

            ShowEdges(_model);
        }

        private void DestroyViews<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
            where TValue : HighlightablePresenter
        {
            foreach (var (_, presenter) in dictionary)
            {
                presenter.View.Destroy();
                presenter.Dispose();
            }
 
            dictionary.Clear();
        }

        private void ShowNodesAndChips(GraphModel graphModel, bool isInteractable)
        {
            foreach (var nodeModel in graphModel.Nodes)
            {
                var nodePresenter = _graphElementsFactory.CreateNodePresenter(
                    _root.transform, nodeModel, isInteractable);
                _nodePresenterByModel[nodeModel] = nodePresenter;

                var chipModel = nodeModel.Chip;
                if (chipModel == null)
                {
                    continue;
                }

                var chipPresenter = _graphElementsFactory.CreateChipPresenter(
                    _root.transform, nodeModel.Position, chipModel, isInteractable);
                _chipPresenterByModel[chipModel] = chipPresenter;
            }
        }

        private void ShowEdges(GraphModel graphModel)
        {
            foreach (var nodeModel in graphModel.Nodes)
            {
                var currentNodePresenter = _nodePresenterByModel[nodeModel];
                var currentNodeView = currentNodePresenter.View as NodeView;

                foreach (var neighbourModel in nodeModel.Neighbours)
                {
                    var neighbourNodePresenter = _nodePresenterByModel[neighbourModel];
                    var neighbourView = neighbourNodePresenter.View as NodeView;
                    
                    if (_renderedEdgesFromView.Contains(neighbourView))
                    {
                        continue;
                    }

                    var edgeView = _graphElementsFactory.CreateEdge(_root.transform);
                    edgeView.Show(currentNodeView, neighbourView);
                    _edges.Add(edgeView);
                }

                _renderedEdgesFromView.Add(currentNodeView);
            }
        }
    }
}