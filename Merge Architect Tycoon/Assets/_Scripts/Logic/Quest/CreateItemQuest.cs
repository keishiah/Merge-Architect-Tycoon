using System;
using System.Collections.Generic;
using _Scripts.Logic.Merge.Items;

namespace _Scripts.Logic.Quest
{
    [Serializable]
    public class CreateItemQuest:QuestBase
    {
        public List<MergeItem> itemsToCreate;
    }
}