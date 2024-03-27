using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DistrictsInfo", menuName = "StaticData/DistrictsInfo", order = 0)]
public class DistrictsInfo : ScriptableObject
{
    public List<DistrictInfo> districts;
}
