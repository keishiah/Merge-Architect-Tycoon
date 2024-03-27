using UnityEngine;

namespace _Scripts.Services.Audio
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "StaticData/AudioData", order = 0)]
    public class AudioData : ScriptableObject
    {
        public AudioClip backgroundMusic;
        public AudioClip buttonClickSound;
    }
}