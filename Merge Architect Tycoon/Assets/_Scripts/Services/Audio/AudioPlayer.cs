using System;
using System.Reflection;
using UnityEngine;
using Zenject;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private AudioSource _ui;

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
        _backgroundMusic.clip = _staticDataService.AudioData.BackgroundMusic;
        _backgroundMusic.Play();

        SetBackgroundEnabled(_audioSettings.IsBackgroundSoundOn.Value);
        SetEffectsEnabled(_audioSettings.IsEffectsSoundOn.Value);

        SetBackgroundVolume(_audioSettings.BackgroundSound.Value);
        SetEffectsVolume(_audioSettings.EffectsSound.Value);
    }

    public void PlayBackgroundMusic() => _backgroundMusic.Play();

    public void PlayUiSound(UiSoundTypes soundTypesType)
    {
        if (!_ui.enabled)
            return;

        switch (soundTypesType)
        {
            case UiSoundTypes.MenuButtonClick:
                PlayMenuButtonSound();
                break;
            case UiSoundTypes.SellItem:
                PlaySellSound();
                break;
            case UiSoundTypes.BuyUpdate:
                _ui.PlayOneShot(_staticDataService.AudioData.SalesRegister);
                break;
            case UiSoundTypes.QuestComplete:
                _ui.PlayOneShot(_staticDataService.AudioData.Victory);
                break;
            case UiSoundTypes.Building:
                _ui.PlayOneShot(_staticDataService.AudioData.Building);
                break;
            default:
                throw new System.Exception($"It is forbidden to use the {soundTypesType} type of audio!");
        }
    }

    public void PlayMergeSound(int tier)
    {
        _ui.PlayOneShot(_staticDataService.AudioData.MergeItem[--tier]);
    }
    private void PlayMenuButtonSound()
    {
        int count = _staticDataService.AudioData.MenuButton.Length;
        int index = UnityEngine.Random.Range(0, count);
        _ui.PlayOneShot(_staticDataService.AudioData.MenuButton[index]);
    }
    private void PlaySellSound()
    {
        int count = _staticDataService.AudioData.MoneyAdd.Length;
        int index = UnityEngine.Random.Range(0, count);
        _ui.PlayOneShot(_staticDataService.AudioData.MoneyAdd[index]);
    }

    public void SetBackgroundVolume(float volume) => _backgroundMusic.volume = volume;
    public void SetEffectsVolume(float volume) => _ui.volume = volume;
    public void SetBackgroundEnabled(bool v) => _backgroundMusic.enabled = v;
    public void SetEffectsEnabled(bool v) => _ui.enabled = v;
}