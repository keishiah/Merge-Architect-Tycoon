using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestObjectiveRenderer : MonoBehaviour
{
    public TextMeshProUGUI itemText;
    public Image performanceItemImage;
    public Image checkMarkImage;
    public TextMeshProUGUI performanceItemText;

    public void RenderObjective(QuestObjective objective, int currentItemCount = int.MinValue)
    {
        itemText.text = objective.GetDescription();
        performanceItemImage.sprite = objective.Sprite;
        if(currentItemCount != int.MinValue)
        {
            //performanceItemText.text = $"{objective.itemText} {currentItemCount}/{objective.GoalCount}";
            //checkMarkImage.gameObject.SetActive(currentItemCount>= objective.GoalCount);
        }
        else
            performanceItemText.text = objective.GetDescription();
    }
}