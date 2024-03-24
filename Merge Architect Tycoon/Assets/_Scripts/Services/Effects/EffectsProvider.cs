using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EffectsProvider : MonoBehaviour
{
    public ParticleSystem smokeEffect;

    public UniTask PlaySmokeEffect(Vector2 position)
    {
        smokeEffect.gameObject.transform.position = position;
        smokeEffect.Play();
        return UniTask.Delay(TimeSpan.FromSeconds(smokeEffect.main.startLifetime.constant));
    }
}