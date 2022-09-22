using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.EcsEntity;
using ECS.Component;
public class DeadPAction : PerformanceAction
{
     public  DeadPAction(Entity Creator,List<Entity> Aims = null)
     {
          actionCreator = Creator;
          actionAims = Aims;
     }
     public override IEnumerator StartAction()
     {
       UnityInstanceComponent unityInstanceC  = actionCreator._components[EcsConst.ComponentConst.UNITYINSTANCE] as UnityInstanceComponent;
       unityInstanceC.m_Animator.Play("die");
       float length = Locator.actionFunc.GetAniamatorLength(unityInstanceC.m_Animator,"die");
       GameObject gameObject  = unityInstanceC.m_Trans.gameObject;
       yield return new WaitForSeconds(length);
       if (unityInstanceC.hpSlider)
       {
          unityInstanceC.hpSlider.value = 1f;
       }
       Locator.pool.RecycleEnemyObj(gameObject);
       if (actionCreator == Locator.entityManager.playerEntity)
       {
            GameObject obj  = Locator.ResLoader.LoadResource<GameObject>("Assets/Prefabs/UI/DeadUI.prefab");
            GameObject.Instantiate(obj,Locator.battle.UIParent);      
       }
     }
}
