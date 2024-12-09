using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Factories
{
    public class GraphFactory
    {
        public GraphModel CreateGraph(List<Vector2Int> nodesPositions, List<Vector2Int> connections,
            List<int> chipsInNodes, Color[] colors)
        {
            var nodes = new List<NodeModel>(nodesPositions.Count);

            CreateNodes(nodesPositions, nodes);

            SetNeighbours(connections, nodes);

            CreateChips(chipsInNodes, colors, nodes);

            return new GraphModel(nodes);
        }

        private void CreateNodes(List<Vector2Int> nodesPositions, List<NodeModel> nodes)
        {
            foreach (var nodePosition in nodesPositions)
            {
                var position = new Vector3(nodePosition.x, nodePosition.y, 0);
                var nodeModel = new NodeModel(position);
                nodes.Add(nodeModel);
            }
        }

        private void SetNeighbours(List<Vector2Int> connections, List<NodeModel> nodes)
        {
            foreach (var connection in connections)
            {
                var firstNode = nodes[connection.x - 1];
                var secondNode = nodes[connection.y - 1];

                firstNode.AddNeighbour(secondNode);
                secondNode.AddNeighbour(firstNode);
            }
        }

        private void CreateChips(IReadOnlyList<int> chipsInNodes, IReadOnlyList<Color> colors,
            IReadOnlyList<NodeModel> nodes)
        {
            for (var chipIndex = 0; chipIndex < chipsInNodes.Count; chipIndex++)
            {
                var color = colors[chipIndex];
                var chipModel = new ChipModel(color, chipIndex);

                var chipsInNode = chipsInNodes[chipIndex] - 1;
                var nodeModel = nodes[chipsInNode];
                nodeModel.SetChip(chipModel);
            }
        }
    }
}