using UnityEngine;
using Zenject;

public class GameSaver : MonoBehaviour
{
    [Inject] private PlayerProgressService _progress;

    void OnApplicationQuit()
    {
        _progress.SaveAll();
    }
}