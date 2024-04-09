using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestPanel : MonoBehaviour
{
    public List<QuestRenderer> ElementsPool;
    [SerializeField] private RectTransform questRendererParent;
    [SerializeField] private QuestRenderer questRendererPrefab;

    [Inject] private QuestsPresenter questsPresenter;

    public void CloseQuestElements()
    {
        foreach (QuestRenderer questElement in ElementsPool)
        {
            questElement.gameObject.SetActive(false);
        }
    }

    public void SetQuestElement(QuestData quest)
    {
        QuestRenderer questElement = ElementsPool.Find(x => x.CurrentData == quest);
        if (questElement == null)
        {
            questElement = ElementsPool.Find(x => !x.gameObject.activeSelf);
            if(questElement == null)
            {
                questElement = Instantiate(questRendererPrefab, questRendererParent);
                questElement.Panel = this;
                ElementsPool.Add(questElement);
            }
        }
        
        questElement.gameObject.SetActive(true);
        questElement.Render(quest);
    }

    public void ShowQuests(IEnumerable<QuestData> quests)
    {
        foreach (QuestData quest in quests)
        {
            SetQuestElement(quest);
        }
    }

    public void Refresh()
    {
        questsPresenter.OpenQuestPopup();
    }
}