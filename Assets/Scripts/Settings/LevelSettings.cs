using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    public class LevelSettings : ILevelSettings
    {
        public List<Vector2Int> NodesPositions { get; }
        public List<Vector2Int> Connections { get; }
        public List<int> StartChipsPositions { get; }
        public List<int> TargetChipsPositions { get; }

        public LevelSettings(
            List<Vector2Int> nodesPositions,
            List<Vector2Int> connections,
            List<int> startChipsPositions,
            List<int> targetChipsPositions)
        {
            NodesPositions = nodesPositions;
            Connections = connections;
            StartChipsPositions = startChipsPositions;
            TargetChipsPositions = targetChipsPositions;
        }
    }
}