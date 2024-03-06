using TMPro;
using UnityEngine;
using Zenject;

public class DiamandsOnShopUi : MonoBehaviour
{
    public TextMeshProUGUI diamandCountText;

    private UiPresenter _uiPresenter;

    [Inject]
    void Construct(UiPresenter uiPresenter)
    {
        _uiPresenter = uiPresenter;
    }

    private void Awake()
    {
        var progress = _uiPresenter.PlayerProgressService.Progress;

        RenderDiamandCount(progress.Diamonds.CurrentCoinsCount);
        _uiPresenter.SubscribeDiamondsCountChanged(RenderDiamandCount);
    }

    public void RenderDiamandCount(int newValue)
    {
        diamandCountText.text = newValue.ToString();
    }
}