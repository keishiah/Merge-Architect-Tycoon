using _Scripts.Services.SaveLoadService;
using UniRx;
using UnityEngine;

namespace _Scripts.Logic
{
    public class Progress
    {
        public Coins Coins;
        public Coins Diamonds;
        public Buldings Buldings;

        public Progress()
        {
            Coins = new Coins();
            Diamonds = new Coins();
            Buldings = new Buldings();
        }

        public void AddCoins(int coins)
        {
            Coins.Add(coins);
            SaveLoadService.Save(SaveKey.Progress, this);
        }

        public void AddDiamonds(int coins)
        {
            Diamonds.Add(coins);
            SaveLoadService.Save(SaveKey.Progress, this);
        }

        public void AddBuilding(string buildingName)
        {
            Buldings.AddCreatedBuildingToList(buildingName);
            SaveLoadService.Save(SaveKey.Progress, this);
        }
    }
}