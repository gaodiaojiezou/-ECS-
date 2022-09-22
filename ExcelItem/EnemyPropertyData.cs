using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPropertyData:ScriptableObject
{
   public EnemyPropertyItem[] enemyItems;
}
[System.Serializable]
public class EnemyPropertyItem
{
    public string name;
    public int hp;
    public int attack;
    public float attackTime;
    public string Skill1ID;
    public string Skill2ID;
    public string Skill3ID;
    public float moveSpeed;

}