using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDeadController : MonoBehaviour
{
      private void Awake() 
      {
        Locator.audioPlayer.PlaySound("Dead");
        StartCoroutine(QuitGame());
      }
      IEnumerator QuitGame()
      {
        yield return new WaitForSeconds(2f);
        Application.Quit();
      }
}
