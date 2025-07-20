using System;
using System.ComponentModel;
using Codebase.Infrastructure.States;
using Codebase.Infrastructure.States.Factory;
using Codebase.Loading;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Start()
        {
            _gameStateMachine.Enter<BootstrapState>();

        }
    }
}