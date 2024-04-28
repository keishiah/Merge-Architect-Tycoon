using System.Collections.Generic;
using UnityEngine;


public class Cities : MonoBehaviour
{
    public List<District> Districts;

    public int GetBuildingsCountInDistrict(int districtId)
    {
        District district = Districts.Find(x => x.districtId == districtId);
        int countBuildingPlace = 0;

        BuildingPlace[] allBuildingPlaces = district.GetComponentsInChildren<BuildingPlace>();

        foreach (BuildingPlace building in allBuildingPlaces)
        {
            if (building is not Main)
            {
                countBuildingPlace++;
            }
        }

        return countBuildingPlace;
    }
}