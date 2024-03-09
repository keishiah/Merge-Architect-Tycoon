using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestElement : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public List<RewardElement> rewardElements;

    public List<QuestPerformanceItem> questPerformanceItems;

    public Button claimButton;

    public void SetQuestText(string text)
    {
        questText.text = text;
    }

    public void RenderQuest(Quest quest)
    {
        claimButton.onClick.RemoveAllListeners();
        HideRewardsAndItems();

        List<Reward> rewards = quest.GetRewardList();
        for (int rewardsCount = 0; rewardsCount < rewards.Count; rewardsCount++)
        {
            ActivateReward(rewards, rewardsCount);
        }

        ActivatePerformanceItems(quest);
    }

    public void MarkQuestAsCompleted(Action<QuestElement> claimButtonClicked)
    {
        claimButton.gameObject.SetActive(true);
        questPerformanceItems[0].ItemCompleted();

        claimButton.onClick.AddListener(() => claimButtonClicked(this));
    }

    private void ActivatePerformanceItems(Quest quest)
    {
        for (int i = 0; i < quest.GetQuestItemsToCreate().Count; i++)
        {
            RenderBuildingPerformanceItem(quest.GetQuestItemsToCreate()[i], questPerformanceItems[i]);
        }
    }


    private void RenderBuildingPerformanceItem(QuestItem item, QuestPerformanceItem questPerformanceItem)
    {
        questPerformanceItem.gameObject.SetActive(true);
        questPerformanceItem.RenderItemPerformance(item.itemName, item.itemImage, item.itemCount);
    }

    private void ActivateReward(List<Reward> rewards, int rewardsCount)
    {
        rewardElements[rewardsCount].gameObject.SetActive(true);
        rewardElements[rewardsCount].RenderReward(rewards[rewardsCount].rewardAmount.ToString(),
            rewards[rewardsCount].rewardSprite);
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
        }
    }
}