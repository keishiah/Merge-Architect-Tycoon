using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TutorialReader : IProgressReader
{
    public void LoadProgress(Progress progress)
    {
        if (PlayerPrefs.GetString(SaveKey.NeedSkipTutorial.ToString()) == "true")
            return;

        if (progress.Tutorial.IsComplite)
            return;

        LoadTutorial();
    }

    private async void LoadTutorial()
    {
        AsyncOperationHandle opHandle = Addressables.LoadAssetAsync<GameObject>(AssetName.TutorialPrefab);
        await opHandle.Task;

        if (opHandle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Failed to load a sprite resource named \"{AssetName.TutorialPrefab}\"!");
            return;
        }

        GameObject.Instantiate((GameObject)opHandle.Result, GameObject.Find("TutorialRoot").transform);
    }
}