using UnityEngine;

public class MergeWidget : Widget
{
    [SerializeField] GameObject _trucks;
    [SerializeField] GameObject _infoPanel;

    public override void OnEnable()
    {
        base.OnEnable();
        _trucks.SetActive(false);
        _infoPanel.SetActive(true);
    }
}