using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityData : ScriptableObject
{
   public AbilityItem[] AbilityItems;
}
[System.Serializable]
public class AbilityItem
{
     public string AbilityID;
     public float AbilityCD;
     public bool isHaveChangeHpAction;
     public bool isHaveChangePropertyAction;
     public bool isHaveChangePostionAction;
     public ChangeHpAction changeHpData;
     public ChangePropertyAction changePropertyData;
     public ChangePostionAction changePostionData;
}