using System;
using Cysharp.Threading.Tasks;
using Events;
using Models;
using UniTaskPubSub;

namespace Views
{
    public class ChipPresenter : HighlightablePresenter
    {
        private readonly ChipView _chipView;
        private readonly IDisposable _subscription;

        public ChipPresenter(ChipModel chipModel, ChipView chipView, AsyncMessageBus messageBus, bool isInteractable)
            : base(chipModel, chipView, messageBus, isInteractable)
        {
            _chipView = chipView;
            
            if (!isInteractable)
            {
                return;
            }

            _subscription = _messageBus.Subscribe<MoveChipEvent>(OnMoveChip);
        }

        private async UniTask OnMoveChip(MoveChipEvent eventData)
        {
            var currentChip = eventData.CurrentChip;

            if (currentChip != _model)
            {
                return;
            }
            
            var route = eventData.Route;
            route.Pop();
            
            while (route.Count > 0)
            {
                var currentNode = route.Pop();
                var nextPosition = currentNode.Position;
                
                await _chipView.ChangePosition(nextPosition);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _subscription?.Dispose();
        }
    }
}