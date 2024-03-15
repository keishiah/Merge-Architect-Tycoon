using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestsPresenter
{
    private Dictionary<QuestElement, QuestBase> _completedQuestsByElements = new();

    public IPlayerProgressService _playerProgressService;
    private QuestPopup _questPopup;
    private QuestsProvider _questsProvider;


    [Inject]
    void Construct(IPlayerProgressService playerProgressService, QuestPopup questPopup,
        QuestsProvider questsProvider)
    {
        _playerProgressService = playerProgressService;
        _questPopup = questPopup;
        _questsProvider = questsProvider;
    }

    public void OpenQuestPopup()
    {
        CloseQuestElements();

        ShowQuests(_questsProvider.GetActiveQuestsList);
        ShowQuests(_questsProvider.GetQuestsWaitingForClaim);
    }

    private void ShowQuests(IEnumerable<QuestBase> quests)
    {
        foreach (QuestBase quest in quests)
        {
            ShowQuest(quest);
        }
    }

    private void ShowQuest(QuestBase questBase)
    {
        if (!_questPopup.GetInactiveQuestElement(out var questElement))
            return;

        questElement.RenderQuestHeader(questBase);

        if (!_questsProvider.GetQuestsWaitingForClaim.Contains(questBase))
        {
            questElement.RenderQuestRewardsAndItems(questBase);
            return;
        }

        if (!_completedQuestsByElements.ContainsKey(questElement))
        {
            _completedQuestsByElements.Add(questElement, questBase);
            questElement.RenderQuestRewardsAndItems(questBase);
            questElement.MarkQuestAsCompleted(questBase, CompleteQuest);
        }
        else
        {
            questElement.MarkQuestAsCompleted(questBase, CompleteQuest);
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