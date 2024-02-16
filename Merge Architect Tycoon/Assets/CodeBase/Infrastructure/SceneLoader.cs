using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public interface ISceneLoader
    {
        void Load(string name, Action onLoaded = null);
    }

    public class SceneLoader : ISceneLoader
    {
        public void Load(string name, Action onLoaded = null)
        {
            LoadScene(name, onLoaded).Forget();
        }

        private async UniTask LoadScene(string nextScene, Action onLoaded = null)
        {
            // if (SceneManager.GetActiveScene().name == nextScene)
            // {
            //     onLoaded?.Invoke();
            //     yield break;
            // }

            await SceneManager.LoadSceneAsync(nextScene).ToUniTask();

            await UniTask.DelayFrame(1);
            onLoaded?.Invoke();
        }
    }
}