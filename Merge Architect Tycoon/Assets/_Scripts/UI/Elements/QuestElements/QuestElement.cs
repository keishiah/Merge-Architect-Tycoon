using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QuestElement : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public List<RewardElement> rewardElements;
    public List<QuestPerformanceItem> questPerformanceItems;
    public Button claimButton;
    private QuestsPresenter _questsPresenter;

    [Inject]
    void Construct(QuestsPresenter questsPresenter)
    {
        _questsPresenter = questsPresenter;
    }

    public void SetQuestText(string text) => questText.text = text;

    public void RenderQuest(Quest quest)
    {
        HideRewardsAndItems();

        List<Reward> rewards = quest.GetRewardList();
        for (int rewardsCount = 0; rewardsCount < rewards.Count; rewardsCount++)
        {
            ActivateReward(rewards, rewardsCount);
        }

        ShowItemsPerformance(quest);
    }

    public void MarkQuestAsCompleted(Action<QuestElement> claimButtonClicked)
    {
        claimButton.onClick.RemoveAllListeners();
        claimButton.gameObject.SetActive(true);
        questPerformanceItems[0].ItemCompleted();

        claimButton.onClick.AddListener(() => claimButtonClicked(this));
    }

    private void ShowItemsPerformance(Quest quest)
    {
        for (int i = 0; i < quest.GetQuestItemsToCreate().Count; i++)
        {
            ShowBuildingItemPerformance(quest.GetQuestItemsToCreate()[i], questPerformanceItems[i]);
        }
    }


    private void ShowBuildingItemPerformance(QuestItem item, QuestPerformanceItem questPerformanceItem)
    {
        questPerformanceItem.gameObject.SetActive(true);
        int currentItemCount = item.GetCurrentItemCount(_questsPresenter._playerProgressService);
        questPerformanceItem.RenderItemPerformance(item.itemText, item.itemImage, currentItemCount, item.itemCount);
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

        claimButton.gameObject.SetActive(false);
    }
}