using Zenject;

public class PlayerProgressService : IPlayerProgressService
{
    [Inject] private Progress _progress;

    public Progress Progress
    {
        get { return _progress; }
        set { _progress = value; }
    }

    public Quests Quests { get; set; }
}