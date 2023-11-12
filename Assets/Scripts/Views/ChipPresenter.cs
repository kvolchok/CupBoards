using Cysharp.Threading.Tasks;
using Models;
using UnityEngine;

namespace Views
{
    public class ChipPresenter : HighlightableObjectPresenter
    {
        public ChipPresenter(ChipModel chipModel, ChipView chipView) : base(chipModel, chipView)
        {
            ((ChipModel)_model).PositionChanged += OnPositionChanged;
        }
        
        private async UniTask OnPositionChanged(Vector3 position)
        {
            await ((ChipView)_view).ChangePosition(position);
        }
        
        public void Highlight(bool isActive)
        {
            ((ChipView)_view).ToggleHighlight(isActive);
        }

        public override void Dispose()
        {
            base.Dispose();
            
            ((ChipModel)_model).PositionChanged -= OnPositionChanged;
        }
    }
}