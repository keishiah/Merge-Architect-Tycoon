using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Debug
{
    public class TutorialQuestsDebugButtons : MonoBehaviour
    {
        public Button addQuestButton;
        public Button completeQuestButton;

        private QuestsProvider _questsProvider;
        private QuestGiver _questGiver;

        [Inject]
        void Construct(QuestsProvider questsProvider, QuestGiver questGiver)
        {
            _questsProvider = questsProvider;
            _questGiver = questGiver;
        }

        private void Start()
        {
            addQuestButton.onClick.AddListener(AddQuest);
            completeQuestButton.onClick.AddListener(CompleteQuest);
        }

        private void AddQuest()
        {
            _questGiver.ActivateTutorialQuest("CreateFirstBuilding");
            _questGiver.ActivateTutorialQuest("FirstDistrictEarn");
        }

        private void CompleteQuest()
        {
            _questsProvider.CheckCompletionOfTutorialQuest("CreateFirstBuilding");
            _questsProvider.CheckCompletionOfTutorialQuest("FirstDistrictEarn");
        }
    }
}