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
            Hide();

            await _messageBus.PublishAsync(new ButtonClickedEvent(false));
        }

        private async void OnNextLevelButtonClicked()
        {
            Hide();

            await _messageBus.PublishAsync(new ButtonClickedEvent(true));
        }

        private void Hide()
        {
            _gameOverOverScreen.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            _gameOverOverScreen.RestartLevel.onClick.RemoveAllListeners();
            _gameOverOverScreen.NextLevel.onClick.RemoveAllListeners();
        }
    }
}