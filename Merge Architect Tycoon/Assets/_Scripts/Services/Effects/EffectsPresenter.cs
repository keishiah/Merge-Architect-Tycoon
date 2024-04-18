using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EffectsPresenter : MonoBehaviour
{
    public List<ParticleSystem> smokeEffectsList;
    private Dictionary<string, ParticleSystem> _activeSmokeEffects = new();

    public void PlaySmokeEffect(Vector2 position, string buildingName)
    {
        if (GetInactiveSmoke(out ParticleSystem smokeEffect))
        {
            _activeSmokeEffects.Add(buildingName, smokeEffect);
            smokeEffect.gameObject.SetActive(true);
            smokeEffect.gameObject.transform.position = position;
            smokeEffect.Play();
        }
    }

    public void StopSmokeEffect(Vector2 position, string buildingName)
    {
        if (_activeSmokeEffects.TryGetValue(buildingName, out ParticleSystem smoke))
        {
            smoke.Stop();
            smoke.gameObject.SetActive(false);
            _activeSmokeEffects.Remove(buildingName);
        }
    }


    private bool GetInactiveSmoke(out ParticleSystem smokeEffect)
    {
        foreach (ParticleSystem smoke in smokeEffectsList)
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