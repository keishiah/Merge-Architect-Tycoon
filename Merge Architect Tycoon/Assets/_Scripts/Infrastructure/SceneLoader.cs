using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneLoader : ISceneLoader
{
    public void Load(string name, Action onLoaded = null)
    {
        if (SceneManager.GetActiveScene().name == name)
        {
            onLoaded?.Invoke();
        }
        else
        {
            LoadScene(name, onLoaded).Forget();
        }
    }

    private async UniTask LoadScene(string nextScene, Action onLoaded = null)
    {
        //No need to switch the scene instantly
        // await SceneManager.LoadSceneAsync(nextScene).ToUniTask();
        await SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive).ToUniTask();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextScene));
        await UniTask.DelayFrame(1);
        onLoaded?.Invoke();
    }
}