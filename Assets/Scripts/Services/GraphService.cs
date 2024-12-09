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

        public GraphModel CreateStartGraph(ILevelSettings levelSettings, IGameSettings gameSettings)
        {
            _startGraph = _graphFactory.CreateGraph(
                levelSettings.NodesPositions,
                levelSettings.Connections,
                levelSettings.StartChipsPositions,
                gameSettings.Colors);

            return _startGraph;
        }

        public GraphModel CreateTargetGraph(ILevelSettings levelSettings, IGameSettings gameSettings)
        {
            _targetGraph = _graphFactory.CreateGraph(
                levelSettings.NodesPositions,
                levelSettings.Connections,
                levelSettings.TargetChipsPositions,
                gameSettings.Colors);

            return _targetGraph;
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