using UnityEngine;
using Zenject;

public class GameSaver : MonoBehaviour
{
    [Inject] private Progress _progress;

    void OnApplicationQuit()
    {
        SaveLoadService.Save(SaveKey.Progress, _progress);
    }
}