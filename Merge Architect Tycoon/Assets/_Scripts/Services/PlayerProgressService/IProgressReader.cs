public interface IProgressReader
{
    void LoadProgress(Progress progress);
}

public class TutorialReader : IProgressReader
{
    public void LoadProgress(Progress progress)
    {
        throw new System.NotImplementedException();
    }
}