using System;
using Events;
using UniTaskPubSub;

namespace Views
{
    public class GameOverPresenter : IDisposable
    {
        private readonly GameOverScreen _gameOverOverScreen;
        private readonly IAsyncPublisher _publisher;

        public GameOverPresenter(GameOverScreen gameOverOverScreen, IAsyncPublisher publisher)
        {
            _gameOverOverScreen = gameOverOverScreen;
            _publisher = publisher;

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

            await _publisher.PublishAsync(new ButtonClickedEvent(false));
        }

        private async void OnNextLevelButtonClicked()
        {
            Hide();

            await _publisher.PublishAsync(new ButtonClickedEvent(true));
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