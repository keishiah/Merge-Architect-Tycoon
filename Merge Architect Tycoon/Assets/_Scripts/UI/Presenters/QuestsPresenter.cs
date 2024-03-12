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

        foreach (Quest quest in _questsProvider.GetActiveQuestsList)
        {
            ShowQuest(quest);
        }

        foreach (Quest quest in _questsProvider.GetQuestsWaitingForClaim)
        {
            ShowQuest(quest);
        }
    }

    private void ShowQuest(Quest quest)
    {
        if (!_questPopup.GetInactiveQuestElement(out var questElement))
            return;
        questElement.gameObject.SetActive(true);
        questElement.SetQuestText(quest.questName);
        questElement.RenderQuest(quest);

        if (!_questsProvider.GetQuestsWaitingForClaim.Contains(quest))
            return;

        if (!_completedQuestsByElements.ContainsKey(questElement))
        {
            _completedQuestsByElements.Add(questElement, quest);
            questElement.MarkQuestAsCompleted(CompleteQuest);
        }
        else
        {
            questElement.MarkQuestAsCompleted(CompleteQuest);
        }
    }


    private void CompleteQuest(QuestElement questElement)
    {
        _questsProvider.ClaimQuestReward(_completedQuestsByElements[questElement]);
        _completedQuestsByElements.Remove(questElement);
    }

    private void CloseQuestElements()
    {
        foreach (QuestElement questElement in _questPopup.questElements)
        {
            questElement.gameObject.SetActive(false);
        }
    }
}