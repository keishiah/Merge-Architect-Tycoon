using System;
using System.Collections.Generic;

namespace _Scripts.Logic.Quest
{
    [Serializable]
    public class CreateItemQuest:QuestBase
    {
        public List<MergeItem> itemsToCreate;
    }
}