using Models;
using Settings;
using UniTaskPubSub;
using UnityEngine;
using VContainer;
using Views;

namespace Factories
{
    public class GraphPresenterFactory
    {
        private readonly GameObject _startView;
        private readonly GameObject _targetView;
        private readonly GraphViewPrefabs _graphPrefabs;
        private readonly IObjectResolver _container;
        private readonly AsyncMessageBus _messageBus;

        public GraphPresenterFactory(
            GameSettings gameSettings,
            IObjectResolver container,
            AsyncMessageBus messageBus)
        {
            _startView = gameSettings.StartGraphView;
            _targetView = gameSettings.TargetGraphView;
            _graphPrefabs = gameSettings.GraphViewPrefabs;
            
            _container = container;
            _messageBus = messageBus;
        }

        public GraphPresenter CreateStartPresenter(GraphModel graphModel)
        {
            return new GraphPresenter(graphModel, _startView, _graphPrefabs, _container, _messageBus);
        }
        
        public GraphPresenter CreateTargetPresenter(GraphModel graphModel)
        {
            return new GraphPresenter(graphModel, _targetView, _graphPrefabs, _container, _messageBus);
        }
    }
}