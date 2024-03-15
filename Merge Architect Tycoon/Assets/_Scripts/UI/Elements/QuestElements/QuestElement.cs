using System;
using System.Collections.Generic;
using _Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QuestElement : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public Image questiImage;
    public List<RewardElement> rewardElements;
    public List<QuestPerformanceItem> questPerformanceItems;
    public Button claimButton;

    private QuestsPresenter _questsPresenter;

    [Inject]
    void Construct(QuestsPresenter questsPresenter)
    {
        _questsPresenter = questsPresenter;
    }

    public void RenderQuestHeader(QuestBase questBase)
    {
        gameObject.SetActive(true);
        questText.text = questBase.questText;
        questiImage.sprite = questBase.questSprite;
    }

    public void RenderQuestRewardsAndItems(QuestBase questBase)
    {
        HideRewardsAndItems();

        List<Reward> rewards = questBase.GetRewardList();
        for (int rewardsCount = 0; rewardsCount < rewards.Count; rewardsCount++)
        {
            ActivateReward(rewards, rewardsCount);
        }

        ShowItemsPerformance(questBase);
    }

    public void MarkQuestAsCompleted(QuestBase questBase, Action<QuestElement> claimButtonClicked)
    {
        claimButton.onClick.RemoveAllListeners();
        claimButton.gameObject.SetActive(true);
        ShowCompletedItemPerformance(questBase);

        claimButton.onClick.AddListener(() => claimButtonClicked(this));
    }

    private void ActivateReward(List<Reward> rewards, int rewardsCount)
    {
        rewardElements[rewardsCount].gameObject.SetActive(true);
        rewardElements[rewardsCount].RenderReward(rewards[rewardsCount].rewardAmount.ToString(),
            rewards[rewardsCount].rewardSprite);
    }

    private void ShowItemsPerformance(QuestBase questBase)
    {
        for (int i = 0; i < questBase.GetQuestItemsToCreate().Count; i++)
        {
            ShowItemPerformance(questBase.GetQuestItemsToCreate()[i], questPerformanceItems[i]);
        }
    }

    private void ShowItemPerformance(QuestItem item, QuestPerformanceItem questPerformanceItem)
    {
        questPerformanceItem.gameObject.SetActive(true);
        int currentItemCount = item.GetCurrentItemCount(_questsPresenter._playerProgressService);
        questPerformanceItem.RenderItemPerformance(item.itemText, item.itemImage, currentItemCount, item.itemCount);
    }

    private void ShowCompletedItemPerformance(QuestBase questBase)
    {
        for (int i = 0; i < questBase.GetQuestItemsToCreate().Count; i++)
        {
            ShowCompletedItemPerformance(questBase.GetQuestItemsToCreate()[i], questPerformanceItems[i]);
            questPerformanceItems[i].ItemCompleted();
        }
    }

    private void ShowCompletedItemPerformance(QuestItem item, QuestPerformanceItem questPerformanceItem)
    {
        questPerformanceItem.gameObject.SetActive(true);
        questPerformanceItem.RenderCompletedItemPerformance(item.itemText, item.itemImage, item.itemCount);
    }


    private void HideRewardsAndItems()
    {
        foreach (var rewardElement in rewardElements)
        {
            rewardElement.gameObject.SetActive(false);
        }

        foreach (var questPerformanceItem in questPerformanceItems)
        {
            questPerformanceItem.gameObject.SetActive(false);
            questPerformanceItem.HideCompletedMark();
        }

        claimButton.gameObject.SetActive(false);
    }
}