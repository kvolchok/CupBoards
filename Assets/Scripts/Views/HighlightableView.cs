using UnityEngine;

namespace Views
{
    public abstract class HighlightableView : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer Sprite { get; protected set; }
        [field: SerializeField] public Color Highlighted { get; private set; }
        [field: SerializeField] public Color UnHighlighted { get; private set; }

        public virtual void ToggleHighlight(bool isActive)
        {
            Sprite.color = isActive ? Highlighted : UnHighlighted;
        }
    }
}