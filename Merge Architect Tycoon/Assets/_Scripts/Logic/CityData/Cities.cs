using System.Collections.Generic;
using UnityEngine;


public class Cities : MonoBehaviour
{
    public List<District> Districts;

    public int GetBuildingsCountInDistrict(int districtId)
    {
        District district = Districts.Find(x => x.districtId == districtId);
        return district.GetComponentsInChildren<BuildingPlace>().Length;
    }
}