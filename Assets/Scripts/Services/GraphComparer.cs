using Models;

namespace Services
{
    public class GraphComparer
    {
        public bool Compare(GraphModel startGraph, GraphModel targetGraph)
        {
            var startGraphNodes = startGraph.Nodes;
            var targetGraphNodes = targetGraph.Nodes;

            for (var i = 0; i < startGraphNodes.Count; i++)
            {
                var firstChip = startGraphNodes[i].Chip;
                var secondChip = targetGraphNodes[i].Chip;

                if (firstChip == null && secondChip == null)
                {
                    continue;
                }

                if (firstChip == null || secondChip == null)
                {
                    return false;
                }

                if (firstChip.Id != targetGraphNodes[i].Chip.Id)
                {
                    return false;
                }
            }

            return true;
        }
    }
}