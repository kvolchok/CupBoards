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

        private NodeModel _startRandomNodeModel;
        private HashSet<NodeModel> _reachableNodes;

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

        public void SelectStartChipWithPossibleMoves()
        {
            var nodeModels = _graphService.GetStartGraph().Nodes;
            
            _startRandomNodeModel = GetRandomNodeModel(nodeModels);
            _reachableNodes = _pathFinderService.FindReachableNodes(_startRandomNodeModel);

            while (_startRandomNodeModel.Chip == null || _reachableNodes.Count == 0)
            {
                _startRandomNodeModel = GetRandomNodeModel(nodeModels);
                _reachableNodes = _pathFinderService.FindReachableNodes(_startRandomNodeModel);
            }

            _publisher.Publish(new NodeSelectedEvent(_startRandomNodeModel));
        }

        public void SelectTargetNode()
        {
            ListPool<NodeModel>.Get(out var reachableNodesList);
            reachableNodesList.AddRange(_reachableNodes);

            var randomNodeModel = GetRandomNodeModel(reachableNodesList);
            while (Equals(_startRandomNodeModel, randomNodeModel))
            {
                randomNodeModel = GetRandomNodeModel(reachableNodesList);
            }

            ListPool<NodeModel>.Release(reachableNodesList);
            _publisher.Publish(new NodeSelectedEvent(randomNodeModel));
        }

        private NodeModel GetRandomNodeModel(IReadOnlyList<NodeModel> nodes)
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