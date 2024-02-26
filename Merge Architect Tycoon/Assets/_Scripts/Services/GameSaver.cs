using CodeBase.Data;
using CodeBase.Services;
using UnityEngine;
using Zenject;

namespace CodeBase._Gameplay
{
    public class GameSaver : MonoBehaviour
    {
        [Inject] private Progress _progress;

        void OnApplicationQuit()
        {
            SaveLoadService.Save(SaveKey.Progress, _progress);
        }
    }
}