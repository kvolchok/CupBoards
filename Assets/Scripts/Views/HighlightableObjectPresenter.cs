using System;
using Models;

namespace Views
{
    public abstract class HighlightableObjectPresenter : IDisposable
    {
        protected readonly HighlightableObject _model;
        protected readonly HighlightableObjectView _view;

        protected HighlightableObjectPresenter(HighlightableObject model, HighlightableObjectView view)
        {
            _model = model;
            _view = view;
            
            _model.HighlightChanged += OnHighlightChanged;
        }
        
        private void OnHighlightChanged(bool isActive)
        {
            _view.ToggleHighlight(isActive);
        }

        public virtual void Dispose()
        {
            _model.HighlightChanged -= OnHighlightChanged;
        }
    }
}