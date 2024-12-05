using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Tests.SharedTestUtilities
{
    public class TestGraphModel : GraphModel
    {
        public override IReadOnlyList<NodeModel> Nodes => _nodeModels;
        private readonly List<NodeModel> _nodeModels = new();

        public TestGraphModel(IReadOnlyList<NodeModel> nodes) : base(nodes)
        {
            var nodeModelId0 = new NodeModel(new Vector3(-1, 1));
            nodeModelId0.SetChip(new ChipModel(Color.white, 0));

            var nodeModelId1 = new NodeModel(new Vector3(0, 1));
            nodeModelId1.SetChip(new ChipModel(Color.white, 1));

            var nodeModelId2 = new NodeModel(new Vector3(1, 1));
            nodeModelId2.SetChip(new ChipModel(Color.white, 2));

            var nodeModelId3 = new NodeModel(new Vector3(-1, 0));

            var nodeModelId4 = new NodeModel(new Vector3(0, 0));

            var nodeModelId5 = new NodeModel(new Vector3(1, 0));

            var nodeModelId6 = new NodeModel(new Vector3(-1, -1));
            nodeModelId6.SetChip(new ChipModel(Color.white, 3));

            var nodeModelId7 = new NodeModel(new Vector3(0, -1));
            nodeModelId7.SetChip(new ChipModel(Color.white, 4));

            var nodeModelId8 = new NodeModel(new Vector3(1, -1));
            nodeModelId8.SetChip(new ChipModel(Color.white, 5));
            
            nodeModelId0.AddNeighbour(nodeModelId3);
            nodeModelId3.AddNeighbour(nodeModelId0);
            nodeModelId1.AddNeighbour(nodeModelId4);
            nodeModelId4.AddNeighbour(nodeModelId1);
            nodeModelId2.AddNeighbour(nodeModelId5);
            nodeModelId5.AddNeighbour(nodeModelId2);
            nodeModelId3.AddNeighbour(nodeModelId4);
            nodeModelId4.AddNeighbour(nodeModelId3);
            nodeModelId4.AddNeighbour(nodeModelId5);
            nodeModelId5.AddNeighbour(nodeModelId4);
            nodeModelId3.AddNeighbour(nodeModelId6);
            nodeModelId6.AddNeighbour(nodeModelId3);
            nodeModelId4.AddNeighbour(nodeModelId7);
            nodeModelId7.AddNeighbour(nodeModelId4);
            nodeModelId5.AddNeighbour(nodeModelId8);
            nodeModelId8.AddNeighbour(nodeModelId5);
            
            _nodeModels.Add(nodeModelId0);
            _nodeModels.Add(nodeModelId1);
            _nodeModels.Add(nodeModelId2);
            _nodeModels.Add(nodeModelId3);
            _nodeModels.Add(nodeModelId4);
            _nodeModels.Add(nodeModelId5);
            _nodeModels.Add(nodeModelId6);
            _nodeModels.Add(nodeModelId7);
            _nodeModels.Add(nodeModelId8);
        }
    }
}