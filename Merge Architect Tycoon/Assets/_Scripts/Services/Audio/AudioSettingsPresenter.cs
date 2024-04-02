using Zenject;

public class AudioSettingsPresenter : IInitializableOnSceneLoaded
{
    [Inject] private ApplicationSettings _settings;
    [Inject] private SettingsPanel _settingsPanel;
    [Inject] private AudioServise _audioService;

    public void OnSceneLoaded()
    {
        if (!_settings.IsBackgroundSoundOn.Value)
            _settingsPanel.BackgroundSoundOff();
        if (!_settings.IsEffectsSoundOn.Value)
            _settingsPanel.EffectsSoundOff();

        _settingsPanel.BackgroundSoundSlider.onValueChanged.AddListener(_audioService.SetBackgroundVolume);
        _settingsPanel.EffectsSoundSlider.onValueChanged.AddListener(_audioService.SetEffectsVolume);
        //_settingsPanel.BackgroundSoundButton.onClick.AddListener(_audioService.SetBackgroundEnable);
        //_settingsPanel.EffectsSoundSlider.onValueChanged.AddListener(_audioService.SetEffectsVolume);
    }
}
