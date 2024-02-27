using CodeBase.Services;

namespace CodeBase.Data
{
    public class Progress
    {
        public Coins Coins;
        public Coins Diamands;

        public Progress()
        {
            Coins = new Coins();
            Diamands = new Coins();
        }

        public void AddCoins(int coins)
        {
            Coins.Add(coins);
            SaveLoadService.Save(SaveKey.Progress, this);
        }
        public void AddDiamonds(int coins)
        {
            Diamands.Add(coins);
            SaveLoadService.Save(SaveKey.Progress, this);
        }
    }
}