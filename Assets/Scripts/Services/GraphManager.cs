using Models;

namespace Services
{
    public class GraphManager
    {
        private GraphModel _startGraph;
        private GraphModel _targetGraph;

        public void SaveGraphs(GraphModel startGraph, GraphModel targetGraph)
        {
            _startGraph = startGraph;
            _targetGraph = targetGraph;
        }
        
        public GraphModel GetStartGraph() => _startGraph;

        public bool CompareGraphs()
        {
            var startGraphNodes = _startGraph.Nodes;
            var targetGraphNodes = _targetGraph.Nodes;
            
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