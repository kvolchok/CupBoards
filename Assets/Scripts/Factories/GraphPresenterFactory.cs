using Models;
using Settings;
using UnityEngine;
using Views;

namespace Factories
{
    public class GraphPresenterFactory
    {
        private readonly GameObject _startGraphRoot;
        private readonly GameObject _targetGraphRoot;
        private readonly GraphElementsFactory _graphElementsFactory;

        public GraphPresenterFactory(GameSettings gameSettings, GraphElementsFactory graphElementsFactory)
        {
            _startGraphRoot = gameSettings.StartGraphRoot;
            _targetGraphRoot = gameSettings.TargetGraphRoot;
            
            _graphElementsFactory = graphElementsFactory;
        }

        public GraphPresenter CreateStartPresenter(GraphModel graphModel)
        {
            return new GraphPresenter(graphModel, _startGraphRoot, _graphElementsFactory);
        }
        
        public GraphPresenter CreateTargetPresenter(GraphModel graphModel)
        {
            return new GraphPresenter(graphModel, _targetGraphRoot, _graphElementsFactory);
        }
    }
}