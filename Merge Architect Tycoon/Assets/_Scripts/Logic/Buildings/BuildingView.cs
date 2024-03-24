using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuildingView : MonoBehaviour
{
    public Image buildingStateImage;
    public Image buildInProcessImage;
    public TextMeshProUGUI timerText;
    private EffectsProvider _effectsProvider;

    [Inject]
    void Construct(EffectsProvider effectsProvider)
    {
        _effectsProvider = effectsProvider;
    }

    public void SetViewInactive()
    {
        buildingStateImage.raycastTarget = false;
        timerText.gameObject.SetActive(false);
        buildingStateImage.gameObject.SetActive(false);
    }

    public void SetViewBuildInProgress()
    {
        buildingStateImage.raycastTarget = false;
        timerText.gameObject.SetActive(true);
    }

    public void SetViewBuildCreated()
    {
        buildingStateImage.raycastTarget = false;
        timerText.gameObject.SetActive(false);
    }

    public void UpdateTimerText(string formattedTime)
    {
        timerText.text = formattedTime;
    }

    public async void ShowBuildSpriteOnCreate(Sprite spriteToShow)
    {
        buildInProcessImage.transform.parent.gameObject.SetActive(false);
        await _effectsProvider.PlaySmokeEffect(buildInProcessImage.transform.position);
        buildingStateImage.transform.localScale = Vector3.zero;
        buildingStateImage.gameObject.SetActive(true);
        buildingStateImage.sprite = spriteToShow;
        await ScaleSpriteWithEffect(buildingStateImage.transform);
    }

    public void ShowBuildingSprite(Sprite spriteToShow)
    {
        buildInProcessImage.transform.parent.gameObject.SetActive(false);
        buildingStateImage.gameObject.SetActive(true);
        buildingStateImage.sprite = spriteToShow;
    }

    private UniTask ScaleSpriteWithEffect(Transform imageTransform)
    {
        return imageTransform.DOScale(1, 1)
            .SetEase(Ease.OutBounce).AsyncWaitForCompletion().AsUniTask();
    }

    public void ShowBuildInProgressSprite(Sprite spriteToShow)
    {
        buildInProcessImage.transform.parent.gameObject.SetActive(true);
        buildInProcessImage.sprite = spriteToShow;
    }
}