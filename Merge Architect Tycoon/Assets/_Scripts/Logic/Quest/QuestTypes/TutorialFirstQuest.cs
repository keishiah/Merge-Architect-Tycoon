using UnityEngine;

[CreateAssetMenu(fileName = "TutorialFirstQuest",
    menuName = "StaticData/Quests/Tutorial/FirstQuest")]
public class TutorialFirstQuest : BaseQuestInfo
{
    public override bool IsReadyToStart(PlayerProgress progress)
    {
        return true;
    }
}