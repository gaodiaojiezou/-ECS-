using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.EcsEntity;
using ECS.Component;
namespace ECS.EcsSystem
{
    public abstract class FsmState
    {
        public abstract void HandlePlayerInput(string INPUT,Entity entity);
        public void UseSkill(string skillID,Entity entity)
        {
            AbilityComponent abilityC = entity._components[EcsConst.ComponentConst.ABILITY] as AbilityComponent;
            BehaviourStateComponent behaviourStateC = entity._components[EcsConst.ComponentConst.STATE] as BehaviourStateComponent;
            Ability ability = abilityC.abilityDict[skillID];
            if(ability.CurrentCd > 0)
            return;
            ability.CurrentCd = ability.CdCoolNum;
            //建立数据层Action
            List<Entity> entities = Locator.systemFunction.GetPlayerSkillRangeEntityList(ability);
            SkillDAction skillD = new SkillDAction(entity,ability,entities);
            Locator.action.dataActions.Enqueue(skillD);
            //建立表现层Acion
            switch (skillID)
            {
              case EcsConst.SkillID.Skill1:
                    Skill1PAction skill1PAction = new Skill1PAction(entity);
                    Locator.action.performanceActions.Enqueue(skill1PAction);
                    behaviourStateC.battleState = EcsConst.EntityBattleState.Skill2;             
              break;
              case EcsConst.SkillID.Skill2:
                    Skill2PAction skill2PAction = new Skill2PAction(entity);
                    Locator.action.performanceActions.Enqueue(skill2PAction);
                    behaviourStateC.battleState = EcsConst.EntityBattleState.Skill2;             
              break;
              case EcsConst.SkillID.Skill3:
                    Skill3PAction skill3PAction = new Skill3PAction(entity);
                    Locator.action.performanceActions.Enqueue(skill3PAction);
                    behaviourStateC.battleState = EcsConst.EntityBattleState.Skill3;             
              break;                     
                                  
              default:
              break;
            }            
        }
    }
    public class IdleFsmState:FsmState
    {
      public override void HandlePlayerInput(string INPUT,Entity entity)
      {
        UnityInstanceComponent unityInstanceC  = entity._components[EcsConst.ComponentConst.UNITYINSTANCE] as UnityInstanceComponent;
        BehaviourStateComponent behaviourStateC = entity._components[EcsConst.ComponentConst.STATE] as BehaviourStateComponent;
        AnimatorStateInfo stateInfo = unityInstanceC.m_Animator.GetCurrentAnimatorStateInfo(0);
        switch (INPUT)
        {
          case EcsConst.SkillID.Skill1:
            if (!stateInfo.IsName("skill1"))
            {
               UseSkill(INPUT,entity);       
            }
          break;
          case EcsConst.SkillID.Skill2:
            if (!stateInfo.IsName("skill2"))
            {
               UseSkill(INPUT,entity);              
            }
          break;
          case EcsConst.SkillID.Skill3:
            if (!stateInfo.IsName("skill3"))
            {
              UseSkill(INPUT,entity);              
            }
          break;                     
                               
          default:
          break;
        }
      }
    }
    public class AttackFsmState:FsmState
    {
      public override void HandlePlayerInput(string INPUT,Entity entity)
      {
        
      }
    }
    public class Skill1FsmState:FsmState
    {
      public override void HandlePlayerInput(string INPUT,Entity entity)
      {
       
      }
    }
    public class Skill2FsmState:FsmState
    {
      public override void HandlePlayerInput(string INPUT,Entity entity)
      {
        
      }
    }
    public class Skill3FsmState:FsmState
    {
      public override void HandlePlayerInput(string INPUT,Entity entity)
      {
        
      }
    }
    public class MoveFsmState:FsmState
    {
      public override void HandlePlayerInput(string INPUT,Entity entity)
      {
        UnityInstanceComponent unityInstanceC  = entity._components[EcsConst.ComponentConst.UNITYINSTANCE] as UnityInstanceComponent;
        BehaviourStateComponent behaviourStateC = entity._components[EcsConst.ComponentConst.STATE] as BehaviourStateComponent;
        AnimatorStateInfo stateInfo = unityInstanceC.m_Animator.GetCurrentAnimatorStateInfo(0);
        switch (INPUT)
        {
          case"move":
            if (!stateInfo.IsName("move"))
            {
              unityInstanceC.m_Animator.Play("move");
            }
          break;
          case EcsConst.SkillID.Skill1:
            if (!stateInfo.IsName("skill1"))
            {
              UseSkill(INPUT,entity);            
            }
          break;
          case EcsConst.SkillID.Skill2:
            if (!stateInfo.IsName("skill2"))
            {
              UseSkill(INPUT,entity);            
            }
          break;
          case EcsConst.SkillID.Skill3:
            if (!stateInfo.IsName("skill3"))
            {
              UseSkill(INPUT,entity);           
            }
          break;                     
                               
          default:
          break;
        }
      }      
    }               
}
