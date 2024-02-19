using Cysharp.Threading.Tasks;
using System;
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
            if(SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                return;
            }

            LoadScene(name, onLoaded).Forget();
        }

        private async UniTask LoadScene(string nextScene, Action onLoaded = null)
        {
            //No need to switch the scene instantly
            await SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive).ToUniTask();
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene));

            await UniTask.DelayFrame(1);
            onLoaded?.Invoke();
        }
    }
}