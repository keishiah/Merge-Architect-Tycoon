using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuildingRenderer : MonoBehaviour
{
    public Image buildingStateImage;
    public TextMeshProUGUI timerText;
    public Button createBuildingInMomentButton;
    public Button buildingButton;

    private EffectsPresenter _effectsPresenter;
    private AudioPlayer _audio;

    private string _buildingName;

    [Inject]
    void Construct(EffectsPresenter effectsPresenter, AudioPlayer audio)
    {
        _effectsPresenter = effectsPresenter;
        _audio = audio;
    }

    public void SetBuildingName(string buildingName)
    {
        _buildingName = buildingName;
    }

    public virtual void SetViewInactive()
    {
        timerText.gameObject.SetActive(false);
        buildingStateImage.gameObject.SetActive(false);
    }

    public void SetViewBuildInProgress()
    {
        _effectsPresenter.PlaySmokeEffect(buildingButton.transform.position, _buildingName);
        _audio.PlayUiSound(UiSoundTypes.Building);
        createBuildingInMomentButton.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
    }

    public virtual void SetViewBuildCreated()
    {
        buildingButton.gameObject.SetActive(true);
        timerText.gameObject.SetActive(false);
    }

    public void UpdateTimerText(string formattedTime)
    {
        timerText.text = formattedTime;
    }

    public void ShowBuildSpriteOnCreate(Sprite spriteToShow)
    {
        buildingStateImage.transform.localScale = Vector3.zero;
        buildingStateImage.gameObject.SetActive(true);
        buildingStateImage.sprite = spriteToShow;
        ScaleSpriteWithEffect(buildingStateImage.transform);
        createBuildingInMomentButton.gameObject.SetActive(false);
    }

    public void ShowBuildingSprite(Sprite spriteToShow)
    {
        buildingStateImage.gameObject.SetActive(true);
        buildingStateImage.sprite = spriteToShow;
    }

    private void ScaleSpriteWithEffect(Transform imageTransform)
    {
        _effectsPresenter.StopSmokeEffect(buildingButton.transform.position, _buildingName);
        imageTransform.DOScale(1, 1)
            .SetEase(Ease.OutBounce);
    }
}