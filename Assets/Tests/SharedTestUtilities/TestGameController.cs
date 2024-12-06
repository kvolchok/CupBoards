using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using GameStates;
using Models;
using Services;
using UniTaskPubSub;
using UnityEngine.Pool;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Tests.SharedTestUtilities
{
    public class TestGameController : IStartable, IDisposable
    {
        private readonly GameStateMachine _stateMachine;
        private readonly GraphService _graphService;
        private readonly PathFinderService _pathFinderService;
        private readonly IAsyncPublisher _publisher;
        private NodeModel _randomNodeModel;

        public TestGameController(GameStateMachine stateMachine, GraphService graphService,
            PathFinderService pathFinderService, IAsyncPublisher publisher)
        {
            _stateMachine = stateMachine;
            _graphService = graphService;
            _pathFinderService = pathFinderService;
            _publisher = publisher;
        }

        public async void Start()
        {
            await _stateMachine.Enter<LevelLoadState>();
        }

        public void SelectStartNode()
        {
            ListPool<NodeModel>.Get(out var nodeModels);
            nodeModels = _graphService.GetStartGraph().Nodes.ToList();
            _randomNodeModel = GetRandomNodeModel(nodeModels);
            ListPool<NodeModel>.Release(nodeModels);
            _publisher.Publish(new NodeSelectedEvent(_randomNodeModel));
        }

        public void SelectTargetNode()
        {
            ListPool<NodeModel>.Get(out var reachableNodes);
            reachableNodes = _pathFinderService.FindReachableNodes(_randomNodeModel).ToList();
            if (reachableNodes.Count == 0)
            {
                _publisher.Publish(new NodeSelectedEvent(_randomNodeModel));
                return;
            }

            _randomNodeModel = GetRandomNodeModel(reachableNodes);
            ListPool<NodeModel>.Release(reachableNodes);
            _publisher.Publish(new NodeSelectedEvent(_randomNodeModel));
        }

        private NodeModel GetRandomNodeModel(List<NodeModel> nodes)
        {
            var randomNodeIndex = Random.Range(0, nodes.Count);
            return nodes[randomNodeIndex];
        }

        public void Dispose()
        {
            _stateMachine?.Dispose();
        }
    }
}