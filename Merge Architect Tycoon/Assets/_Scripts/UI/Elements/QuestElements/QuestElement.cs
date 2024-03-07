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

    public void InitializeElement()
    {
        HideRewards();
        HideQuestPerformanceItems();
    }

    public void SetQuestText(string text)
    {
        questText.text = text;
    }

    public void ActivateBuildingQuest(List<Reward> rewards, Quest quest)
    {
        // gameObject.GetComponent<TMP_Dropdown>().AddOptions(rewards.Select(x => x.rewardAmount.ToString()).ToList());

        for (int rewardsCount = 0; rewardsCount < rewards.Count; rewardsCount++)
        {
            ActivateReward(rewards, rewardsCount);
            ActivatePerformanceItem(quest, rewardsCount);
        }
    }

    public void SetQuestRewards(List<Reward> rewards, Quest quest)
    {
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

    private void ActivateReward(List<Reward> rewards, int rewardsCount)
    {
        rewardElements[rewardsCount].gameObject.SetActive(true);
        rewardElements[rewardsCount].RenderReward(rewards[rewardsCount].rewardAmount.ToString(),
            rewards[rewardsCount].rewardSprite);
    }

    private void ActivatePerformanceItem(Quest quest, int rewardsCount)
    {
        int itemToActivatePositionInList = questPerformanceItems.Count - rewardsCount - 1;
        questPerformanceItems[itemToActivatePositionInList].gameObject.SetActive(true);
        questPerformanceItems[itemToActivatePositionInList].RenderBuildingQuestPerformance(quest.buildingName,
            quest.buildingImage);
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

    public void SetQuestAsCompleted()
    {
        claimButton.gameObject.SetActive(true);
    }
}