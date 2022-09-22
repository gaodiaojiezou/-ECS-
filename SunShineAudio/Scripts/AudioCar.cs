using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SunShineAudio
{
    public class AudioCar:MonoBehaviour
    {
        private AudioGroup audioGroup;
        private AudioSource audioSource;
        public void Initialize(AudioGroup _audioGroup)
        {
            audioGroup = _audioGroup;
            audioSource = transform.GetComponent<AudioSource>();
            audioSource.clip = audioGroup.audioMessage.audioClip;
            audioSource.loop = audioGroup.audioMessage.IsLoop;
            audioSource.volume = audioGroup.audioMessage.volume*AudioData.instance.SoundVolume;
        }
        public void Play()
        {
            audioSource.Play();
        }
        public bool IsPlaying()
        {
            return audioSource.isPlaying;
        }
        public void ChangeGameSoundVolume()
        {
            audioSource.volume = audioGroup.audioMessage.volume * AudioData.instance.SoundVolume;
        }
    }
}
