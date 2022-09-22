using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.EcsEntity;
using ECS.Component; 
public class Skill3PAction : PerformanceAction
{

     public  Skill3PAction(Entity Creator,List<Entity> Aims = null)
     {
          actionCreator = Creator;
          actionAims = Aims;
     }
     public override IEnumerator StartAction()
     {
       UnityInstanceComponent unityInstanceC  = actionCreator._components[EcsConst.ComponentConst.UNITYINSTANCE] as UnityInstanceComponent;
       BehaviourStateComponent behaviourStateC = actionCreator._components[EcsConst.ComponentConst.STATE] as BehaviourStateComponent;
       Locator.audioPlayer.PlaySound("Skill3");
       unityInstanceC.m_Animator.Play("skill3");
       GameObject gameObject1  = Locator.ResLoader.LoadResource<GameObject>("Assets/Prefabs/SkillEffect/bulunxierde_skin01/FX_bulunxierde_skin01_skill3_shifa.prefab");
       Vector3 pos = new Vector3(unityInstanceC.m_Trans.position.x,unityInstanceC.m_Trans.position.y,-2f);
       GameObject.Instantiate(gameObject1,pos,unityInstanceC.m_Trans.rotation); 
       float length = Locator.actionFunc.GetAniamatorLength(unityInstanceC.m_Animator,"skill3");   
       yield return new WaitForSeconds(1.2f);
       GameObject gameObject2  = Locator.ResLoader.LoadResource<GameObject>("Assets/Prefabs/SkillEffect/bulunxierde_skin01/FX_bulunxierde_skin01_skill3.prefab");
       Vector3 pos1 = new Vector3(unityInstanceC.m_Trans.position.x,unityInstanceC.m_Trans.position.y,-2f);
       GameObject.Instantiate(gameObject2,pos1,unityInstanceC.m_Trans.rotation);        
       behaviourStateC.battleState = EcsConst.EntityBattleState.Idle;
     }
}
