using Factories;
using Models;
using Settings;

namespace Services
{
    public class GraphService
    {
        private readonly GraphFactory _graphFactory;
        private readonly GraphComparer _graphComparer;

        private GraphModel _startGraph;
        private GraphModel _targetGraph;

        public GraphService(GraphFactory graphFactory, GraphComparer graphComparer)
        {
            _graphFactory = graphFactory;
            _graphComparer = graphComparer;
        }

        public GraphModel CreateGraph(ILevelSettings levelSettings, IGameSettings gameSettings, bool isStartGraph)
        {
            var chipsPositions = isStartGraph ? 
                levelSettings.StartChipsPositions : levelSettings.TargetChipsPositions;
            
            var graph = _graphFactory.CreateGraph(levelSettings.NodesPositions, levelSettings.Connections,
                chipsPositions, gameSettings.Colors);

            if (isStartGraph)
            {
                _startGraph = graph;
            }
            else
            {
                _targetGraph = graph;
            }

            return graph;
        }

        public GraphModel GetStartGraph() => _startGraph;

        public bool CompareGraphs()
        {
            return CompareGraphs(_startGraph, _targetGraph);
        }

        public bool CompareGraphs(GraphModel startGraph, GraphModel targetGraph)
        {
            return _graphComparer.Compare(startGraph, targetGraph);
        }
    }
}