using System;
using UniRx;

[Serializable]
public class AudioSettings
{
    public ReactiveProperty<bool> IsBackgroundSoundOn = new(true);
    public ReactiveProperty<float> BackgroundSound = new(.8f);
    public ReactiveProperty<bool> IsEffectsSoundOn = new(true);
    public ReactiveProperty<float> EffectsSound = new(.8f);
}
