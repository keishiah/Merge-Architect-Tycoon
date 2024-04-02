using System;
using UnityEngine;

public class TruckGoAway : TruckBehaviour
{
    public float StartXPosition;
    public float EndXPosition;
    public float Speed = 250f;

    public RectTransform RectTtransform;

    public override void Enter()
    {
        StartXPosition = RectTtransform.anchoredPosition.x;
        Time = DateTime.Now;
    }

    public override void Update()
    {
        if (RectTtransform.anchoredPosition.x > EndXPosition)
        {
            IsComplete = true;
            return;
        }

        TimeSpan inWayTime = DateTime.Now - Time;
        float traversedPath = (float)inWayTime.TotalSeconds * Speed;
        RectTtransform.anchoredPosition = new Vector2(StartXPosition + traversedPath, 0);
    }
}