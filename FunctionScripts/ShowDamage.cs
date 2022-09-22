using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowDamage : MonoBehaviour
{
    private TextMeshProUGUI   cD1Text;
    public  int  damage = 1;
    private void OnEnable() 
    {
        cD1Text = this.GetComponent<TextMeshProUGUI>();
        cD1Text.text = "-"+damage;
        StartCoroutine(RecycleDamage());
    }

    private IEnumerator  RecycleDamage()
    {
        yield return new WaitForSeconds(0.5f);
        Locator.pool.RecycleDamageObj(this.gameObject);
    }
}
