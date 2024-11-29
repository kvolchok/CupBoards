using Cysharp.Threading.Tasks;
using Events;
using Factories;
using Services;
using UniTaskPubSub;
using UnityEngine;

namespace Tests.SharedTestUtilities
{
    public class TestGraphService : GraphService
    {
        private readonly IAsyncPublisher _publisher;

        public TestGraphService(GraphComparer graphComparer, GraphFactory graphFactory, IAsyncPublisher publisher)
            : base(graphFactory, graphComparer)
        {
            _publisher = publisher;
        }

        public async UniTask SelectRandomNodeModel()
        {
            var randomNodeIndex = Random.Range(0, StartGraph.Nodes.Count);
            var randomNodeModel = StartGraph.Nodes[randomNodeIndex];

            await _publisher.PublishAsync(new NodeSelectedEvent(randomNodeModel));
        }
    }
}