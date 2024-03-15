public class BuildingCreationSaver
{
    private readonly IPlayerProgressService _playerProgressService;

    public BuildingCreationSaver(IPlayerProgressService playerProgressService)
    {
        _playerProgressService = playerProgressService;
    }

    public void SaveCreationProgress(string buildingName, int creationRest)
    {
    }
}