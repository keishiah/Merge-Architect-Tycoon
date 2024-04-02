using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "StaticData/AudioData", order = 0)]
public class AudioData : ScriptableObject
{
    public AudioClip backgroundMusic;
    public AudioClip buttonClickSound;
}