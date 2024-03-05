using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DistrictsData", menuName = "StaticData/DistrictsData", order = 0)]
public class DistrictsData : ScriptableObject
{
    public List<DistrictInfo> districts;
}
