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
        public Cities Cities;

        public Progress()
        {
            Coins = new Coins();
            Diamonds = new Coins();
            Buldings = new Buldings();
            Cities = new Cities();
        }

        public void AddCoins(int coins)
        {
            Debug.Log($"add{coins}");
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

    public class Cities
    {
        private ReactiveDictionary<string, string> _createdCitiesDictionary = new();

        public Cities()
        {
        }

        public void AddCityToDictionary(string cityName)
        {
            _createdCitiesDictionary.Add(cityName, null);
        }

        public void AddDistrictToCity(string cityName, string districtName)
        {
            _createdCitiesDictionary.Add(cityName, districtName);
        }
    }
}