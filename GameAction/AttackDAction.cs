using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.EcsEntity;
using ECS.Component;
public class AttackDAction : DataAction
{
     public  AttackDAction(Entity Creator,List<Entity> Aims = null)
     {
          actionCreator = Creator;
          actionAims = Aims;
     } 
     public override IEnumerator StartAction()
     {
        PropertyComponent propertyC = actionCreator._components[EcsConst.ComponentConst.PROPERTY] as PropertyComponent;
        
        int attack = propertyC.attack;
        yield return new WaitForSeconds(1f);
        foreach (var item in actionAims)
        {
            PropertyComponent propertyCAim = item._components[EcsConst.ComponentConst.PROPERTY] as PropertyComponent;
            TransformComponent transformC  = item._components[EcsConst.ComponentConst.TRANFORM] as TransformComponent;
            propertyCAim.hp  = propertyCAim.hp - attack ;
        }
     }        
}
