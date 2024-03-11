namespace _Scripts.Logic.Quest
{
    public static class QuestIdentifier
    {
        private static int _questId = 0;

        public static int GetQuestId()
        {
            _questId++;
            return _questId;
        }
    }
}