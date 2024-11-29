using System.Collections.Generic;
using System.Linq;
using Factories;
using Models;
using NUnit.Framework;
using Services;
using Settings;
using Tests.SharedTestUtilities;
using Utils;

namespace Tests.EditModeTests
{
    public class PathFinderServiceTests
    {
        private PathFinderService _pathFinderService;
        private IReadOnlyList<NodeModel> _startGraphNodes;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            var parser = new LevelSettingsParser();
            var userProgressController = new UserProgressController();
            var levelSettingsProvider = new LevelSettingsProvider(parser, userProgressController);
            levelSettingsProvider.LoadConfigs();


            var graphFactory = new GraphFactory();
            var graphComparer = new GraphComparer();
            var graphService = new GraphService(graphFactory, graphComparer);

            var levelSettings = levelSettingsProvider.GetCurrentLevel();
            var gameSettings = new TestGameSettings();
            var startGraph = graphService.CreateStartGraph(levelSettings, gameSettings);
            _startGraphNodes = startGraph.Nodes;

            _pathFinderService = new PathFinderService(graphService);
        }

        [Test]
        [TestCase(3, 0)]
        [TestCase(4, 1)]
        [TestCase(5, 2)]
        public void FindReachableNodes_FromFirstNode(int expectedNodeIndex, int reachableNodeIndex)
        {
            // Act
            var startGraphNode = _startGraphNodes[0];
            var reachableNodes = _pathFinderService.FindReachableNodes(startGraphNode).ToList();
            
            // Assert
            Assert.AreEqual(_startGraphNodes[expectedNodeIndex], reachableNodes[reachableNodeIndex]);
        }
        
        [Test]
        [TestCase(0, 0)]
        [TestCase(3, 1)]
        [TestCase(4, 2)]
        [TestCase(5, 3)]
        public void FindRoute_FromFirstNode(int expectedNodeIndex, int reachableNodeIndex)
        {
            // Act
            var startGraphNode = _startGraphNodes[0];
            var targetGraphNode = _startGraphNodes[5];
            var reachableNodes = _pathFinderService.FindRoute(startGraphNode, targetGraphNode).ToList();
            
            // Assert
            Assert.AreEqual(_startGraphNodes[expectedNodeIndex], reachableNodes[reachableNodeIndex]);
        }
    }
}