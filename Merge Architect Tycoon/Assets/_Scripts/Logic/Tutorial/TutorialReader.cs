using UnityEngine;

public class TutorialReader
{
    public void LoadProgress(PlayerProgress progress)
    {
        if (PlayerPrefs.GetString(SaveKey.NeedSkipTutorial.ToString()) == "true")
            return;

        if (progress.Tutorial.IsComplite)
            return;

        LoadTutorial(progress);
    }

    private void LoadTutorial(PlayerProgress progress)
    {
        //AsyncOperationHandle opHandle = Addressables.LoadAssetAsync<GameObject>(AssetName.TutorialPrefab);
        //await opHandle.Task;

        //if (opHandle.Status != AsyncOperationStatus.Succeeded)
        //{
        //    Debug.LogError($"Failed to load a prefab resource named \"{AssetName.TutorialPrefab}\"!");
        //    return;
        //}

        //GameObject Tutorial = GameObject.Instantiate((GameObject)opHandle.Result, GameObject.Find("TutorialRoot").transform);
        GameObject Tutorial = GameObject.Find("TutorialPopup");
        Tutorial.GetComponent<TutorialHandler>().StartTutorialFromIndex(progress.Tutorial.StepIndex);
    }
}