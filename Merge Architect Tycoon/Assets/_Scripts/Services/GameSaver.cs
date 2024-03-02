using _Scripts.Logic;
using _Scripts.Services.SaveLoadService;
using UnityEngine;
using Zenject;

namespace _Scripts.Services
{
    public class GameSaver : MonoBehaviour
    {
        [Inject] private Progress _progress;

        void OnApplicationQuit()
        {
            SaveLoadService.SaveLoadService.Save(SaveKey.Progress, _progress);
        }
    }
}