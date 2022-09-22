using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.EcsEntity;
using ECS.Component;
public class EnterPosePAction:PerformanceAction
{
     public  EnterPosePAction(Entity Creator,List<Entity> Aims = null)
     {
          actionCreator = Creator;
          actionAims = Aims;
     }
     public override IEnumerator StartAction()
     {
       UnityInstanceComponent unityInstanceC  = actionCreator._components[EcsConst.ComponentConst.UNITYINSTANCE] as UnityInstanceComponent;
       BehaviourStateComponent behaviourStateC = actionCreator._components[EcsConst.ComponentConst.STATE] as BehaviourStateComponent;
       unityInstanceC.m_Trans.gameObject.SetActive(true);
       Locator.audioPlayer.PlaySound("EnterPose");
       unityInstanceC.m_Animator.Play("switch");
       float length = Locator.actionFunc.GetAniamatorLength(unityInstanceC.m_Animator,"switch");
       yield return new WaitForSeconds(length);
       behaviourStateC.state = EcsConst.EntityState.Battle;
     }
}
