using System.Collections.Generic;
using Zenject;


public class QuestsProvider
{
    private PlayerProgress _playerProgress;
    private PlayerProgressService _playerProgressService;

    [Inject]
    void Construct(PlayerProgress playerProgress, PlayerProgressService playerProgressService)
    {
        _playerProgress = playerProgress;
        _playerProgressService = playerProgressService;
    }

    public void ActivateQuest(QuestData questBase)
    {
        questBase.Subscribe(_playerProgress);
        questBase.OnComplete += CheckQuestsCompleted;
    }

    public void CheckAllQuestsCompleted()
    {
        List<QuestData> completedQuests = _playerProgress.Quests.ActiveQuests.FindAll(quest => quest.QuestInfo.IsCompleted(quest));
        foreach (QuestData completedQuest in completedQuests)
        {
            CheckQuestsCompleted(completedQuest);
        }
    }

    public void CheckQuestsCompleted(QuestData completedQuest)
    {
        _playerProgress.Quests.ActiveQuests.Remove(completedQuest);
        _playerProgress.Quests.QuestsWaitingForClaim.Add(completedQuest.QuestInfo.ID);
    }

    public void ClaimQuestReward(BaseQuestInfo questBase)
    {
        questBase.GiveReward(_playerProgressService);
        _playerProgress.Quests.CompletedQuests.Add(questBase.ID);
        _playerProgress.Quests.QuestsWaitingForClaim.Remove(questBase.ID);
    }

    public void AddActiveQuest(QuestData quest)
    {
        _playerProgress.Quests.ActiveQuests.Add(quest);
        SaveLoadService.Save(SaveKey.Quests, _playerProgress.Quests);
    }

    public void AddQuestWaitingForClaim(QuestData quest)
    {
        string ID = quest.QuestInfo.ID;
        _playerProgress.Quests.QuestsWaitingForClaim.Add(ID);
        if (_playerProgress.Quests.ActiveQuests.Contains(quest))
            _playerProgress.Quests.ActiveQuests.Remove(quest);
        SaveLoadService.Save(SaveKey.Quests, _playerProgress.Quests);
    }

    public void AddCompletedQuest(QuestData quest)
    {
        string ID = quest.QuestInfo.ID;
        _playerProgress.Quests.CompletedQuests.Add(ID);
        if (_playerProgress.Quests.QuestsWaitingForClaim.Contains(ID))
            _playerProgress.Quests.QuestsWaitingForClaim.Remove(ID);
        SaveLoadService.Save(SaveKey.Quests, _playerProgress.Quests);
    }
}