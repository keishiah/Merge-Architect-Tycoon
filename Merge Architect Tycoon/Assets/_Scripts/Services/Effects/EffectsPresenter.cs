using UnityEngine;

public class EffectsPresenter : MonoBehaviour
{
    public ParticleSystem smokeEffect;


    public void PlaySmokeEffect(Vector2 position)
    {
        smokeEffect.gameObject.SetActive(true);
        smokeEffect.gameObject.transform.position = position;
        smokeEffect.Play();
    }

    public void StopSmokeEffect()
    {
        smokeEffect.Stop();
        smokeEffect.gameObject.SetActive(false);
    }

}