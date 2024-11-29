using Factories;
using GameStates;
using Services;
using Settings;
using Tests.SharedTestUtilities;
using UniTaskPubSub;
using UnityEngine;
using Utils;
using VContainer;
using VContainer.Unity;
using Views;

namespace Tests.PlayMode.LifeTimeScope
{
    public class IntegrationTestsLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private GameSettings _gameSettings;
        [SerializeField]
        private GameOverScreen _gameOverScreen;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            RegisterGameStateMachine(builder);

            builder.Register<AsyncMessageBus>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();

            builder.Register<GameOverPresenterFactory>(Lifetime.Singleton);
            builder.Register<GameOverPresenter>(Lifetime.Singleton);

            builder.Register<LevelSettingsParser>(Lifetime.Singleton);
            builder.Register<LevelSettingsProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<UserProgressController>(Lifetime.Singleton);

            builder.Register<GraphPresenterFactory>(Lifetime.Singleton);
            builder.Register<GraphElementsFactory>(Lifetime.Singleton);

            builder.Register<TestGraphService>(Lifetime.Singleton)
                .As<GraphService>()
                .AsSelf();

            builder.Register<GraphFactory>(Lifetime.Singleton);
            builder.Register<GraphComparer>(Lifetime.Singleton);

            builder.Register<PathFinderService>(Lifetime.Singleton);
            builder.Register<TestGameController>(Lifetime.Singleton);

            builder.RegisterInstance(_gameSettings).AsImplementedInterfaces();
            builder.RegisterInstance(_gameSettings.GraphViewPrefabs);
            builder.RegisterInstance(_gameOverScreen);

            builder.RegisterEntryPoint<UIController>();
        }

        private void RegisterGameStateMachine(IContainerBuilder builder)
        {
            builder.Register<GameStateMachine>(Lifetime.Singleton);

            builder.Register<BootstrapState>(Lifetime.Singleton);
            builder.Register<LevelLoadState>(Lifetime.Singleton);
            builder.Register<SelectStartNodeState>(Lifetime.Singleton);
            builder.Register<SelectTargetNodeState>(Lifetime.Singleton);
            builder.Register<ChipMovingState>(Lifetime.Singleton);
            builder.Register<FinishGameState>(Lifetime.Singleton);
        }
    }
}