using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestsPresenter
{
    public IPlayerProgressService _playerProgressService;
    private QuestPopup _questPopup;
    private QuestsProvider _questsProvider;

    private Dictionary<QuestElement, Quest> _completedQuestsByElements = new();

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
        _questPopup.gameObject.SetActive(true);
        CloseQuestElements();

        foreach (Quest quest in _questsProvider.GetActiveQuestsList)
        {
            ShowActiveQuestInPopup(quest);
        }

        foreach (Quest quest in _questsProvider.GetQuestsWaitingForClaim)
        {
            ShowQuestsWaitingForClaim(quest);
        }
    }

    private void ShowActiveQuestInPopup(Quest quest)
    {
        if (_questPopup.GetInactiveQuestElement(out var questElement))
        {
            questElement.gameObject.SetActive(true);
            questElement.SetQuestText(quest.questName);
            questElement.RenderQuest(quest);
        }
    }

    private void ShowQuestsWaitingForClaim(Quest quest)
    {
        if (_questPopup.GetInactiveQuestElement(out var questElement))
        {
            questElement.gameObject.SetActive(true);
            questElement.SetQuestText(quest.questName);
            questElement.RenderQuest(quest);

            if (_questsProvider.GetQuestsWaitingForClaim.Contains(quest) &&
                !_completedQuestsByElements.ContainsKey(questElement))
            {
                _completedQuestsByElements.Add(questElement, quest);
                questElement.MarkQuestAsCompleted(CompleteQuest);
            }
            else if (_questsProvider.GetQuestsWaitingForClaim.Contains(quest) &&
                     _completedQuestsByElements.ContainsKey(questElement))
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