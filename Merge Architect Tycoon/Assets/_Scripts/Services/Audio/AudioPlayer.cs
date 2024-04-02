using UnityEngine;
using Zenject;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusicAudioSource;
    [SerializeField] private AudioSource _uiMusicAudioSource;
    private StaticDataService _staticDataService;
    private ApplicationSettings _settings;

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
        _backgroundMusicAudioSource.loop = true;

        SetBackgroundEnabled(_settings.IsBackgroundSoundOn.Value);
        SetEffectsEnabled(_settings.IsEffectsSoundOn.Value);
        SetBackgroundVolume(_settings.BackgroundSoundValue.Value);
        SetEffectsVolume(_settings.EffectsSoundValue.Value);
    }

    public void PlayBackgroundMusic() => _backgroundMusicAudioSource.Play();

    public void PlayUiSound(UiSoundTypes soundTypesType)
    {
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