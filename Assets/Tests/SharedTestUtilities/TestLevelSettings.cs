using System.Collections.Generic;
using Settings;
using UnityEngine;

namespace Tests.SharedTestUtilities
{
    public class TestLevelSettings : ILevelSettings
    {
        public List<Vector2Int> NodesPositions { get; } = new()
        {
            new Vector2Int(-1, 1),
            new Vector2Int(0, 1),
            new Vector2Int(1, 1),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 0),
            new Vector2Int(1, 0),
            new Vector2Int(-1, -1),
            new Vector2Int(0, -1),
            new Vector2Int(1, -1)
        };
        
        public List<Vector2Int> Connections { get; } = new()
        {
            new Vector2Int(1, 4),
            new Vector2Int(2, 5),
            new Vector2Int(3, 6),
            new Vector2Int(4, 5),
            new Vector2Int(5, 6),
            new Vector2Int(4, 7),
            new Vector2Int(5, 8),
            new Vector2Int(6, 9)
        };
        
        public List<int> StartChipsPositions { get; } = new()
        {
            1, 2, 3, 7, 8, 9
        };
        
        public List<int> TargetChipsPositions { get; } = new()
        {
            7, 8, 9, 1, 2, 3
        };
    }
}