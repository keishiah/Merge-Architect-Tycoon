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
        public Button clearSavesButton;

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
            clearSavesButton.onClick.AddListener(ClearSaves);
        }

        private void ClearSaves()
        {
            SaveLoadService.ClearAll();
            Application.Quit();
        }

        private void AddQuest()
        {
            _questGiver.CheckAllQuestsForActivation();
        }

        private void CompleteQuest()
        {
            _questsProvider.CheckAllQuestsCompleted();
        }
        
    }
}