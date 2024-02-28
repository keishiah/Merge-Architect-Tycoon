using System.Collections.Generic;
using CodeBase.Services;
using UniRx;
using UnityEngine;

namespace CodeBase.Data
{
    public class Progress
    {
        public Coins Coins;
        public Coins Diamands;
        public Buldings Buldings;
        public Cities Cities;

        public Progress()
        {
            Coins = new Coins();
            Diamands = new Coins();
            Buldings = new Buldings();
            Cities = new Cities();
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

        public void AddBuilding(string buildingName)
        {
            Buldings.AddCreatedBuildingToList(buildingName);
            SaveLoadService.Save(SaveKey.Progress, this);
        }
    }

    public class Cities
    {
        private ReactiveDictionary<string, string> _createdCitiesDictionary = new();

        public void AddCityToDictionary(string cityName)
        {
            _createdCitiesDictionary.Add(cityName, null);
        }

        public void AddDistrictToCity(string cityName,string districtName)
        {
            _createdCitiesDictionary.Add(cityName, districtName);
        }
    }
}