using System;
using UniRx;

[Serializable]
public class ApplicationSettings
{
    public ReactiveProperty<bool> IsBackgroundSoundOn = new(true);
    public ReactiveProperty<float> BackgroundSoundValue = new(1f);
    public ReactiveProperty<bool> IsEffectsSoundOn = new(true);
    public ReactiveProperty<float> EffectsSoundValue = new(1f);
}
