using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace StateMachine
{
    public abstract class StateMachine : IDisposable
    {
        private readonly Dictionary<Type, IExitableState> _states = new();
        private IExitableState _currentState;

        protected StateMachine(params IExitableState[] states)
        {
            foreach (var state in states)
            {
                RegisterState(state);
            }
        }

        public async UniTask Enter<TState>() where TState : class, IState
        {
            var newState = await ChangeState<TState>();
            await newState.Enter();
        }

        public async UniTask Enter<TState, TStateContext>(TStateContext context)
            where TState : class, IStateWithContext<TStateContext>
        {
            var newState = await ChangeState<TState>();
            await newState.Enter(context);
        }

        private void RegisterState(IExitableState state)
        {
            state.Initialize(this);
            _states[state.GetType()] = state;
        }

        private async UniTask<TState> ChangeState<TState>() where TState : class, IExitableState
        {
            if (_currentState != null)
            {
                await _currentState.Exit();
            }

            var state = GetState<TState>();
            _currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }

        public void Dispose()
        {
            _currentState?.Exit();
        }
    }
}