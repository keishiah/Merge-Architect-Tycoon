using UniRx;
using Zenject;

public class AudioSettingsPresenter : IInitializableOnSceneLoaded
{
    [Inject] private ApplicationSettings _settings;
    [Inject] private SettingsPanel _settingsPanel;
    [Inject] private AudioServise _audioService;
    [Inject] private AudioPlayer _audioPlayer;

    public void OnSceneLoaded()
    {
        if (!_settings.IsBackgroundSoundOn.Value)
            _settingsPanel.BackgroundSoundOff();
        if (!_settings.IsEffectsSoundOn.Value)
            _settingsPanel.EffectsSoundOff();

        _settingsPanel.BackgroundSoundSlider.onValueChanged.AddListener(_audioService.SetBackgroundVolume);
        _settingsPanel.EffectsSoundSlider.onValueChanged.AddListener(_audioService.SetEffectsVolume);
        _settingsPanel.BackgroundSoundButton.onClick.AddListener(_audioService.SwitchBackground);
        _settingsPanel.EffectsSoundButton.onClick.AddListener(_audioService.SwitchEffects);

        _settings.IsBackgroundSoundOn.Subscribe(_audioPlayer.SetBackgroundEnabled);
        _settings.IsEffectsSoundOn.Subscribe(_audioPlayer.SetEffectsEnabled);
        _settings.BackgroundSoundValue.Subscribe(_audioService.SetBackgroundVolume);
        _settings.EffectsSoundValue.Subscribe(_audioService.SetEffectsVolume);
    }
}
