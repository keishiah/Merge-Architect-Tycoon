using UniRx;
using Zenject;

public class AudioSettingsPresenter : IInitializableOnSceneLoaded
{
    [Inject] private ApplicationSettings _settings;
    [Inject] private AudioPlayer _audioPlayer;
    [Inject] private AudioServise _audioService;
    [Inject] private SettingsPanel _settingsPanel;

    private AudioSettings _audio => _settings.Audio;

    public void OnSceneLoaded()
    {
        if (!_audio.IsBackgroundSoundOn.Value)
            _settingsPanel.BackgroundSoundOff();
        if (!_audio.IsEffectsSoundOn.Value)
            _settingsPanel.EffectsSoundOff();
        _settingsPanel.SerBackgroundVolume(_audio.BackgroundSound.Value);
        _settingsPanel.SerEffectsVolume(_audio.EffectsSound.Value);

        _settingsPanel.BackgroundSoundSlider.onValueChanged.AddListener(_audioService.SetBackgroundVolume);
        _settingsPanel.EffectsSoundSlider.onValueChanged.AddListener(_audioService.SetEffectsVolume);
        _settingsPanel.BackgroundSoundButton.onClick.AddListener(_audioService.SwitchBackground);
        _settingsPanel.EffectsSoundButton.onClick.AddListener(_audioService.SwitchEffects);
        _settingsPanel.BackgroundSoundButton.onClick.AddListener(_settingsPanel.SwitchBackgroundSound);
        _settingsPanel.EffectsSoundButton.onClick.AddListener(_settingsPanel.SwitchEffectsSound);

        _audio.IsBackgroundSoundOn.Subscribe(_audioPlayer.SetBackgroundEnabled);
        _audio.IsEffectsSoundOn.Subscribe(_audioPlayer.SetEffectsEnabled);
        _audio.BackgroundSound.Subscribe(_audioPlayer.SetBackgroundVolume);
        _audio.EffectsSound.Subscribe(_audioPlayer.SetEffectsVolume);
    }
}
