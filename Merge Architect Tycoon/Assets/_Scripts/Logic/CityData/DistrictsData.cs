using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Logic.CityData
{
    [CreateAssetMenu(fileName = "DistrictsData", menuName = "StaticData/DistrictsData", order = 0)]
    public class DistrictsData : ScriptableObject
    {
        public List<DistrictInfo> districts;
    }
    [Serializable]
    public class DistrictInfo
    {
        public int districtId;
        public int currencyCount;
        public int timeToEarn;
    }
}