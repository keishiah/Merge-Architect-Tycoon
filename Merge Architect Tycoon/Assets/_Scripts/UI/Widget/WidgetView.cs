using UnityEngine;
using Zenject;

public class WidgetView : MonoBehaviour
{
    [Inject] EffectsPresenter _effectsPresenter;

    public virtual void OnOpen()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnClose()
    {
        gameObject.SetActive(false);
    }
}