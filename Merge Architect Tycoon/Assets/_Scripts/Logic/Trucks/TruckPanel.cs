using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TruckPanel : MonoBehaviour
{
    public Button MenuTruckButton;
    public Sprite[] RebuyTruckSprites;
    public Image RebuyTruckImage;
    public Button UpdateTruckButton;
    public TextMeshProUGUI UpdateTruckButtonText;
    public Button BoostTruckButton;
    public TextMeshProUGUI[] BoostTexts;
    public Slider[] BoostSliders;
    public Button[] ResourceButtons;
    public Button BuyTruckButton;

    private int _boostLimit;
    [SerializeField]
    private RectTransform _resourceCursor;

    public void BoostInit(int limit)
    {
        _boostLimit = limit;
        foreach (Slider slider in BoostSliders)
        {
            slider.maxValue = _boostLimit;
        }
    }

    public void RenderBoost(int count)
    {
        foreach (Slider slider in BoostSliders)
        {
            slider.value = count;
        }
        foreach (TextMeshProUGUI text in BoostTexts)
        {
            text.text = $"{count}/{_boostLimit}";
        }
    }

    public void UpdateButtonRefresh(string text, bool interactable = true)
    {
        UpdateTruckButtonText.text = text;
        UpdateTruckButton.interactable = interactable;
    }

    public void BoostButtonRefresh()
    {
        throw new NotImplementedException();
    }

    public void ResourcesRefresh(int level)
    {
        for(int i = 0; i < ResourceButtons.Length; i++)
        {
            ResourceButtons[i].gameObject.SetActive(i<level);
        }
    }

    public void ResourceChoise(int i)
    {
        _resourceCursor.SetParent(ResourceButtons[i].transform, worldPositionStays: false);
    }

    public void SubscribeResources(Action<int> setResource)
    {
        for (int i = 0; i < ResourceButtons.Length; i++)
        {
            int index = i;//new instance
            ResourceButtons[i].onClick.AddListener(() => setResource(index));
        }
    }
}
