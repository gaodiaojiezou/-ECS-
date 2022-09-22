using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunShineAudio
{
    public class AudioData
    {
        private static AudioData mInstance;
        private Dictionary<string, AudioMessage> soundGroup;
        private Dictionary<string, AudioMessage> bgmGroup;
        const string MUSIC_VOLUME = "MusicVolume_local";
        const string SOUND_VOLUME = "SoundVolume_local";
        private float mMusicVolume;
        private float mSoundVolume;
        //音乐音量
        public float MusicVolume
        {
            get { return mMusicVolume; }
            set
            {
                mMusicVolume = value;
                PlayerPrefs.SetFloat(MUSIC_VOLUME, value);
            }
        }
        //音效音量
        public float SoundVolume
        {
            get { return mSoundVolume; }
            set
            {
                mSoundVolume = value;
                PlayerPrefs.SetFloat(SOUND_VOLUME, value);
            }
        }
        public static AudioData instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new AudioData();
                    return mInstance;
                }
                else
                    return mInstance;
            }
        }
        public AudioData()
        {
            soundGroup = new Dictionary<string, AudioMessage>();
            bgmGroup = new Dictionary<string, AudioMessage>();
            AudioList audioList = Locator.ResLoader.LoadResource<AudioList>("Assets/ScriptableData/AudioList.asset");
            foreach(AudioMessage ex in audioList.bgmShowList)
            {
                bgmGroup.Add(ex.audioClip.name, ex);
            }    
            foreach(AudioMessage ex in audioList.soundShowList)
            {
                soundGroup.Add(ex.audioClip.name, ex);
            }
            UpdateMusicVolume();
            UpdateSoundVolume();
        }
        void UpdateMusicVolume()
        {
            if (PlayerPrefs.HasKey(MUSIC_VOLUME))
            {
                MusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME);
            }
            else
            {
                MusicVolume = 1f;
            }
        }
        //更新音效音量
        void UpdateSoundVolume()
        {
            if (PlayerPrefs.HasKey(SOUND_VOLUME))
            {
                SoundVolume = PlayerPrefs.GetFloat(SOUND_VOLUME);
            }
            else
            {
                SoundVolume = 1f;
            }
        }
        public AudioMessage GetSoundMessage(string name)
        {
            soundGroup.TryGetValue(name, out AudioMessage audioMessage);
            if (audioMessage == null)
            {
                Debug.Log("不存在该Sound" + name);
                return null;
            }
            return audioMessage;
        }
        public AudioMessage GetBgmMessage(string name)
        {
            bgmGroup.TryGetValue(name, out AudioMessage audioMessage);
            if (audioMessage == null)
            {
                Debug.Log("不存在该Bgm" + name);
                return null;
            }
            return audioMessage;
        }
    }
}
