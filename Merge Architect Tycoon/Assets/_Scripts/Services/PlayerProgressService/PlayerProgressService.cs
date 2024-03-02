using _Scripts.Logic;
using Zenject;

namespace _Scripts.Services.PlayerProgressService
{
    public class PlayerProgressService : IPlayerProgressService
    {
        [Inject]
        private Progress _progress;
        public Progress Progress
        {
            get
            { 
                return _progress;
                // if (_progress != null)
                //     return _progress;
                //
                // Debug.LogError("There is null Progress!");
                // return new();
            }
            set
            {
                _progress = value;
            }
        }
    }
}