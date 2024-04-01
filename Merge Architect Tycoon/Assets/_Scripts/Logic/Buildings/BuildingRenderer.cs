﻿using Cysharp.Threading.Tasks;
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

    private EffectsPresenter _effectsPresenter;

    [Inject]
    void Construct(EffectsPresenter effectsPresenter)
    {
        _effectsPresenter = effectsPresenter;
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
        _effectsPresenter.PlaySmokeEffect(buildingStateImage.transform.position);
        createBuildingInMomentButton.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
    }

    public void SetViewBuildCreated()
    {
        buildingStateImage.raycastTarget = false;
        Destroy(timerText.gameObject);
    }

    public void UpdateTimerText(string formattedTime)
    {
        timerText.text = formattedTime;
    }

    public void ShowBuildSpriteOnCreate(Sprite spriteToShow)
    {
        buildingStateImage.transform.localScale = Vector3.zero;
        _effectsPresenter.StopSmokeEffect();
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
        imageTransform.DOScale(1, 1)
            .SetEase(Ease.OutBounce).AsyncWaitForCompletion().AsUniTask();
    }
}