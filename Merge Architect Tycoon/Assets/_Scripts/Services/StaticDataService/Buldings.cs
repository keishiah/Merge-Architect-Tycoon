using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data
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