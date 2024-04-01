using System;
using UnityEngine;

public class TruckToUnload : TruckBehaviour
{
    public float StartXPosition;
    public float EndXPosition;
    public float Speed = 250f;

    public RectTransform RectTtransform;

    public override void Enter()
    {
        RectTtransform.anchoredPosition = new Vector2(StartXPosition, 0);
        Time = DateTime.Now;
    }
    public override void Update()
    {
        if(RectTtransform.anchoredPosition.x >= EndXPosition)
        {
            RectTtransform.anchoredPosition = new Vector2(EndXPosition, 0);
            IsComplete = true;
            return;
        }

        TimeSpan inWayTime = DateTime.Now - Time;
        float traversedPath = (float)inWayTime.TotalSeconds * Speed;
        RectTtransform.anchoredPosition = new Vector2(StartXPosition + traversedPath, 0);
    }
}