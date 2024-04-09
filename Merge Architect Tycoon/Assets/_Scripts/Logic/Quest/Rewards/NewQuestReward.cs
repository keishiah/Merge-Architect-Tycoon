using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest",
    menuName = "StaticData/Quests/Rewards/NewQuestReward")]
public class NewQuestReward : Reward
{
    [SerializeField] private QuestInfo quest;

    public override void GiveReward(PlayerProgressService progress)
    {
        progress.ActivateQuest(quest);
    }
}