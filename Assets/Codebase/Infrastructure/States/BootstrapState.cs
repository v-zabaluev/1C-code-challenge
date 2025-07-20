using Codebase.Infrastructure.Factory;
using Codebase.Infrastructure.Services;
using UnityEngine;

namespace Codebase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string INITIAL_SCENE_NAME = "Preload";
        private const string GAMEPLAY_SCENE_NAME = "MainScene";

        private GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;

            RegisterServices();
        }

        public async void Enter()
        {
            await _sceneLoader.LoadAsync(INITIAL_SCENE_NAME, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(GAMEPLAY_SCENE_NAME);
        }

        private void RegisterServices()
        {
            Debug.Log("All services registered");
        }

        public void Exit()
        {
        }
    }
}