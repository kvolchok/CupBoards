using System;
using Cysharp.Threading.Tasks;
using Events;
using Extensions;
using Models;
using UniTaskPubSub;
using Object = UnityEngine.Object;

namespace Views
{
    public abstract class HighlightablePresenter : IDisposable
    {
        public HighlightableView View { get; }
        
        protected readonly IHighlightable Model;
        protected readonly AsyncMessageBus MessageBus;

        private readonly CompositeDisposable _subscriptions;

        protected HighlightablePresenter(IHighlightable model, HighlightableView view, AsyncMessageBus messageBus,
            bool isInteractable)
        {
            View = view;
            Model = model;
            MessageBus = messageBus;

            if (!isInteractable)
            {
                return;
            }

            _subscriptions = new CompositeDisposable
            {
                MessageBus.Subscribe<TurnOnHighlightEvent>(OnTurnOnHighlight),
                MessageBus.Subscribe<TurnOffHighlightsEvent>(OnTurnOffHighlights)
            };
        }

        public void ClearView()
        {
            Object.Destroy(View.gameObject);
        }

        private UniTask OnTurnOnHighlight(TurnOnHighlightEvent eventData)
        {
            var models = eventData.Models;

            if (models.Contains(Model))
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