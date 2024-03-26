using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EffectsPresenter : MonoBehaviour
{
    public ParticleSystem smokeEffect;


    public void PlaySmokeEffect(Vector2 position)
    {
        smokeEffect.gameObject.transform.position = position;
        smokeEffect.Play();
    }

    public void StopSmokeEffect()
    {
        smokeEffect.Stop();
    }
}