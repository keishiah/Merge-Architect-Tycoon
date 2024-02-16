using CodeBase.Data;

namespace CodeBase.Services.SaveLoadService
{
    public interface ISaveLoadService
    {
        void SaveProgress();
        Progress LoadProgress();
    }
}