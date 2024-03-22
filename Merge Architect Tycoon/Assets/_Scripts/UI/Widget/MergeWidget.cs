﻿using UnityEngine;

public class MergeWidget : Widget
{
    [SerializeField] GameObject _trucks;
    [SerializeField] GameObject _infoPanel;

    public override void OnOpen()
    {
        base.OnOpen();
        _trucks.SetActive(false);
        _infoPanel.SetActive(false);
    }
}