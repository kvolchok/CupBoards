using Cysharp.Threading.Tasks;
using Models;
using UniTaskPubSub;
using UnityEngine;

namespace Views
{
    public class ChipPresenter : HighlightableObjectPresenter
    {
        public ChipPresenter(ChipModel chipModel, ChipView chipView, AsyncMessageBus messageBus,
            bool isInteractable) : base(chipModel, chipView, messageBus, isInteractable)
        {
            if (!isInteractable)
            {
                return;
            }
            
            ((ChipModel)_model).PositionChanged += OnPositionChanged;
        }

        private async UniTask OnPositionChanged(Vector3 position)
        {
            await ((ChipView)View).ChangePosition(position);
        }

        public override void Dispose()
        {
            base.Dispose();
            
            ((ChipModel)_model).PositionChanged -= OnPositionChanged;
        }
    }
}