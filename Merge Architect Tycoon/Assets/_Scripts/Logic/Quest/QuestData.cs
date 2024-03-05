using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Logic.Quest
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "StaticData/QuestData", order = 0)]
    public class QuestData : ScriptableObject
    {
        public List<Quest> Quests;
    }

    [Serializable]
    public class Quest
    {
        public int questId;
        public QuestType questType;
        public QuestBase Data;

        private void OnValidate()
        {
            switch (questType)
            {
                case QuestType.CreateItemQuest:
                    Data = new CreateItemQuest();
                    break;
                case QuestType.BuildingQuest:
                    Data = new BuildingQuest();
                    break;
            }
        }
    }

    public enum QuestType
    {
        CreateItemQuest,
        BuildingQuest
    }

    [Serializable]
    public abstract class QuestBase
    {
        public string buildingName;
    }
}