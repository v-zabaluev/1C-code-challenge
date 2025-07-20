using System;
using System.Collections.Generic;
using Codebase.Infrastructure.Factory;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.States.Factory;
using Codebase.Loading;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.States
{
    public class GameStateMachine : IInitializable
    {
        private readonly StateFactory _factory;
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        [Inject]
        public GameStateMachine(StateFactory factory)
        {
            _factory = factory;

            _states = new Dictionary<Type, IExitableState>();
        }

        public void Initialize()
        {
            _states = new Dictionary<Type, IExitableState> 
            {
                [typeof(BootstrapState)] = _factory.CreateState<BootstrapState>(),
                [typeof(LoadLevelState)] =
                    _factory.CreateState<LoadLevelState>(),
                [typeof(GameLoopState)] = _factory.CreateState<GameLoopState>(),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();

            state.Enter();
            
            Debug.Log($"Entered {typeof(TState).Name}");
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            TState state = ChangeState<TState>();

            state.Enter(payload);
            Debug.Log($"Entered {typeof(TState).Name}");
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();

            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}