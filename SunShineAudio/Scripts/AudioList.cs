using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace SunShineAudio
{
    public class AudioList :ScriptableObject
    {
        public List<AudioMessage> bgmShowList = new List<AudioMessage>();
        public List<AudioMessage> soundShowList = new List<AudioMessage>();
    }
    [Serializable]
    public class AudioMessage
    {
        public AudioClip audioClip;
        public bool IsLoop;
        public float volume;
        public int maxNum;
    }
}
