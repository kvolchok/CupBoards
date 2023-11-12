using System;
using Models;

namespace Views
{
    public abstract class HighlightableObjectPresenter : IDisposable
    {
        public HighlightableObject Model  { get; }
        public HighlightableObjectView View { get; }

        protected HighlightableObjectPresenter(HighlightableObject model, HighlightableObjectView view)
        {
            Model = model;
            View = view;
            
            Model.HighlightChanged += OnHighlightChanged;
        }
        
        private void OnHighlightChanged(bool isActive)
        {
            View.ToggleHighlight(isActive);
        }

        public virtual void Dispose()
        {
            Model.HighlightChanged -= OnHighlightChanged;
        }
    }
}