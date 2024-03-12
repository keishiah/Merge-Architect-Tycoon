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

    public void RenderQuestHeader(Quest quest)
    {
        gameObject.SetActive(true);
        questText.text = quest.questText;
        questiImage.sprite = quest.questSprite;
    }

    public void RenderQuestRewardsAndItems(Quest quest)
    {
        HideRewardsAndItems();

        List<Reward> rewards = quest.GetRewardList();
        for (int rewardsCount = 0; rewardsCount < rewards.Count; rewardsCount++)
        {
            ActivateReward(rewards, rewardsCount);
        }

        ShowItemsPerformance(quest);
    }

    public void MarkQuestAsCompleted(Quest quest, Action<QuestElement> claimButtonClicked)
    {
        claimButton.onClick.RemoveAllListeners();
        claimButton.gameObject.SetActive(true);
        ShowCompletedItemPerformance(quest);

        claimButton.onClick.AddListener(() => claimButtonClicked(this));
    }

    private void ActivateReward(List<Reward> rewards, int rewardsCount)
    {
        rewardElements[rewardsCount].gameObject.SetActive(true);
        rewardElements[rewardsCount].RenderReward(rewards[rewardsCount].rewardAmount.ToString(),
            rewards[rewardsCount].rewardSprite);
    }

    private void ShowItemsPerformance(Quest quest)
    {
        for (int i = 0; i < quest.GetQuestItemsToCreate().Count; i++)
        {
            ShowItemPerformance(quest.GetQuestItemsToCreate()[i], questPerformanceItems[i]);
        }
    }

    private void ShowItemPerformance(QuestItem item, QuestPerformanceItem questPerformanceItem)
    {
        questPerformanceItem.gameObject.SetActive(true);
        int currentItemCount = item.GetCurrentItemCount(_questsPresenter._playerProgressService);
        questPerformanceItem.RenderItemPerformance(item.itemText, item.itemImage, currentItemCount, item.itemCount);
    }

    private void ShowCompletedItemPerformance(Quest quest)
    {
        for (int i = 0; i < quest.GetQuestItemsToCreate().Count; i++)
        {
            ShowCompletedItemPerformance(quest.GetQuestItemsToCreate()[i], questPerformanceItems[i]);
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