using _Scripts.Logic;

namespace _Scripts.Services.PlayerProgressService
{
    public interface IProgressReader
    {
        void LoadProgress(Progress progress);
    }
}