using UnityEngine;
using UnityEngine.UI;

public class ItemInfoSlot : MonoBehaviour
{
    private MergeItem mergeItem;
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private Image questionMarkImage;
    [SerializeField]
    private Image nextItemArrowImage;

    public void SetItem(MergeItem m_mergeItem, bool isOpened)
    {
        questionMarkImage.gameObject.SetActive(false);
        nextItemArrowImage.gameObject.SetActive(false);
        itemImage.sprite = m_mergeItem.ItemSprite;
        mergeItem = m_mergeItem;

        if (!isOpened)
        {
            questionMarkImage.gameObject.SetActive(true);
        }
        if (m_mergeItem.nextItem != null)
        {
            nextItemArrowImage.gameObject.SetActive(true);
        }

    }
}