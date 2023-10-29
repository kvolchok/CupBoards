using System;
using Cysharp.Threading.Tasks;
using Events;
using UniTaskPubSub;

namespace Views
{
    public class FinishScreenPresenter : IDisposable
    {
        private readonly FinishGameScreen _finishGameScreen;
        private readonly AsyncMessageBus _messageBus;
        
        private IDisposable _subscription;

        public FinishScreenPresenter(FinishGameScreen finishGameScreen, AsyncMessageBus messageBus)
        {
            _finishGameScreen = finishGameScreen;
            _messageBus = messageBus;

            _subscription = _messageBus.Subscribe<EnterFinishGameStateEvent>(OnEnterFinishGameStateEvent);
            _finishGameScreen.RestartLevel.onClick.AddListener(OnRestartLevelButtonClicked);
            _finishGameScreen.NextLevel.onClick.AddListener(OnNextLevelButtonClicked);
        }

        private UniTask OnEnterFinishGameStateEvent(EnterFinishGameStateEvent eventData)
        {
            _finishGameScreen.gameObject.SetActive(true);

            return UniTask.CompletedTask;
        }

        private async void OnRestartLevelButtonClicked()
        {
            _finishGameScreen.gameObject.SetActive(false);
            
            await _messageBus.PublishAsync(new RestartLevelButtonClickedEvent());
        }
        
        private async void OnNextLevelButtonClicked()
        {
            _finishGameScreen.gameObject.SetActive(false);
            
            await _messageBus.PublishAsync(new NextLevelButtonClickedEvent());
        }
        
        public void Dispose()
        {
            _subscription?.Dispose();
            _finishGameScreen.RestartLevel.onClick.RemoveAllListeners();
            _finishGameScreen.NextLevel.onClick.RemoveAllListeners();
        }
    }
}