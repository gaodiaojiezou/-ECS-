using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.EcsEntity;
using ECS.Component; 
public class SkillDAction : DataAction
{
     private Ability ability; 
     public  SkillDAction(Entity Creator,Ability useAbility,List<Entity> Aims = null)
     {
          actionCreator = Creator;
          actionAims = Aims;
          ability = useAbility;
     }  
     public override IEnumerator StartAction()
     {
        yield return new WaitForSeconds(1f);
        ability.actions.TryGetValue(EcsConst.ABilityActionType.ChangeHp,out ABilityAction action);
        if (action != null)
        {
           ChangeHpAction changeHpAction = action as ChangeHpAction;
           if (changeHpAction.camp == EcsConst.EntityCamp.Enemy)
           {
               foreach (var item in actionAims)
               {
                  PropertyComponent propertyC = item._components[EcsConst.ComponentConst.PROPERTY] as PropertyComponent; 
                  TransformComponent transformC  = item._components[EcsConst.ComponentConst.TRANFORM] as TransformComponent;
                  UnityInstanceComponent unityInstanceC  = item._components[EcsConst.ComponentConst.UNITYINSTANCE] as UnityInstanceComponent;
                  propertyC.hp = propertyC.hp - changeHpAction.HpNum;
                  unityInstanceC.hpSlider.value = propertyC.hp/propertyC.maxHp;
                  //跳出伤害数字
                  Locator.pool.GetDamageObj(transformC.position,changeHpAction.HpNum);
               }            
           }else if(changeHpAction.camp == EcsConst.EntityCamp.Player)
           {
              PropertyComponent propertyC = actionCreator._components[EcsConst.ComponentConst.PROPERTY] as PropertyComponent; 
              if ((propertyC.hp + changeHpAction.HpNum)> propertyC.maxHp)
              {
                propertyC.hp = int.Parse(propertyC.maxHp.ToString());
              }else
              {
                  propertyC.hp = propertyC.hp + changeHpAction.HpNum;
              }
           }

        }  
     }       
}
