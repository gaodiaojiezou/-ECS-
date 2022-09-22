using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPropertyData:ScriptableObject
{
    public PlayerPropertyItem[] playerItems ;
}
[System.Serializable]
public class PlayerPropertyItem
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
