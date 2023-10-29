using Models;
using Views;

namespace Factories
{
    public class GraphPresenterFactory
    {
        private readonly GraphView _startView;
        private readonly GraphView _targetView;

        public GraphPresenterFactory(GraphView startView, GraphView targetView)
        {
            _startView = startView;
            _targetView = targetView;
        }

        public GraphPresenter CreateStartPresenter(GraphModel graphModel)
        {
            return new GraphPresenter(_startView, graphModel);
        }
        public GraphPresenter CreateTargetPresenter(GraphModel graphModel)
        {
            return new GraphPresenter(_targetView, graphModel);
        }
    }
}