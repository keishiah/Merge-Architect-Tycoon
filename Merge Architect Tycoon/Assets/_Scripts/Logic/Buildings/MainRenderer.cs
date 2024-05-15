using UnityEngine.UI;
using Zenject;

public class MainRenderer : BuildingRenderer
{
    public Button showMainPopupButton;
    public MainPopup mainPopup;

    [Inject] private BuildingProvider _buildingProvider;

    private void Start()
    {
        showMainPopupButton.onClick.AddListener(OpenMainPopup);
    }

    private void OpenMainPopup()
    {
        mainPopup.gameObject.SetActive(true);
    }

    public override void SetViewInactive()
    {
        base.SetViewInactive();
        showMainPopupButton.gameObject.SetActive(false);
        // mainPopup.gameObject.SetActive(false);
    }

    public override void SetViewBuildCreated()
    {
        base.SetViewBuildCreated();
        showMainPopupButton.gameObject.SetActive(true);
        if (!_buildingProvider.GetNextMainPart(out var mainInfo))
            showMainPopupButton.gameObject.SetActive(false);
    }
}