using Factories;
using NUnit.Framework;
using Services;
using Settings;
using Tests.SharedTestUtilities;

namespace Tests.EditMode
{
    public class GraphServiceTests
    {
        private TestGraphModel _testGraphModel;
        private ILevelSettings _levelSettings;
        private TestGameSettings _gameSettings;
        private GraphService _graphService;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            // Creating Test Graph Model
            _testGraphModel = new TestGraphModel(null);
            // Creating Test Level Settings
            _levelSettings = new TestLevelSettings();
            // Creating Test Game Settings
            _gameSettings = new TestGameSettings();

            // Creating Graph Service
            var graphFactory = new GraphFactory();
            var graphComparer = new GraphComparer();
            _graphService = new GraphService(graphFactory, graphComparer);
        }

        [Test]
        public void CreateGraph_WithSameSettings_ReturnsCorrectResult()
        {
            // Act
            var targetGraphModel = _graphService.CreateStartGraph(_levelSettings, _gameSettings);

            // Assert
            Assert.AreEqual(_testGraphModel, targetGraphModel);
        }

        [Test]
        public void CompareGraphs_WithSameSettings_ReturnsTrue()
        {
            // Act
            var targetGraphModel = _graphService.CreateStartGraph(_levelSettings, _gameSettings);
            var areGraphsEqual = _graphService.CompareGraphs(_testGraphModel, targetGraphModel);

            // Assert
            Assert.AreEqual(areGraphsEqual, true);
        }
    }
}