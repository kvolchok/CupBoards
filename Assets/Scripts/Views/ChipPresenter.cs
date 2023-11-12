using Cysharp.Threading.Tasks;
using Models;
using UnityEngine;

namespace Views
{
    public class ChipPresenter : HighlightableObjectPresenter
    {
        public ChipPresenter(ChipModel chipModel, ChipView chipView, bool isInteractable) : base(chipModel, chipView)
        {
            if (isInteractable)
            {
                ((ChipModel)Model).PositionChanged += OnPositionChanged;
            }
        }
        
        private async UniTask OnPositionChanged(Vector3 position)
        {
            await ((ChipView)View).ChangePosition(position);
        }
        
        public void Highlight(bool isActive)
        {
            ((ChipView)_view).ToggleHighlight(isActive);
        }

        public override void Dispose()
        {
            base.Dispose();
            
            ((ChipModel)Model).PositionChanged -= OnPositionChanged;
        }
    }
}