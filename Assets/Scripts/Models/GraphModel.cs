using System.Collections.Generic;

namespace Models
{
    public class GraphModel
    {
        public virtual IReadOnlyList<NodeModel> Nodes { get; }

        public GraphModel(IReadOnlyList<NodeModel> nodes)
        {
            Nodes = nodes;
        }

        #region Equality members

        public override bool Equals(object obj)
        {
            return obj is GraphModel graphModel && Equals(graphModel);
        }

        private bool Equals(GraphModel other)
        {
            for (var i = 0; i < Nodes.Count; i++)
            {
                var currentNode = Nodes[i];
                var otherNode = other.Nodes[i];
                if (!currentNode.Equals(otherNode))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return Nodes != null ? Nodes.GetHashCode() : 0;
        }

        #endregion
    }
}