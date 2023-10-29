using UniTaskPubSub;
using Views;

namespace Factories
{
    public class FinishScreenPresenterFactory
    {
        private readonly FinishGameScreen _finishGameScreen;
        private readonly AsyncMessageBus _messageBus;

        public FinishScreenPresenterFactory(FinishGameScreen finishGameScreen, AsyncMessageBus messageBus)
        {
            _finishGameScreen = finishGameScreen;
            _messageBus = messageBus;
        }

        public FinishScreenPresenter CreateFinishScreenPresenter()
        {
            return new FinishScreenPresenter(_finishGameScreen, _messageBus);
        }
    }
}