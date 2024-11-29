using System.Collections;
using NUnit.Framework;
using Tests.PlayMode.LifeTimeScope;
using Tests.SharedTestUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using VContainer;

namespace Tests.PlayMode
{
    public class CupBoardsIntegrationTests
    {
        private const string TEST_SCENE_NAME = "CupBoardsIntegrationTestScene";
        private IObjectResolver _resolver;

        [UnitySetUp]
        public IEnumerator UnitySetUp()
        {
            SceneManager.LoadScene(TEST_SCENE_NAME, LoadSceneMode.Additive);

            yield return new WaitForSeconds(1f);

            var lifetimeScope = Object.FindObjectOfType<IntegrationTestsLifetimeScope>();
            Assert.IsNotNull(lifetimeScope, "Could not find LifetimeScope in the scene.");
            _resolver = lifetimeScope.Container;
        }

        [UnityTearDown]
        public IEnumerator UnityTearDown()
        {
            yield return SceneManager.UnloadSceneAsync(TEST_SCENE_NAME);
            yield return new WaitForSeconds(1f);
        }

        [UnityTest]
        [Category("LongRunning")]
        [Timeout(1000 * 60 * 60 * 24)]
        public IEnumerator Game_With100NodeSelections_WorksWithoutAnyExceptions()
        {
            var gameController = _resolver.Resolve<TestGameController>();
            Assert.IsNotNull(gameController, "Could not resolve YourType from VContainer.");

            gameController.Start();

            for (var i = 0; i < 100; i++)
            {
                gameController.SelectNode();
                yield return new WaitForSeconds(1f);
                Debug.Log($"Move: {i}");
            }

            Assert.Pass();
        }
    }
}