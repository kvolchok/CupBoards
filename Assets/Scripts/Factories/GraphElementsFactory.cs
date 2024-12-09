using Models;
using Settings;
using UniTaskPubSub;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Views;

namespace Factories
{
    public class GraphElementsFactory
    {
        private readonly NodeView _nodePrefab;
        private readonly ChipView _chipPrefab;
        private readonly EdgeView _edgePrefab;

        private readonly IObjectResolver _container;
        private readonly AsyncMessageBus _messageBus;

        public GraphElementsFactory(GraphViewPrefabs graphViewPrefabs, IObjectResolver container,
            AsyncMessageBus messageBus)
        {
            _nodePrefab = graphViewPrefabs.NodePrefab;
            _chipPrefab = graphViewPrefabs.ChipPrefab;
            _edgePrefab = graphViewPrefabs.EdgePrefab;

            _container = container;
            _messageBus = messageBus;
        }

        public NodePresenter CreateNodePresenter(Transform parent, NodeModel nodeModel, bool isInteractable)
        {
            var nodeView = _container.Instantiate(_nodePrefab, parent);
            nodeView.Initialize(nodeModel.Position);

            return new NodePresenter(nodeModel, nodeView, _messageBus, isInteractable);
        }

        public ChipPresenter CreateChipPresenter(Transform parent, Vector3 chipPosition, ChipModel chipModel,
            bool isInteractable)
        {
            var chipView = _container.Instantiate(_chipPrefab, parent);
            chipView.Initialize(chipPosition, chipModel.Color);

            return new ChipPresenter(chipModel, chipView, _messageBus, isInteractable);
        }

        public EdgeView CreateEdge(Transform parent)
        {
            return _container.Instantiate(_edgePrefab, parent);
        }
    }
}