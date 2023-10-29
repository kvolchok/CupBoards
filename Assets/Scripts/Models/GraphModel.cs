using System.Collections.Generic;

namespace Models
{
    public class GraphModel
    {
        public IReadOnlyList<NodeModel> Nodes { get; }

        public GraphModel(IReadOnlyList<NodeModel> nodes)
        {
            Nodes = nodes;
        }
    }
}