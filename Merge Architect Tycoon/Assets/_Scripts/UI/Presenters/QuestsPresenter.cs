using System.Collections.Generic;
using Zenject;

public class QuestsPresenter
{
    private Dictionary<QuestElement, BaseQuestInfo> _completedQuestsByElements = new();

    private List<BaseQuestInfo> _questsInfo;
    private QuestPopup _questPopup;
    private QuestsProvider _questsProvider;
    public PlayerProgress _playerProgress { get; set; }


    [Inject]
    void Construct(PlayerProgress playerProgress, QuestPopup questPopup,
        QuestsProvider questsProvider, StaticDataService staticDataService)
    {
        _playerProgress = playerProgress;
        _questPopup = questPopup;
        _questsProvider = questsProvider;
        _questsInfo = staticDataService.Quests;
    }

    public void OpenQuestPopup()
    {
        CloseQuestElements();

        ShowQuests(_playerProgress.Quests.QuestsWaitingForClaim);
        ShowQuests(_playerProgress.Quests.ActiveQuests);
    }
    private void ShowQuests(IEnumerable<string> quests)
    {
        foreach (string quest in quests)
        {
            BaseQuestInfo findingQuest = _questsInfo.Find(questBase => questBase.ID == quest);
            ShowWaitingForClaimQuest(findingQuest);
        }
    }
    private void ShowQuests(IEnumerable<QuestData> quests)
    {
        foreach (QuestData quest in quests)
        {
            ShowActiveQuest(quest);
        }
    }
    private void ShowWaitingForClaimQuest(BaseQuestInfo questInfo)
    {
        if (!_questPopup.GetInactiveQuestElement(out QuestElement questElement))
            return;

        questElement.RenderQuestHeader(questInfo);

        questElement.RenderQuestRewardsAndItems(questInfo);
    }
    private void ShowActiveQuest(QuestData questData)
    {
        if (!_questPopup.GetInactiveQuestElement(out QuestElement questElement))
            return;

        questElement.RenderQuestHeader(questData.QuestInfo);

        if (!_completedQuestsByElements.ContainsKey(questElement))
        {
            _completedQuestsByElements.Add(questElement, questData.QuestInfo);
            questElement.RenderQuestRewardsAndItems(questData.QuestInfo);
            questElement.MarkQuestAsCompleted(questData.QuestInfo, CompleteQuest);
        }
        else
        {
            questElement.MarkQuestAsCompleted(questData.QuestInfo, CompleteQuest);
        }
    }

    private void CompleteQuest(QuestElement questElement)
    {
        _questsProvider.ClaimQuestReward(_completedQuestsByElements[questElement]);
        _completedQuestsByElements.Remove(questElement);
        questElement.gameObject.SetActive(false);
    }

    private void CloseQuestElements()
    {
        foreach (QuestElement questElement in _questPopup.questElements)
        {
            questElement.gameObject.SetActive(false);
        }
    }
}