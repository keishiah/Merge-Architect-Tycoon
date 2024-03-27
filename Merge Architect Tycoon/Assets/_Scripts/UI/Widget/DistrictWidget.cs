public class DistrictWidget : WidgetView
{
    private DistrictPopup _districtUi;

    private void Awake()
    {
        _districtUi = GetComponentInChildren<DistrictPopup>();
    }
    public override void OnOpen()
    {
        base.OnOpen();
        _districtUi.OpenDistrict();
    }
}
