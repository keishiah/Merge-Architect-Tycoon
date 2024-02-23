using CodeBase.Services;

namespace CodeBase.Data
{
    public class Progress
    {
        public Coins Coins;

        public Progress()
        {
            Coins = new Coins();
        }

        public void AddCoins(int coins)
        {
            Coins.AddCoins(coins);
            SaveLoadService.Save(SaveKey.Progress, this);
        }
    }
}