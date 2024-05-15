using System;
using System.Collections.Generic;
using DG.Tweening;
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
    public Image youWillGet;
    public Button OpenCloseObjectivesButton;
    public VerticalLayoutGroup ObjectivesLayout;
    private int _objectivesHeight;
    private bool _isOpened;

    public QuestPanel Panel;

    [Space] [SerializeField] private RectTransform questRewardRendererParent;
    [SerializeField] private QuestRewardRenderer questRewardRendererPrefab;
    [SerializeField] private RectTransform questObjectiveRendererParent;
    [SerializeField] private QuestObjectiveRenderer questObjectiveRendererPrefab;

    [Space] public QuestData CurrentData;
    private QuestInfo info => CurrentData.QuestInfo;

    private AudioPlayer _audio;

    public void Render(QuestData quest = null)
    {
        if (quest != null) CurrentData = quest;

        DisableAllDetails();
        RenderQuestHeader();
        RenderDetails();
        SetButtons();
        SetUpObjectivesHeight();
    }

    private void Start()
    {
        OpenCloseObjectivesButton.onClick.AddListener(OpenCloseObjectives);
    }

    private void SetUpObjectivesHeight()
    {
        if (ObjectiveRenderers.Count > 0)
        {
            var height = (int)ObjectiveRenderers[0].GetComponent<LayoutElement>().preferredHeight;
            ObjectivesLayout.padding.top =
                -1 * height * ObjectiveRenderers.Count - (int)ObjectivesLayout.spacing;
            _objectivesHeight = ObjectivesLayout.padding.top;
        }
    }


    private void OpenCloseObjectives()
    {
        if (!_isOpened)
        {
            _isOpened = true;
            DOTween.To(() => ObjectivesLayout.padding.top, x =>
                {
                    ObjectivesLayout.padding.top = x;
                    LayoutRebuilder.ForceRebuildLayoutImmediate(ObjectivesLayout.GetComponent<RectTransform>());
                }, 0, .7f)
                .SetEase(Ease.OutCubic);
        }
        else
        {
            _isOpened = false;

            DOTween.To(() => ObjectivesLayout.padding.top, x =>
                {
                    ObjectivesLayout.padding.top = x;
                    LayoutRebuilder.ForceRebuildLayoutImmediate(ObjectivesLayout.GetComponent<RectTransform>());
                }, _objectivesHeight, .7f)
                .SetEase(Ease.OutCubic);
        }
    }

    public void ClaimReward()
    {
        CurrentData.ClaimQuestReward();
        _audio.PlayUiSound(UiSoundTypes.QuestComplete);
        Panel.Refresh();
    }

    private void SetButtons()
    {
        ClaimButton.gameObject.SetActive(false);
        youWillGet.gameObject.SetActive(false);
        if (CurrentData.IsQuestComplete())
            ClaimButton.gameObject.SetActive(true);
        else
            youWillGet.gameObject.SetActive(true);
    }

    private void RenderQuestHeader()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
        QuestText.text = info.Discription;
        QuestiImage.sprite = info.Sprite;
    }

    private void RenderDetails()
    {
        for (int i = 0; i < info.RewardList.Count; i++)
        {
            RenderRewardElement(i);
        }

        for (int i = 0; i < info.Objectives.Count; i++)
        {
            RenderObjectiveElement(i);
        }
    }

    private void DisableAllDetails()
    {
        for (int i = 0; i < RewardRenderers.Count; i++)
        {
            RewardRenderers[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < ObjectiveRenderers.Count; i++)
        {
            ObjectiveRenderers[i].gameObject.SetActive(false);
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
            ObjectiveRenderers[i].RenderObjective(CurrentData.ProgressList[i], info.Objectives[i],
                CurrentData.IsObjectiveComplete(i));
        }
    }

    private void RenderRewardElement(int i)
    {
        if (info.RewardList[i].IsHiden)
            return;

        if (i >= RewardRenderers.Count)
        {
            QuestRewardRenderer element = Instantiate(questRewardRendererPrefab, questRewardRendererParent);
            RewardRenderers.Add(element);
        }

        if (i < info.RewardList.Count)
        {
            RewardRenderers[i].gameObject.SetActive(true);
            RewardRenderers[i].RenderReward(info.RewardList[i].Amount.ToString(), info.RewardList[i].Sprite);
        }
    }

    public void SetAudio(AudioPlayer audio)
    {
        _audio = audio;
    }
}