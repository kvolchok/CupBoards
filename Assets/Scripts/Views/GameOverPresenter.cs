using System;
using Events;
using UniTaskPubSub;

namespace Views
{
    public class GameOverPresenter : IDisposable
    {
        private readonly GameOverScreen _gameOverOverScreen;
        private readonly AsyncMessageBus _messageBus;

        public GameOverPresenter(GameOverScreen gameOverOverScreen, AsyncMessageBus messageBus)
        {
            _gameOverOverScreen = gameOverOverScreen;
            _messageBus = messageBus;

            _gameOverOverScreen.RestartLevel.onClick.AddListener(OnRestartLevelButtonClicked);
            _gameOverOverScreen.NextLevel.onClick.AddListener(OnNextLevelButtonClicked);
        }
        
        public void Show()
        {
            _gameOverOverScreen.gameObject.SetActive(true);
        }

        private async void OnRestartLevelButtonClicked()
        {
            _gameOverOverScreen.gameObject.SetActive(false);
            
            await _messageBus.PublishAsync(new RestartLevelButtonClickedEvent());
        }
        
        private async void OnNextLevelButtonClicked()
        {
            _gameOverOverScreen.gameObject.SetActive(false);
            
            await _messageBus.PublishAsync(new NextLevelButtonClickedEvent());
        }
        
        public void Dispose()
        {
            _gameOverOverScreen.RestartLevel.onClick.RemoveAllListeners();
            _gameOverOverScreen.NextLevel.onClick.RemoveAllListeners();
        }
    }
}