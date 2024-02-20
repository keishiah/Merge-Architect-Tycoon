using CodeBase.Services.SaveLoadService;
using UnityEngine;
using Zenject;

namespace CodeBase._Gameplay
{
    public class GameSaver : MonoBehaviour
    {
        [Inject] private ISaveLoadService _saveLoadService;

        void OnApplicationQuit()
        {
            _saveLoadService.SaveProgress();
        }
    }
}