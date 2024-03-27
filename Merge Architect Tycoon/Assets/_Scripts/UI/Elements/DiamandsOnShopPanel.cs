using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class DiamandsOnShopPanel : MonoBehaviour
{
    public TextMeshProUGUI diamandCountText;

    private PlayerProgress _playerProgress;

    [Inject]
    void Construct(PlayerProgress playerProgress)
    {
        _playerProgress = playerProgress;
    }

    private void Awake()
    {
        _playerProgress.Riches.Diamonds.Subscribe(i => RenderDiamandCount(i));
        RenderDiamandCount(_playerProgress.Riches.Diamonds.Value);
    }

    public void RenderDiamandCount(int newValue)
    {
        diamandCountText.text = newValue.ToString();
    }
}