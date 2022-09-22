using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestory : MonoBehaviour
{
    public float lifeTime;
    private float deltaTime;
    void OnEnable()
    {
        deltaTime = lifeTime;
    }   
    private void Update() 
    {
        deltaTime -= Time.deltaTime;
        if(deltaTime <= 0)
        {
            Destroy(this.gameObject);
        }
    } 
}
