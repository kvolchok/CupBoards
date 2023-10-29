using Cysharp.Threading.Tasks;

namespace StateMachine
{
    public interface IStateWithContext<TStateContext> : IExitableState
    {
        UniTask Enter(TStateContext context);
    }
}