using System;
using UnityEngine;

namespace _Scripts.Logic.Trucks.Behaviour
{
    public class TruckGoAway : TruckBehaviour
    {
        public float _startXPosition;
        public float _endXPosition;
        public float _speed = 250f;

        public RectTransform _rectTtransform;

        public override void Enter()
        {
            _startXPosition = _rectTtransform.anchoredPosition.x;
            _time = DateTime.Now;
        }

        public override void Update()
        {
            if (_rectTtransform.anchoredPosition.x > _endXPosition)
            {
                IsComplete = true;
                return;
            }

            TimeSpan inWayTime = DateTime.Now - _time;
            float traversedPath = (float)inWayTime.TotalSeconds * _speed;
            _rectTtransform.anchoredPosition = new Vector2(_startXPosition + traversedPath, 0);
        }
    }
}