using UnityEngine;

public class MergeWidget : WidgetView
{
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private GameObject _menuPanel;

    private void Awake()
    {
        if(enabled)
        {
            OnOpen();
        }
        else
        {
            OnClose();
        }
    }

    public override void OnOpen()
    {
        base.OnOpen();
        _infoPanel.SetActive(false);
        _menuPanel.SetActive(false);
    }

    public override void OnClose()
    {
        base.OnClose();
        _menuPanel.SetActive(true);
    }
}