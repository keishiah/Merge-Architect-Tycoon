using UnityEngine;
using UnityEngine.UI;

public class TruckInfoPanel : MonoBehaviour
{
    public GameObject InfoPanel;
    public Image image;
    public Sprite Off;
    public Sprite On;

    private void OnEnable()
    {
        InfoPanel.SetActive(false);
        image.sprite = Off;
    }
    public void SwitchInfo()
    {
        InfoPanel.SetActive(!InfoPanel.activeSelf);
        image.sprite = InfoPanel.activeSelf ? On : Off;
    }
}