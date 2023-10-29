using System;

namespace Models
{
    public abstract class HighlightableObject
    {
        public event Action<bool> HighlightChanged;
        public bool Highlighted { get; private set; }
        
        public void ToggleHighlight(bool isActive)
        {
            Highlighted = isActive;
            HighlightChanged?.Invoke(isActive);
        }
    }
}