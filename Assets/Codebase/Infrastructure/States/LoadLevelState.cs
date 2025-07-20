using Codebase.Infrastructure.Factory;
using Codebase.Loading;
using UnityEngine;

namespace Codebase.Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        private LoadingCurtain _loadingCurtain;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public async void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            await _sceneLoader.LoadAsync(sceneName, OnLoaded);
            Debug.Log($"Loading level {sceneName}");
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            InitGameWorld();
            _stateMachine.Enter<GameLoopState>();
        }
        

        private void InitGameWorld()
        {
            //Read data from SO
            //Create spawners
            //Set their params

            //Set player health
            //Reset score
        }
    }
}