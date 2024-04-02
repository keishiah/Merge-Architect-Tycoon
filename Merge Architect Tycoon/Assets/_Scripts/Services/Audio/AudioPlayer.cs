using UnityEngine;
using Zenject;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource _backgroundMusicAudioSource;
    private AudioSource _uiMusicAudioSource;
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
        _backgroundMusicAudioSource = gameObject.AddComponent<AudioSource>();
        _uiMusicAudioSource = gameObject.AddComponent<AudioSource>();

        _backgroundMusicAudioSource.clip = _staticDataService.AudioData.backgroundMusic;
        _backgroundMusicAudioSource.loop = true;

        _backgroundMusicAudioSource.enabled = _settings.IsBackgroundSoundOn;
        _uiMusicAudioSource.enabled = _settings.IsEffectsSoundOn;
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
    public void SwitchBackgroundVolume() => _backgroundMusicAudioSource.enabled = !_backgroundMusicAudioSource.enabled;
    public void SwitchEffectsVolume() => _uiMusicAudioSource.enabled = !_uiMusicAudioSource.enabled;
}