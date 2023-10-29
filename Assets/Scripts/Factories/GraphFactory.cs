using System.Collections.Generic;
using Models;
using Settings;
using UnityEngine;

namespace Factories
{
    public class GraphFactory
    {
        public GraphModel CreateStartGraph(LevelSettings levelSettings, GameSettings gameSettings)
        {
            return CreateGraph(levelSettings.NodesPositions, levelSettings.Connections,
                levelSettings.StartChipsPositions, gameSettings.Colors);
        }
    
        public GraphModel CreateTargetGraph(LevelSettings levelSettings, GameSettings gameSettings)
        {
            return CreateGraph(levelSettings.NodesPositions, levelSettings.Connections,
                levelSettings.TargetChipsPositions, gameSettings.Colors);
        }
    
        private GraphModel CreateGraph(List<Vector2Int> nodesPositions, List<Vector2Int> connections,
            List<int> chipsNodes, Color[] colors)
        {
            var nodes = new List<NodeModel>(nodesPositions.Count);

            CreateNodes(nodesPositions, nodes);
        
            SetNeighbours(connections, nodes);

            CreateChips(chipsNodes, colors, nodes);

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

        private void CreateChips(List<int> chipsNodes, Color[] colors, List<NodeModel> nodes)
        {
            for (var chipIndex = 0; chipIndex < chipsNodes.Count; chipIndex++)
            {
                var color = colors[chipIndex];
                var chipModel = new ChipModel(color, chipIndex);

                var chipsNode = chipsNodes[chipIndex] - 1;
                var nodeModel = nodes[chipsNode];
                nodeModel.SetChip(chipModel);
            }
        }
    }
}