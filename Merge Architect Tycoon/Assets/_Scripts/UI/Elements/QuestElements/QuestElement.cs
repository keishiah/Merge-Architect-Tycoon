using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QuestElement : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public Image questiImage;
    public List<QuestRewardRenderer> rewardElements;
    public List<QuestObjectiveRenderer> questPerformanceItems;
    public Button claimButton;

    private QuestsPresenter _questsPresenter;
    private BaseQuestInfo _questInfo;

    [Inject]
    void Construct(QuestsPresenter questsPresenter)
    {
        _questsPresenter = questsPresenter;
    }

    public void RenderQuestHeader(BaseQuestInfo questBase)
    {
        gameObject.SetActive(true);
        questText.text = questBase.Text;
        questiImage.sprite = questBase.Sprite;
    }

    public void RenderQuestRewardsAndItems(BaseQuestInfo questBase)
    {
        HideRewardsAndItems();

        List<Reward> rewards = questBase.GetRewardList();
        for (int rewardsCount = 0; rewardsCount < rewards.Count; rewardsCount++)
        {
            ActivateReward(rewards, rewardsCount);
        }

        ShowobjectivesPerformance(questBase);
    }

    public void MarkQuestAsCompleted(BaseQuestInfo questBase, Action<QuestElement> claimButtonClicked)
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

    private void ShowobjectivesPerformance(BaseQuestInfo questBase)
    {
        List<QuestObjective> objectives = questBase.GetQuestObjectives();
        for (int i = 0; i < objectives.Count; i++)
        {
            questPerformanceItems[i].gameObject.SetActive(true);
            questPerformanceItems[i].RenderItemPerformance(objectives[i].itemText, objectives[i].itemImage, 
                objectives[i].GoalCount, _questInfo.GetQuestObjectives()[i].GoalCount);
        }
    }

    private void ShowCompletedItemPerformance(BaseQuestInfo questBase)
    {
        List<QuestObjective> objectives = questBase.GetQuestObjectives();
        for (int i = 0; i < objectives.Count; i++)
        {
            ShowCompletedItemPerformance(objectives[i], questPerformanceItems[i]);
            questPerformanceItems[i].ItemCompleted();
        }
    }

    private void ShowCompletedItemPerformance(QuestObjective item, QuestObjectiveRenderer questPerformanceItem)
    {
        questPerformanceItem.gameObject.SetActive(true);
        questPerformanceItem.RenderCompletedItemPerformance(item.itemText, item.itemImage, item.GoalCount);
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