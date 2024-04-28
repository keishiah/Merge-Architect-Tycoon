using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CitiesMapPopup : MonoBehaviour
{
    public List<DistrictPopup> Districts;

    public DistrictPopup GetNextDistrict(int districtId) => Districts.Find(x => x.DistrictId == districtId + 1);
}