using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class QuestPopup : MonoBehaviour
{
    public List<TextMeshProUGUI> widgetElements;

    private QuestsPresenter _questsPresenter;

    [Inject]
    public void Construct(QuestsPresenter questsPresenter)
    {
        _questsPresenter = questsPresenter;
    }

    public void Start()
    {
        _questsPresenter.SetQuestWidget(this);
    }

    public void RenderElement1(string buildingName, int coins, int resourceCount, MergeItem resource)
    {
        widgetElements[0].text = $"Строй {buildingName}";
        widgetElements[1].text = $"тебе надо {coins} копеек";
        widgetElements[2].text = $"тебе надо {resourceCount} {resource}";
    }
}