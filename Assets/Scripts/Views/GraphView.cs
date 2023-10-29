using System.Collections.Generic;
using Models;
using Settings;
using UnityEngine;
using VContainer;

namespace Views
{
    public class GraphView : MonoBehaviour
    {
        public Dictionary<NodeModel, NodeView> NodeViewByModel { get; } = new();
        public Dictionary<ChipModel, ChipView> ChipViewByModel { get; } = new();
        
        private readonly List<EdgeView> _edges = new();
        private readonly HashSet<NodeView> _renderedEdgesFromView = new();
        
        private NodeView _nodePrefab;
        private ChipView _chipPrefab;
        private EdgeView _edgePrefab;

        [Inject]
        public void Construct(GraphViewPrefabs graphPrefabs)
        {
            _nodePrefab = graphPrefabs.NodePrefab;
            _chipPrefab = graphPrefabs.ChipPrefab;
            _edgePrefab = graphPrefabs.EdgePrefab;
        }
        
        public void ClearGraph()
        {
            DestroyViews(NodeViewByModel);
            DestroyViews(ChipViewByModel);
            
            foreach (var edgeView in _edges)
            {
                Destroy(edgeView.gameObject);
            }
            
            _edges.Clear();
            
            _renderedEdgesFromView.Clear();
        }
        
        public void ShowGraph(GraphModel graphModel)
        {
            ShowNodesAndChips(graphModel);
        
            ShowEdges(graphModel);
        }
        
        private void DestroyViews<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
            where TValue : HighlightableObjectView
        {
            foreach (var (_, view) in dictionary)
            {
                Destroy(view.gameObject);
            }
            
            dictionary.Clear();
        }

        private void ShowNodesAndChips(GraphModel graphModel)
        {
            foreach (var nodeModel in graphModel.Nodes)
            {
                var nodeView = Instantiate(_nodePrefab, transform);
                nodeView.Initialize(nodeModel.Position);
                NodeViewByModel[nodeModel] = nodeView;

                if (nodeModel.Chip == null)
                {
                    continue;
                }

                var chipView = Instantiate(_chipPrefab, transform);
                chipView.Initialize(nodeModel.Position, nodeModel.Chip);
                ChipViewByModel[nodeModel.Chip] = chipView;
            }
        }

        private void ShowEdges(GraphModel graphModel)
        {
            foreach (var nodeModel in graphModel.Nodes)
            {
                var currentNodeView = NodeViewByModel[nodeModel];

                foreach (var neighbourModel in nodeModel.Neighbours)
                {
                    var neighbourView = NodeViewByModel[neighbourModel];
                
                    if (_renderedEdgesFromView.Contains(neighbourView))
                    {
                        continue;
                    }
                    
                    var edge = Instantiate(_edgePrefab, transform);
                    edge.Show(currentNodeView, neighbourView);
                    _edges.Add(edge);
                }

                _renderedEdgesFromView.Add(currentNodeView);
            }
        }
    }
}