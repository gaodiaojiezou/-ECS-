using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.EcsEntity;
using ECS.Component;
public class AttackPAction :PerformanceAction
{
     public  AttackPAction(Entity Creator,List<Entity> Aims = null)
     {
          actionCreator = Creator;
          actionAims = Aims;
     }
     public override IEnumerator StartAction()
     {
       UnityInstanceComponent unityInstanceC  = actionCreator._components[EcsConst.ComponentConst.UNITYINSTANCE] as UnityInstanceComponent;
       BehaviourStateComponent behaviourStateC = actionCreator._components[EcsConst.ComponentConst.STATE] as BehaviourStateComponent;
       unityInstanceC.m_Animator.Play("attack1");
       float length = Locator.actionFunc.GetAniamatorLength(unityInstanceC.m_Animator,"attack1");
       yield return new WaitForSeconds(length);
       behaviourStateC.battleState = EcsConst.EntityBattleState.Idle;
     }
}
