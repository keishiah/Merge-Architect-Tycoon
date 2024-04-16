using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "StaticData/AudioData", order = 0)]
public class AudioData : ScriptableObject
{
    [Header("Background")]
    public AudioClip BackgroundMusic;
    [Header("UI")]
    public AudioClip[] MenuButton;
    [Space]
    public AudioClip[] MergeItem;
    [Space]
    public AudioClip SalesRegister;
    [Space]
    public AudioClip Building;
    [Space]
    public AudioClip[] MoneyAdd;
    [Space]
    public AudioClip Victory;
}