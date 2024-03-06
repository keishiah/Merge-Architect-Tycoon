using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class QuestPopup : MonoBehaviour
{
    public List<QuestElement> questElements;

    private QuestsPresenter _questsPresenter;

    [Inject]
    public void Construct(QuestsPresenter questsPresenter)
    {
        _questsPresenter = questsPresenter;
    }

    public void InitializePopup()
    {
        foreach (var questElement in questElements)
        {
            questElement.gameObject.SetActive(false);
        }
    }

    public bool GetInactiveQuestElement(out QuestElement questElement)
    {
        questElement = questElements.Find(x => !x.gameObject.activeSelf);
        return questElement != null;
    }
}