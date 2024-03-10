using TMPro;
using UnityEngine;
using Zenject;

public class CoinsCounterUi : UiViewBase, IInitializableOnSceneLoaded
{
    public TextMeshProUGUI moneyCountText;
    public TextMeshProUGUI diamandCountText;

    private UiPresenter _uiPresenter;
    private QuestGiver _questGiver;

    [Inject]
    void Construct(UiPresenter uiPresenter, QuestGiver questGiver)
    {
        _uiPresenter = uiPresenter;
        _questGiver = questGiver;
    }

    private void Awake()
    {
        InitUiElement(_uiPresenter);
    }

    public override void InitUiElement(UiPresenter uiPresenter)
    {
        _uiPresenter = uiPresenter;
        _uiPresenter.AddUiElementToElementsList(this);
    }

    public void OnSceneLoaded()
    {
        var progress = _uiPresenter.PlayerProgressService.Progress;

        RenderMoneyCount(progress.Coins.CurrentCoinsCount);
        // RenderDiamandCount(progress.Diamonds.CurrentCoinsCount);

        _uiPresenter.SubscribeMoneyCountChanged(RenderMoneyCount);
        _uiPresenter.SubscribeDiamondsCountChanged(RenderDiamandCount);
    }

    public void AddCoins()
    {
        _questGiver.ActivateTutorialQuests("CreateFirstBuilding");
        _questGiver.ActivateTutorialQuests("FirstDistrictEarn");
// #if UNITY_EDITOR
        if (Application.version.StartsWith("d"))
        {
            _uiPresenter.PlayerProgressService.Progress.AddCoins(100500);
            _uiPresenter.PlayerProgressService.Progress.AddDiamonds(100500);
        }
        // else
        // {
// #endif
        //Shop.TryToBuy();
// #if UNITY_EDITOR
//             }
// #endif
    }

    public void RenderMoneyCount(int newValue)
    {
        moneyCountText.text = newValue.ToString();
    }

    public void RenderDiamandCount(int newValue)
    {
        diamandCountText.text = newValue.ToString();
    }
}