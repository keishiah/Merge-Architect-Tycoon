using System;
using UnityEngine;
using Zenject;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource _backgroundMusicAudioSource;
    private AudioSource _uiMusicAudioSource;
    private IStaticDataService _staticDataService;

    [Inject]
    void Construct(IStaticDataService staticDataService)
    {
        _staticDataService = staticDataService;
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
    }

    public void PlayBackgroundMusic()
    {
        _backgroundMusicAudioSource.Play();
    }

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
}