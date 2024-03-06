using System;
using System.Collections.Generic;
using _Scripts.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestElement : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public List<RewardElement> rewardElements;

    public Button claimButton;

    public List<QuestPerformanceItem> questPerformanceItems;


    public void SetQuestText(string text)
    {
        questText.text = text;
    }

    public void SetQuestRewards(List<Reward> rewards, Quest quest)
    {
        HideRewards();
        HideQuestPerformanceItems();
        for (int rewardsCount = 0; rewardsCount < rewards.Count; rewardsCount++)
        {
            rewardElements[rewardsCount].gameObject.SetActive(true);
            rewardElements[rewardsCount].RenderReward(rewards[rewardsCount].rewardAmount.ToString(),
                rewards[rewardsCount].rewardSprite);

            if (quest.questType == QuestType.BuildingQuest)
            {
                questPerformanceItems[rewardsCount].gameObject.SetActive(true);
                questPerformanceItems[rewardsCount].RenderBuildingQuestPerformance(quest.buildingName,
                    quest.buildingImage);
            }
            else
            {
                questPerformanceItems[rewardsCount].gameObject.SetActive(true);

                foreach (var item in quest.itemsToMerge)
                {
                    questPerformanceItems[rewardsCount].RenderCreateItemQuestPerformance(item, quest.itemsCount);
                }
            }
        }
    }

    private void HideQuestPerformanceItems()
    {
        foreach (var questPerformanceItem in questPerformanceItems)
        {
            questPerformanceItem.gameObject.SetActive(false);
        }
    }

    private void HideRewards()
    {
        foreach (var rewardElement in rewardElements)
        {
            rewardElement.gameObject.SetActive(false);
        }
    }
}