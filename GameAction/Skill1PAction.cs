using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.EcsEntity;
using ECS.Component; 
public class Skill1PAction : PerformanceAction
{
     public  Skill1PAction(Entity Creator,List<Entity> Aims = null)
     {
          actionCreator = Creator;
          actionAims = Aims;
     }
     public override IEnumerator StartAction()
     {
       UnityInstanceComponent unityInstanceC  = actionCreator._components[EcsConst.ComponentConst.UNITYINSTANCE] as UnityInstanceComponent;
       BehaviourStateComponent behaviourStateC = actionCreator._components[EcsConst.ComponentConst.STATE] as BehaviourStateComponent;
       Locator.audioPlayer.PlaySound("Skill1");
       unityInstanceC.m_Animator.Play("skill1");
       GameObject gameObject  = Locator.ResLoader.LoadResource<GameObject>("Assets/Prefabs/SkillEffect/bulunxierde_skin01/FX_bulunxierde_skin01_skill1.prefab");
       Vector3 pos = new Vector3(unityInstanceC.m_Trans.position.x,unityInstanceC.m_Trans.position.y,-2f);
       GameObject playerObj = GameObject.Instantiate(gameObject,pos,unityInstanceC.m_Trans.rotation);
       float length = Locator.actionFunc.GetAniamatorLength(unityInstanceC.m_Animator,"skill1");
       yield return new WaitForSeconds(length);
       behaviourStateC.battleState = EcsConst.EntityBattleState.Idle;
     }       
}
