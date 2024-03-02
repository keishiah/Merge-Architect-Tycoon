using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Logic
{
    [Serializable]
    public class Buldings
    {
        [SerializeField] private List<string> _createdBuildings = new();
        public List<string> CreatedBuildings => _createdBuildings;

        public void AddCreatedBuildingToList(string buildingName)
        {
            _createdBuildings.Add(buildingName);
        }
    }
}