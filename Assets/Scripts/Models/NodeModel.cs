using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class NodeModel : IHighlightable
    {
        public IReadOnlyList<NodeModel> Neighbours => _neighbours;

        public Vector3 Position { get; }
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

        #region Equality members

        public override bool Equals(object obj)
        {
            return obj is NodeModel nodeModel && Equals(nodeModel);
        }

        private bool Equals(NodeModel other)
        {
            if (other == null || GetType() != other.GetType())
            {
                return false;
            }
            
            if (_neighbours.Count != other.Neighbours.Count)
            {
                return false;
            }
            
            for (var i = 0; i < _neighbours.Count; i++)
            {
                var currentNodeModel = _neighbours[i];
                var otherNodeModel = other.Neighbours[i];
                if (currentNodeModel.Position != otherNodeModel.Position)
                {
                    return false;
                }
            }

            return Position == other.Position && Chip?.Id == other.Chip?.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_neighbours, Position, Chip);
        }
        
        public static bool operator ==(NodeModel current, NodeModel other)
        {
            return Equals(current, other);
        }

        public static bool operator !=(NodeModel current, NodeModel other)
        {
            return !Equals(current, other);
        }

        #endregion
    }
}