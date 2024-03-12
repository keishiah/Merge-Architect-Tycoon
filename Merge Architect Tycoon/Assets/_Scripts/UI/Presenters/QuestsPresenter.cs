using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestsPresenter
{
    private Dictionary<QuestElement, Quest> _completedQuestsByElements = new();

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

    private void ShowQuests(IEnumerable<Quest> quests)
    {
        foreach (Quest quest in quests)
        {
            ShowQuest(quest);
        }
    }

    private void ShowQuest(Quest quest)
    {
        if (!_questPopup.GetInactiveQuestElement(out var questElement))
            return;
        questElement.RenderQuestHeader(quest);

        if (!_questsProvider.GetQuestsWaitingForClaim.Contains(quest))
            return;

        if (!_completedQuestsByElements.ContainsKey(questElement))
        {
            _completedQuestsByElements.Add(questElement, quest);
            questElement.MarkQuestAsCompleted(quest, CompleteQuest);
        }
        else
        {
            questElement.MarkQuestAsCompleted(quest, CompleteQuest);
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