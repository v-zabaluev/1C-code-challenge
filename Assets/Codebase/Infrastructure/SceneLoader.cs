using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Codebase.Infrastructure
{
    public class SceneLoader
    {
        public async UniTask LoadAsync(string sceneName, Action onSceneLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onSceneLoaded?.Invoke();
                return;
            }

            var loadingOperation = SceneManager.LoadSceneAsync(sceneName);
            await loadingOperation.ToUniTask();

            onSceneLoaded?.Invoke();
        }
    }
}