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

        private readonly Dictionary<IHighlightable, HighlightablePresenter> _presenterByModel = new();
        private readonly List<EdgeView> _edges = new();
        private readonly HashSet<HighlightableView> _renderedEdgesFromView = new();

        public GraphPresenter(GraphModel graphModel, GameObject graphRoot, GraphElementsFactory graphElementsFactory)
        {
            _model = graphModel;
            _root = graphRoot;
            _graphElementsFactory = graphElementsFactory;
        }

        public void ClearView()
        {
            DestroyViews(_presenterByModel);

            foreach (var edgeView in _edges)
            {
                Object.Destroy(edgeView.gameObject);
            }

            _edges.Clear();
            _renderedEdgesFromView.Clear();
        }

        public void ShowGraph(bool isInteractable)
        {
            ShowNodesAndChips(_model, isInteractable);
            ShowEdges(_model);
        }

        private void DestroyViews(Dictionary<IHighlightable, HighlightablePresenter> dictionary)
        {
            foreach (var presenter in dictionary.Values)
            {
                presenter.Dispose();
                presenter.ClearView();
            }

            dictionary.Clear();
        }

        private void ShowNodesAndChips(GraphModel graphModel, bool isInteractable)
        {
            foreach (var nodeModel in graphModel.Nodes)
            {
                var nodePresenter = _graphElementsFactory.CreateNodePresenter(
                    _root.transform, nodeModel, isInteractable);
                _presenterByModel[nodeModel] = nodePresenter;

                var chipModel = nodeModel.Chip;
                if (chipModel == null)
                {
                    continue;
                }

                var chipPresenter = _graphElementsFactory.CreateChipPresenter(
                    _root.transform, nodeModel.Position, chipModel, isInteractable);
                _presenterByModel[chipModel] = chipPresenter;
            }
        }

        private void ShowEdges(GraphModel graphModel)
        {
            foreach (var nodeModel in graphModel.Nodes)
            {
                var currentNodePresenter = _presenterByModel[nodeModel];
                var currentNodeView = currentNodePresenter.View;

                foreach (var neighbourModel in nodeModel.Neighbours)
                {
                    var neighbourNodePresenter = _presenterByModel[neighbourModel];
                    var neighbourView = neighbourNodePresenter.View;

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