using Cysharp.Threading.Tasks;
using DG.Tweening;
using Models;
using UnityEngine;

namespace Views
{
    public class ChipView : HighlightableObjectView
    {
        [SerializeField]
        private float _movingDuration;

        public void Initialize(Vector3 position, ChipModel chip)
        {
            transform.localPosition = position;
            Sprite.color = chip.Color; 
        }

        public override void ToggleHighlight(bool isActive)
        {
            var spriteColor = Sprite.color;
            spriteColor.a = isActive ? Highlighted.a : UnHighlighted.a;

            Sprite.color = spriteColor;
        }

        public async UniTask ChangePosition(Vector3 position)
        {
            await transform.DOMove(position, _movingDuration).ToUniTask();
        }
    }
}