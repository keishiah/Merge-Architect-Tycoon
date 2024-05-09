using Zenject;

public class BuildingQuestPresenter : IInitializableOnSceneLoaded
{
    [Inject] BuildingQuestPanel questPanel;

    public void OnSceneLoaded()
    {
        questPanel.OnSceneLoaded();
    }
}