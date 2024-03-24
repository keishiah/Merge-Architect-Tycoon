using UnityEngine;
using Zenject;

public class MergeWidget : Widget
{
    [SerializeField] private GameObject _trucks;
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
        _trucks.SetActive(false);
        _infoPanel.SetActive(false);
        _menuPanel.SetActive(false);
    }

    public override void OnClose()
    {
        base.OnClose();
        _menuPanel.SetActive(true);
    }
}