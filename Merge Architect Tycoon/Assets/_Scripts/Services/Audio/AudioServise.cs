﻿using Zenject;

public class AudioServise
{
    [Inject] private ApplicationSettings _settings;

    public void SetBackgroundVolume(float volume)
    {
        _settings.BackgroundSoundValue.Value = volume;
        SaveLoadService.Save(SaveKey.SoundSettings, _settings);
    }
    public void SetBackgroundEnable(bool enable)
    {
        _settings.IsBackgroundSoundOn.Value = enable;
        SaveLoadService.Save(SaveKey.SoundSettings, _settings);
    }
    public void SwitchBackground()
    {
        _settings.IsBackgroundSoundOn.Value = !_settings.IsBackgroundSoundOn.Value;
        SaveLoadService.Save(SaveKey.SoundSettings, _settings);
    }

    public void SetEffectsVolume(float volume)
    {
        _settings.EffectsSoundValue.Value = volume;
        SaveLoadService.Save(SaveKey.SoundSettings, _settings);
    }
    public void SetEffectsEnable(bool enable)
    {
        _settings.IsEffectsSoundOn.Value = enable;
        SaveLoadService.Save(SaveKey.SoundSettings, _settings);
    }
    public void SwitchEffects()
    {
        _settings.IsEffectsSoundOn.Value = !_settings.IsEffectsSoundOn.Value;
        SaveLoadService.Save(SaveKey.SoundSettings, _settings);
    }
}