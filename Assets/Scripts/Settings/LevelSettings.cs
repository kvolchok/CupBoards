using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    public class LevelSettings
    {
        public List<Vector2Int> NodesPositions { get; private set; }
        public List<Vector2Int> Connections { get; private set; }
        public List<int> StartChipsPositions { get; private set; }
        public List<int> TargetChipsPositions { get; private set; }
    
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