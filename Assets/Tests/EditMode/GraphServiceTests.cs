using Factories;
using NUnit.Framework;
using Services;
using Settings;
using Tests.SharedTestUtilities;
using Utils;

namespace Tests.EditModeTests
{
    public class GraphServiceTests
    {
        private ILevelSettingsProvider _levelSettingsProvider;
        private IGameSettings _gameSettings;
        private GraphService _graphService;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            var parser = new LevelSettingsParser();
            var userProgressController = new UserProgressController();
            _levelSettingsProvider = new LevelSettingsProvider(parser, userProgressController);
            _levelSettingsProvider.LoadConfigs();

            _gameSettings = new TestGameSettings();

            var graphFactory = new GraphFactory();
            var graphComparer = new GraphComparer();
            _graphService = new GraphService(graphFactory, graphComparer);
        }

        [Test]
        public void CreateGraphs_WithSameSettings()
        {
            // Act
            var levelSettings = _levelSettingsProvider.GetCurrentLevel();
            var startGraph = _graphService.CreateStartGraph(levelSettings, _gameSettings);
            var targetGraph = _graphService.CreateTargetGraph(levelSettings, _gameSettings);

            // Assert
            Assert.AreEqual(startGraph, targetGraph);
        }
        
        [Test]
        public void CompareGraphs_WithSameSettings()
        {
            // Act
            var levelSettings = _levelSettingsProvider.GetCurrentLevel();
            var startGraph = _graphService.CreateStartGraph(levelSettings, _gameSettings);
            var targetGraph = _graphService.CreateStartGraph(levelSettings, _gameSettings);

            var areGraphsEqual = _graphService.CompareGraphs(startGraph, targetGraph);

            // Assert
            Assert.AreEqual(areGraphsEqual, true);
        }
    }
}