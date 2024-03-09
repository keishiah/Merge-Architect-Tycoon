using Zenject;

public class TutorialQuestTracker
{
    private QuestGiver _questGiver;

    [Inject]
    void Construct(QuestGiver questGiver)
    {
        _questGiver = questGiver;
    }

    public void CheckQuestCompleted(string questName)
    {
        if (_questGiver.GetCurrentQuest(GiveQuestCondition.Tutorial, out var quest))
            if (quest.questName == questName)
                _questGiver.QuestCompleted(GiveQuestCondition.Tutorial);
    }
}