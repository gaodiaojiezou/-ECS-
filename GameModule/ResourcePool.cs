using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePool 
{
    private Dictionary<string,Queue<GameObject>> enemyObjPool = new Dictionary<string, Queue<GameObject>>();
    private Queue<GameObject> damagePool = new Queue<GameObject>();
    private static readonly string enemyObjPath = "Assets/Prefabs/EntityObj/"; 
    private static readonly string damageObjPath = "Assets/Prefabs/UI/Damage.prefab";  

    public GameObject GetEnemyObj(string enemyName)
    {
        GameObject obj = null;
        if (enemyObjPool.ContainsKey(enemyName))
        {
            if (enemyObjPool[enemyName].Count > 0)
            {
                obj = enemyObjPool[enemyName].Dequeue();
                obj.SetActive(true);                
            }
        }
        if (obj != null)
        {
            return obj;
        }else
        {
            GameObject gameObject  = Locator.ResLoader.LoadResource<GameObject>(enemyObjPath + enemyName +".prefab");
            obj = GameObject.Instantiate(gameObject,Locator.battle.enemyParent);
            obj.name = enemyName;
            return obj;
        }
    }
    public void RecycleEnemyObj(GameObject obj)
    {
       obj.transform.localRotation = Quaternion.identity; 
       string name = obj.name;
        if (!enemyObjPool.ContainsKey(name))
        {       
           Queue<GameObject> queue = new Queue<GameObject>();
           queue.Enqueue(obj);
           enemyObjPool.Add(name,queue);
        }else
        {
            enemyObjPool[name].Enqueue(obj);
        }
        obj.SetActive(false);
    }
    public void GetDamageObj(Vector3 pos,int damage) 
    {
        GameObject obj = null;
        if (damagePool.Count > 0)
        {
            obj = damagePool.Dequeue();
        }else
        {
            GameObject gameObject  = Locator.ResLoader.LoadResource<GameObject>(damageObjPath);
            obj = GameObject.Instantiate(gameObject,Locator.battle.damageParent);
        }
        Vector2 screenPos = Camera.main.WorldToScreenPoint(pos); 
        obj.transform.position = screenPos + new Vector2(0,100);
        ShowDamage showDamage = obj.transform.GetComponent<ShowDamage>();
        showDamage.damage = damage;
        obj.SetActive(true);
    }
    public void RecycleDamageObj(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        damagePool.Enqueue(obj);
    }
}
