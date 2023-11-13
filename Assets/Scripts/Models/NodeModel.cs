using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class NodeModel : IHighlightable
    { 
        public IReadOnlyList<NodeModel> Neighbours => _neighbours;
    
        public Vector3 Position { get; private set; }
        public ChipModel Chip { get; private set; }
        
        private readonly List<NodeModel> _neighbours = new();
    
        public NodeModel(Vector3 position)
        {
            Position = position;
        }
    
        public void SetChip(ChipModel chip)
        {
            Chip = chip;
        }
    
        public void AddNeighbour(NodeModel node)
        {
            _neighbours.Add(node);
        }
    }
}