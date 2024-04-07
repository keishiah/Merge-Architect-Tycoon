using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestRenderer : MonoBehaviour
{
    public TextMeshProUGUI QuestText;
    public Image QuestiImage;
    public List<QuestRewardRenderer> RewardRenderers;
    public List<QuestObjectiveRenderer> ObjectiveRenderers;
    public Button ClaimButton;

    [Space]
    [SerializeField] private RectTransform questRewardRendererParent;
    [SerializeField] private QuestRewardRenderer questRewardRendererPrefab;
    [SerializeField] private RectTransform questObjectiveRendererParent;
    [SerializeField] private QuestObjectiveRenderer questObjectiveRendererPrefab;

    [Space]
    public QuestData CurrentData;
    private BaseQuestInfo info => CurrentData.QuestInfo;

    public void Show(QuestData quest)
    {
        CurrentData = quest;
        RenderQuestHeader();
        RenderDetails();
    }

    private void RenderQuestHeader()
    {
        if(!gameObject.activeSelf)
            gameObject.SetActive(true);
        QuestText.text = info.Discription;
        QuestiImage.sprite = info.Sprite;
    }
    private void RenderDetails()
    {
        for (int i = 0; i < info.RewardList.Count || i < RewardRenderers.Count; i++)
        {
            RenderRewardElement(i);
        }
        for (int i = 0; i < info.Objectives.Count || i < ObjectiveRenderers.Count; i++)
        {
            RenderObjectiveElement(i);
        }
    }
    private void RenderObjectiveElement(int i)
    {
        if (i >= ObjectiveRenderers.Count)
        {
            QuestObjectiveRenderer element = Instantiate(questObjectiveRendererPrefab, questObjectiveRendererParent);
            ObjectiveRenderers.Add(element);
        }

        if (i < info.Objectives.Count)
        {
            ObjectiveRenderers[i].gameObject.SetActive(true);
            ObjectiveRenderers[i].RenderObjective(info.Objectives[i], CurrentData.ProgressList[i].Value);
        }
        else if (i < ObjectiveRenderers.Count)
            ObjectiveRenderers[i].gameObject.SetActive(false);
    }
    private void RenderRewardElement(int i)
    {
        if (i >= RewardRenderers.Count)
        {
            QuestRewardRenderer element = Instantiate(questRewardRendererPrefab, questRewardRendererParent);
            RewardRenderers.Add(element);
        }

        if (i < info.RewardList.Count)
        {
            RewardRenderers[i].gameObject.SetActive(true);
            RewardRenderers[i].RenderReward(info.RewardList[i].rewardAmount.ToString(), info.RewardList[i].rewardSprite);
        }
        else if (i < RewardRenderers.Count)
            RewardRenderers[i].gameObject.SetActive(false);
    }
}