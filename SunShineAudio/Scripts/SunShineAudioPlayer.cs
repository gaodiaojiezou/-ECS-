using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SunShineAudio
{
    [RequireComponent(typeof(AudioSource))]
    public class SunShineAudioPlayer 
    {
        public static SunShineAudioPlayer _instance;
        public float fadeInSeconds = 1.0f;
        [SerializeField]
        private string currentBgm;
        private AudioMaster audioMaster;
        private Dictionary<string, AudioGroup> currentGroup = new Dictionary<string, AudioGroup>();
        public float MusicVolume
        {
            get
            {
                return AudioData.instance.MusicVolume;
            }
        }
        public float SoundVolume
        {
            get
            {
                return AudioData.instance.SoundVolume;
            }
        }

        public SunShineAudioPlayer()
        {
        }
        public void Initialize()
        {
            _instance = this;
            GameObject obj = Locator.ResLoader.LoadResource<GameObject>("Assets/Prefabs/Audio/AudioMaster.prefab");
            GameObject _audioMaster = GameObject.Instantiate(obj);
            audioMaster = _audioMaster.GetComponent<AudioMaster>();
            audioMaster.Initialize();
        }
        public void ChangeGameMusicVolume(float volume)
        {
            AudioData.instance.MusicVolume = volume;
            audioMaster.ChangeGameBgmVolume();
        }
        public void ChangeSoundVolume(float volume)
        {
            AudioData.instance.SoundVolume = volume;
            foreach(AudioGroup ex in currentGroup.Values)
            {
                ex.ChangeGameSoundVolume();
            }
        }

        public void PlayBgm(string bgmName)
        {
            audioMaster.PlayBgm(bgmName);
            currentBgm = bgmName;
            
        }

        public void StopBgm()
        {
            audioMaster.StopBgm();
        }
        public void OpenBgmCycleMode()
        {
            audioMaster.isCycleBgm = true;
        }
        public void CloseBgmCycleMode()
        {
            audioMaster.isCycleBgm = false;
        }
        public void AddCycleBgm(string name)
        {
            audioMaster.cycleBgmList.Add(name);
        }
        public void PlaySound(string name)
        {
            AudioGroup audioGroup;
            if (!currentGroup.ContainsKey(name))
            {
                GameObject obj = Locator.ResLoader.LoadResource<GameObject>("Assets/Prefabs/Audio/AudioGroup.prefab");
                GameObject _audioGroup = GameObject.Instantiate(obj);
                _audioGroup.transform.name = name + "_AudioGroup";
                _audioGroup.transform.parent = audioMaster.transform;
                audioGroup = _audioGroup.GetComponent<AudioGroup>();
                audioGroup.Initialize(AudioData.instance.GetSoundMessage(name));
                currentGroup.Add(name, audioGroup);
            }
            else
            {
                currentGroup.TryGetValue(name, out audioGroup);
            }
            audioGroup.Play();
        }

    }
}
