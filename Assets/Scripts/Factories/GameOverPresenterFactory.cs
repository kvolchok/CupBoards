using UniTaskPubSub;
using Views;

namespace Factories
{
    public class GameOverPresenterFactory
    {
        private readonly GameOverScreen _gameOverScreen;
        private readonly AsyncMessageBus _messageBus;

        public GameOverPresenterFactory(GameOverScreen gameOverScreen, AsyncMessageBus messageBus)
        {
            _gameOverScreen = gameOverScreen;
            _messageBus = messageBus;
        }

        public GameOverPresenter Create()
        {
            return new GameOverPresenter(_gameOverScreen, _messageBus);
        }
    }
}