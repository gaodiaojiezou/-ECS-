using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SunShineAudio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioMaster : MonoBehaviour
    {
        private AudioSource bgmCar;
        private SunShineAudioPlayer player;
        private AudioMessage currentBgmMessage;
        public bool isCycleBgm = false;
        public List<string> cycleBgmList = new List<string>();

        public void Initialize()
        {
           bgmCar = transform.GetComponent<AudioSource>();
            player = SunShineAudioPlayer._instance;
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// 播放Bgm
        /// </summary>
        public void PlayBgm(string bgmName)
        {
            StopAllCoroutines();
            currentBgmMessage = AudioData.instance.GetBgmMessage(bgmName);
            if (currentBgmMessage == null)
                return;
            bgmCar.clip = currentBgmMessage.audioClip;
            bgmCar.loop = currentBgmMessage.IsLoop;
            bgmCar.volume = 0;  
            bgmCar.Play();
            float currentVolume = currentBgmMessage.volume * AudioData.instance.MusicVolume;
            if (player.fadeInSeconds > 0f)
            {
                bgmCar.volume = 0f;
                StartCoroutine(FadeToVolume(bgmCar, currentVolume, player.fadeInSeconds));
            }
        }
        /// <summary>
        /// 停止播放Bgm
        /// </summary>
        public void StopBgm()
        {
            StopAllCoroutines();
            if (player.fadeInSeconds > 0f && gameObject.activeInHierarchy)
            {
                StartCoroutine(StopBgmCo(player.fadeInSeconds));
            }
            else
            {
                bgmCar.Stop();
            }
        }
        public void ChangeGameBgmVolume()
        {
            bgmCar.volume = currentBgmMessage.volume * AudioData.instance.MusicVolume;
        }

        private IEnumerator StopBgmCo(float fadeOutSeconds)
        {
            yield return FadeToVolume(bgmCar, 0f, fadeOutSeconds);
            bgmCar.Stop();
        }

        /// <summary>
        /// 声音的淡入淡出
        /// </summary>
        /// <param name="audioSource"></param>
        /// <param name="volume"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        private IEnumerator FadeToVolume(AudioSource audioSource, float volume, float duration)
        {
            float time = 0f;
            float orginalvolume = audioSource.volume;
            while (time < duration)
            {
                time += UnityEngine.Time.deltaTime;
                audioSource.volume = Mathf.Lerp(orginalvolume, volume, time / duration);
                yield return new WaitForEndOfFrame();
            }
            audioSource.volume = volume;
        }
        private void Update()
        {
            if(isCycleBgm)
            {
                if(!bgmCar.isPlaying)
                {
                    int BgmNum = Random.Range(0, cycleBgmList.Count);
                    PlayBgm(cycleBgmList[BgmNum]);
                }
            }
        }
    }
}
