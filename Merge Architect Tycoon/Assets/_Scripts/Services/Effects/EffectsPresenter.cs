using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EffectsPresenter : MonoBehaviour
{
    public List<ParticleSystem> smokeEffectsList;


    public void PlaySmokeEffect(Vector2 position)
    {
        if (GetInactiveSmoke(out ParticleSystem smokeEffect))
        {
            smokeEffect.gameObject.SetActive(true);
            smokeEffect.gameObject.transform.position = position;
            smokeEffect.Play();
        }
    }

    public void StopSmokeEffect(Vector2 position)
    {
        foreach (ParticleSystem smoke in smokeEffectsList)
        {
            if (smoke.gameObject.activeSelf && (Vector2)smoke.gameObject.transform.position == position)
            {
                smoke.Stop();
                smoke.gameObject.SetActive(false);
                return;
            }
        }
    }

    private bool GetInactiveSmoke(out ParticleSystem smokeEffect)
    {
        foreach (ParticleSystem smoke in this.smokeEffectsList)
        {
            if (!smoke.gameObject.activeSelf)
            {
                smokeEffect = smoke;
                return true;
            }
        }

        smokeEffect = default;
        return false;
    }
}