using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    public interface ILevelSettings
    {
        List<Vector2Int> NodesPositions { get; }
        List<Vector2Int> Connections { get; }
        List<int> StartChipsPositions { get; }
        List<int> TargetChipsPositions { get; }
    }
}