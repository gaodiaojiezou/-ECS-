using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModuleInit : MonoBehaviour
{
    private void Awake() 
    {
        Locator.ResLoader = new ResourceLoader();
        Locator.ResLoader.Initialize();
        Locator.audioPlayer = new SunShineAudio.SunShineAudioPlayer();
        Locator.audioPlayer.Initialize();
        Locator.audioPlayer.PlayBgm("Forest");
        Locator.audioPlayer.ChangeGameMusicVolume(0.5f);
        Locator.audioPlayer.ChangeSoundVolume(0.5f);
        
    }
}
