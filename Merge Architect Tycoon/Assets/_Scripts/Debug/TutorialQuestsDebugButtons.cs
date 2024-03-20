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
        private FirebaseLogger _firebaseLogger;

        [Inject]
        void Construct(QuestsProvider questsProvider, QuestGiver questGiver,FirebaseLogger firebaseLogger)
        {
            _questsProvider = questsProvider;
            _questGiver = questGiver;
            _firebaseLogger = firebaseLogger;
        }

        private void Start()
        {
            addQuestButton.onClick.AddListener(AddQuest);
            completeQuestButton.onClick.AddListener(CompleteQuest);
            clearSavesButton.onClick.AddListener(ClearSaves);
        }

        private void ClearSaves()
        {
            PlayerPrefs.DeleteAll();
            Application.Quit();
        }

        private void AddQuest()
        {
            _questGiver.CheckAllQuestsForActivation();
            _firebaseLogger.LogEvent("tutorial_get_quest_quests_popup");
        }

        private void CompleteQuest()
        {
            _questsProvider.CheckAllQuestsCompleted();
        }
        
    }
}