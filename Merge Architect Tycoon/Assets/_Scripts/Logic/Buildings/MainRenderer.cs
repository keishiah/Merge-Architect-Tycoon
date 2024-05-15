using UnityEngine.UI;

public class MainRenderer : BuildingRenderer
{
    public Button showMainPopupButton;
    public MainPopup mainPopup;

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
    }
}