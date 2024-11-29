using Factories;
using Models;
using Settings;

namespace Services
{
    public class GraphService
    {
        private readonly GraphFactory _graphFactory;
        private readonly GraphComparer _graphComparer;

        protected GraphModel StartGraph;
        private GraphModel _targetGraph;

        public GraphService(GraphFactory graphFactory, GraphComparer graphComparer)
        {
            _graphFactory = graphFactory;
            _graphComparer = graphComparer;
        }

        public GraphModel CreateStartGraph(ILevelSettings levelSettings, IGameSettings gameSettings)
        {
            StartGraph = _graphFactory.CreateGraph(
                levelSettings.NodesPositions,
                levelSettings.Connections,
                levelSettings.StartChipsPositions,
                gameSettings.Colors);

            return StartGraph;
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

        public GraphModel GetStartGraph() => StartGraph;

        public bool CompareGraphs()
        {
            return CompareGraphs(StartGraph, _targetGraph);
        }
        
        public bool CompareGraphs(GraphModel startGraph, GraphModel targetGraph)
        {
            return _graphComparer.Compare(startGraph, targetGraph);
        }
    }
}