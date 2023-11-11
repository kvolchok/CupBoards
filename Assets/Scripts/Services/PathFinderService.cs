using System.Collections.Generic;
using Models;

namespace Services
{
    public class PathFinderService
    {
        private readonly GraphService _graphService;
        
        private readonly Queue<NodeModel> _nodesToVisit = new();
        private readonly HashSet<NodeModel> _visitedNodes = new();
        
        private readonly Dictionary<NodeModel, int> _distancesFromStartNode = new();
        private readonly Dictionary<NodeModel, NodeModel> _previousNodes = new();
        private readonly List<NodeModel> _unvisitedNodes = new();
        private readonly Stack<NodeModel> _route = new();

        public PathFinderService(GraphService graphService)
        {
            _graphService = graphService;
        }

        public IEnumerable<NodeModel> FindReachableNodes(NodeModel startNode)
        {
            _nodesToVisit.Clear();
            _visitedNodes.Clear();
            
            _nodesToVisit.Enqueue(startNode);

            while (_nodesToVisit.Count > 0)
            {
                var currentNode = _nodesToVisit.Dequeue();
                _visitedNodes.Add(currentNode);

                var neighbours = currentNode.Neighbours;

                foreach (var neighbour in neighbours)
                {
                    if (_visitedNodes.Contains(neighbour) || neighbour.Chip != null)
                    {
                        continue;
                    }
                    
                    _nodesToVisit.Enqueue(neighbour);
                }
            }

            _visitedNodes.Remove(startNode);
            
            return _visitedNodes;
        }

        public Stack<NodeModel> FindRoute(NodeModel startNode, NodeModel targetNode)
        {
            var startGraph = _graphService.GetStartGraph();
            var nodes = startGraph.Nodes;

            ClearPreviousRouteCalculation(nodes);

            _distancesFromStartNode[startNode] = 0;

            while (_unvisitedNodes.Count > 0)
            {
                var currentNode = FindNodeWithMinDistance();

                _unvisitedNodes.Remove(currentNode);

                if (currentNode == targetNode)
                {
                    break;
                }

                FindAllRoutesFrom(currentNode);
            }
            
            return GetRoute(targetNode);
        }

        private void ClearPreviousRouteCalculation(IReadOnlyList<NodeModel> nodes)
        {
            _distancesFromStartNode.Clear();
            _previousNodes.Clear();
            _unvisitedNodes.Clear();
            _route.Clear();

            foreach (var node in nodes)
            {
                _distancesFromStartNode[node] = int.MaxValue;
                _previousNodes[node] = null;
                _unvisitedNodes.Add(node);
            }
        }
        
        private NodeModel FindNodeWithMinDistance()
        {
            var minDistance = int.MaxValue;
            NodeModel minNode = null;

            foreach (var unvisitedNode in _unvisitedNodes)
            {
                if (_distancesFromStartNode[unvisitedNode] < minDistance)
                {
                    minDistance = _distancesFromStartNode[unvisitedNode];
                    minNode = unvisitedNode;
                }
            }

            return minNode;
        }
        
        private void FindAllRoutesFrom(NodeModel currentNode)
        {
            var neighbours = currentNode.Neighbours;
            foreach (var neighbour in neighbours)
            {
                var distance = _distancesFromStartNode[currentNode] + 1;

                if (distance < _distancesFromStartNode[neighbour] && neighbour.Chip == null)
                {
                    _distancesFromStartNode[neighbour] = distance;
                    _previousNodes[neighbour] = currentNode;
                }
            }
        }
        
        private Stack<NodeModel> GetRoute(NodeModel targetNode)
        {
            var currentNode = targetNode;

            while (currentNode != null)
            {
                _route.Push(currentNode);
                currentNode = _previousNodes[currentNode];
            }

            return _route;
        }
    }
}