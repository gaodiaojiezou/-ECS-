using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SunShineAudio
{
    public class AudioGroup:MonoBehaviour
    {
        public AudioMessage audioMessage;
        private AudioCar[] audioCars;
     

        public void Initialize(AudioMessage _audioMessage)
        {
            audioMessage = _audioMessage;
            audioCars = new AudioCar[audioMessage.maxNum];
        }
        public void Play()
        {
            for(int i =0; i <audioCars.Length;i++ )
            {
                if(audioCars[i] == null)
                {
                    GameObject obj = Locator.ResLoader.LoadResource<GameObject>("Assets/Prefabs/Audio/AudioCar.prefab");
                    GameObject _audioCar = GameObject.Instantiate(obj);
                    _audioCar.transform.parent = transform;
                    _audioCar.transform.name = "AudioCar_" + i;
                    audioCars[i] = _audioCar.GetComponent<AudioCar>();
                    audioCars[i].Initialize(this);
                    audioCars[i].Play();
                    break;
                }
                if(!audioCars[i].IsPlaying())
                {
                    audioCars[i].Play();
                    break;
                }
            }
        }
        public void Stop()
        {

        }
        public void ChangeGameSoundVolume()
        {
            for (int i = 0; i < audioCars.Length; i++)
            {
                if (audioCars[i] != null)
                {
                    audioCars[i].ChangeGameSoundVolume();
                }
            }
         }
    }
}
