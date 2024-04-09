using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestObjectiveRenderer : MonoBehaviour
{
    public TextMeshProUGUI itemText;
    public Image performanceItemImage;
    public Image checkMarkImage;
    public TextMeshProUGUI performanceItemText;

    public void RenderObjective(QuestProgress questProgress, QuestObjective objective, bool isDone)
    {
        itemText.text = objective.GetDescription();
        performanceItemImage.sprite = objective.Sprite;
        performanceItemText.text = objective.GetProgressText(questProgress);
        checkMarkImage.gameObject.SetActive(isDone);
    }
}