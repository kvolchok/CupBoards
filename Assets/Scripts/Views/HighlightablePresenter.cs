using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Events;
using Extensions;
using Models;
using UniTaskPubSub;

namespace Views
{
    public abstract class HighlightablePresenter : IDisposable
    {
        public HighlightableView View { get; }
        
        protected readonly IHighlightable _model;
        protected readonly AsyncMessageBus _messageBus;
        
        private readonly CompositeDisposable _subscriptions;

        protected HighlightablePresenter(
            IHighlightable model,
            HighlightableView view,
            AsyncMessageBus messageBus,
            bool isInteractable)
        {
            _model = model;
            View = view;
            _messageBus = messageBus;
            
            if (!isInteractable)
            {
                return;
            }

            _subscriptions = new CompositeDisposable
            {
                _messageBus.Subscribe<TurnOnHighlightEvent>(OnTurnOnHighlight),
                _messageBus.Subscribe<TurnOffHighlightsEvent>(OnTurnOffHighlights)
            };
        }
        
        private UniTask OnTurnOnHighlight(TurnOnHighlightEvent eventData)
        {
            var models = eventData.Models;

            if (models.Contains(_model))
            {
                View.ToggleHighlight(true);
            }

            return UniTask.CompletedTask;
        }
        
        private UniTask OnTurnOffHighlights(TurnOffHighlightsEvent eventData)
        {
            View.ToggleHighlight(false);
            
            return UniTask.CompletedTask;
        }

        public virtual void Dispose()
        {
            _subscriptions?.Dispose();
        }
    }
}