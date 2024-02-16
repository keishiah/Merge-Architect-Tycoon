using CodeBase.Data;

namespace CodeBase.Services.PlayerProgressService
{
    public interface IPlayerProgressService
    {
        Progress Progress { get; set; }
    }
}