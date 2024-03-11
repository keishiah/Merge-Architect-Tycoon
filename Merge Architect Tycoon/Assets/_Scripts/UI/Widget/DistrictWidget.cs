using UnityEngine;

public class DistrictWidget : Widget
{
    private DistrictUi _districtUi;

    private void Awake()
    {
        _districtUi = GetComponentInChildren<DistrictUi>();
    }
    public override void OnOpen()
    {
        base.OnOpen();
        _districtUi.OpenDistrict();
    }
}
