using UnityEngine;
using Zenject;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusicAudioSource;
    [SerializeField] private AudioSource _uiMusicAudioSource;
    private StaticDataService _staticDataService;
    private ApplicationSettings _settings;

    private AudioSettings _audioSettings => _settings.Audio;

    [Inject]
    void Construct(StaticDataService staticDataService, ApplicationSettings audioSettings)
    {
        _staticDataService = staticDataService;
        _settings = audioSettings;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void InitializeAudioPlayer()
    {
        _backgroundMusicAudioSource.clip = _staticDataService.AudioData.backgroundMusic;
        _backgroundMusicAudioSource.Play();

        SetBackgroundEnabled(_audioSettings.IsBackgroundSoundOn.Value);
        SetEffectsEnabled(_audioSettings.IsEffectsSoundOn.Value);
        SetBackgroundVolume(_audioSettings.BackgroundSound.Value);
        SetEffectsVolume(_audioSettings.EffectsSound.Value);
    }

    public void PlayBackgroundMusic() => _backgroundMusicAudioSource.Play();

    public void PlayUiSound(UiSoundTypes soundTypesType)
    {
        if (!_uiMusicAudioSource.enabled)
            return;

        switch (soundTypesType)
        {
            case UiSoundTypes.ButtonClick:
                _uiMusicAudioSource.PlayOneShot(_staticDataService.AudioData.buttonClickSound);
                break;
        }
    }

    public void SetBackgroundVolume(float volume) => _backgroundMusicAudioSource.volume = volume;
    public void SetEffectsVolume(float volume) => _uiMusicAudioSource.volume = volume;
    public void SetBackgroundEnabled(bool v) => _backgroundMusicAudioSource.enabled = v;
    public void SetEffectsEnabled(bool v) => _uiMusicAudioSource.enabled = v;
}