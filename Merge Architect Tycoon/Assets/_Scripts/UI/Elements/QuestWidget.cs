using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class QuestWidget : MonoBehaviour
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
            widgetElements[0].text = $"Строй {buildingName},тебе надо {coins} копеек и {resourceCount} {resource}";
        }

        public void RenderWidgetValues(string value1, string value2, string value3)
        {
            widgetElements[1].text = value2;
            widgetElements[1].text = value3;
        }
    }
}