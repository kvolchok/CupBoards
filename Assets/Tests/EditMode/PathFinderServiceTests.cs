using System.Collections.Generic;
using System.Linq;
using Factories;
using Models;
using NUnit.Framework;
using Services;
using Settings;
using Tests.SharedTestUtilities;
using Utils;

namespace Tests.EditMode
{
    public class PathFinderServiceTests
    {
        private TestGraphModel _testGraphModel;
        private IReadOnlyList<NodeModel> _testGraphNodes;
        private PathFinderService _pathFinderService;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _testGraphModel = new TestGraphModel(null);
            _testGraphNodes = _testGraphModel.Nodes;

            _pathFinderService = new PathFinderService();
        }

        [Test]
        [TestCase(3, 0)]
        [TestCase(4, 1)]
        [TestCase(5, 2)]
        public void FindReachableNodes_FromFirstNode_ReturnsCorrectReachableNodes(
            int expectedNodeIndex, int reachableNodeIndex)
        {
            // Act
            var startGraphNode = _testGraphNodes[0];
            var reachableNodes = _pathFinderService.FindReachableNodes(startGraphNode).ToList();

            // Assert
            Assert.AreEqual(_testGraphNodes[expectedNodeIndex], reachableNodes[reachableNodeIndex]);
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(3, 1)]
        [TestCase(4, 2)]
        [TestCase(5, 3)]
        public void FindRoute_FromFirstNode_ReturnsCorrectRoute(int expectedNodeIndex, int reachableNodeIndex)
        {
            // Act
            var startGraphNode = _testGraphNodes[0];
            var targetGraphNode = _testGraphNodes[5];
            var reachableNodes = _pathFinderService
                .FindRoute(_testGraphModel, startGraphNode, targetGraphNode)
                .ToList();

            // Assert
            Assert.AreEqual(_testGraphNodes[expectedNodeIndex], reachableNodes[reachableNodeIndex]);
        }
    }
}