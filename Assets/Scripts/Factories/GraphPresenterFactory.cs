using Models;
using UnityEngine;
using Views;

namespace Factories
{
    public class GraphPresenterFactory
    {
        private readonly GraphElementsFactory _graphElementsFactory;

        public GraphPresenterFactory(GraphElementsFactory graphElementsFactory)
        {
            _graphElementsFactory = graphElementsFactory;
        }

        public GraphPresenter CreateGraphPresenter(GraphModel graphModel, GameObject graphRoot)
        {
            return new GraphPresenter(graphModel, graphRoot, _graphElementsFactory);
        }
    }
}