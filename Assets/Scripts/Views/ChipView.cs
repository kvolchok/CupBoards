using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Views
{
    public class ChipView : HighlightableView
    {
        [SerializeField] private float _movingSpeed;

        public void Initialize(Vector3 position, Color color)
        {
            transform.localPosition = position;
            Sprite.color = color;
        }

        public override void ToggleHighlight(bool isActive)
        {
            var spriteColor = Sprite.color;
            spriteColor.a = isActive ? Highlighted.a : UnHighlighted.a;

            Sprite.color = spriteColor;
        }

        public async UniTask ChangePosition(Vector3 newPosition)
        {
            var distance = Vector3.Distance(transform.position, newPosition);
            var movingDuration = distance / _movingSpeed;
            
            await transform.DOMove(newPosition, movingDuration).ToUniTask();
        }
    }
}