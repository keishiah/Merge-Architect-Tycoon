using CodeBase.Data;

namespace CodeBase.Services
{
    public class PlayerProgressService : IPlayerProgressService
    {
        private Progress _progress;
        public Progress Progress
        {
            get
            { 
                if (_progress != null)
                    return _progress;
                return new();
            }
            set
            {
                _progress = value;
            }
        }
    }
}