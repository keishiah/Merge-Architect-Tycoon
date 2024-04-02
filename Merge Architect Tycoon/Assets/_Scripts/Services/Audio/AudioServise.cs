using Zenject;

public class AudioServise
{
    [Inject] private ApplicationSettings _settings;

    public void SetBackgroundVolume(float volume)
    {
        _settings.Audio.BackgroundSound.Value = volume;
        SaveLoadService.Save(SaveKey.SoundSettings, _settings.Audio);
    }
    public void SetBackgroundEnable(bool enable)
    {
        _settings.Audio.IsBackgroundSoundOn.Value = enable;
        SaveLoadService.Save(SaveKey.SoundSettings, _settings.Audio);
    }
    public void SwitchBackground()
    {
        _settings.Audio.IsBackgroundSoundOn.Value = !_settings.Audio.IsBackgroundSoundOn.Value;
        SaveLoadService.Save(SaveKey.SoundSettings, _settings.Audio);
    }

    public void SetEffectsVolume(float volume)
    {
        _settings.Audio.EffectsSound.Value = volume;
        SaveLoadService.Save(SaveKey.SoundSettings, _settings.Audio);
    }
    public void SetEffectsEnable(bool enable)
    {
        _settings.Audio.IsEffectsSoundOn.Value = enable;
        SaveLoadService.Save(SaveKey.SoundSettings, _settings.Audio);
    }
    public void SwitchEffects()
    {
        _settings.Audio.IsEffectsSoundOn.Value = !_settings.Audio.IsEffectsSoundOn.Value;
        SaveLoadService.Save(SaveKey.SoundSettings, _settings.Audio);
    }
}