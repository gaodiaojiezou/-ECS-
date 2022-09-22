using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.EcsEntity;
using ECS.Component;
namespace ECS.EcsSystem
{
    public class FsmStateSystem : ISystem
    {
        public string name => EcsConst.SystemConst.FSM;
        private Dictionary<EcsConst.EntityBattleState,FsmState> fsmStateDict;

        private bool _enabled = false;

        public bool enabled
        {
            get { return _enabled; }
        }
        public FsmStateSystem()
        {
            fsmStateDict = new Dictionary<EcsConst.EntityBattleState, FsmState>();
            IdleFsmState idleFsmState = new IdleFsmState();
            fsmStateDict.Add(EcsConst.EntityBattleState.Idle,idleFsmState);
            AttackFsmState attackFsmState = new AttackFsmState();
            fsmStateDict.Add(EcsConst.EntityBattleState.Attack,attackFsmState);
            Skill1FsmState skill1FsmState = new Skill1FsmState();
            fsmStateDict.Add(EcsConst.EntityBattleState.Skill1,skill1FsmState);
            Skill2FsmState skill2FsmState = new Skill2FsmState();
            fsmStateDict.Add(EcsConst.EntityBattleState.Skill2,skill2FsmState);
            Skill3FsmState skill3FsmState = new Skill3FsmState();
            fsmStateDict.Add(EcsConst.EntityBattleState.Skill3,skill3FsmState);  
            MoveFsmState moveFsmState =new MoveFsmState();
            fsmStateDict.Add(EcsConst.EntityBattleState.Move,moveFsmState);          
        }
        public void Update(float deltaTime)
        {
            UpdatePlayerEntity(deltaTime);
            UpdateEnemyEntity(deltaTime);
        }
       public void LateUpdate(float deltaTime)
        {

        } 
        public void UpdatePlayerEntity(float deltaTime)
        {
            Entity entity = Locator.entityManager.playerEntity;
            UnityInstanceComponent unityInstanceC  = entity._components[EcsConst.ComponentConst.UNITYINSTANCE] as UnityInstanceComponent;
            PlayerInputComponent playerInputC = Locator.world.GetCommonComponent(EcsConst.ComponentConst.INPUT) as PlayerInputComponent; 
            BehaviourStateComponent behaviourStateC = entity._components[EcsConst.ComponentConst.STATE] as BehaviourStateComponent; 
            EcsConst.EntityState entityState =  behaviourStateC.state;
            AnimatorStateInfo stateInfo = unityInstanceC.m_Animator.GetCurrentAnimatorStateInfo(0); 
            if (entityState == EcsConst.EntityState.Init)
            {
                EnterPosePAction enterPoseP = new EnterPosePAction(entity);
                Locator.action.performanceActions.Enqueue(enterPoseP);
                behaviourStateC.state = EcsConst.EntityState.EnterPose;
            }else if(entityState == EcsConst.EntityState.Dead)
            {
                return;
            }else if(entityState == EcsConst.EntityState.Battle)
            {
               if (behaviourStateC.battleState == EcsConst.EntityBattleState.Idle & !stateInfo.IsName("wait"))
               {
                  unityInstanceC.m_Animator.Play("wait");
               }
               if (behaviourStateC.battleState == EcsConst.EntityBattleState.Move & !stateInfo.IsName("move"))
               {
                  unityInstanceC.m_Animator.Play("move");
               }                 
               Queue<string> input  = new Queue<string>();
               foreach (string item in playerInputC.SkillInput)
               {
                 input.Enqueue(item);
               }
               playerInputC.SkillInput.Clear();
               foreach (string item in input)
               {
                fsmStateDict[behaviourStateC.battleState].HandlePlayerInput(item,entity);
               }
            }

        }
        private void UpdateEnemyEntity(float deltaTime) 
        {
            for (int i = 0; i < Locator.entityManager.entities.Length; i++)
            {
                Entity entity = Locator.entityManager.entities[i];
                if (entity == null )
                {
                    continue;
                }
                UnityInstanceComponent unityInstanceC  = entity._components[EcsConst.ComponentConst.UNITYINSTANCE] as UnityInstanceComponent; 
                PropertyComponent propertyC = entity._components[EcsConst.ComponentConst.PROPERTY] as PropertyComponent;
                BehaviourStateComponent behaviourStateC = entity._components[EcsConst.ComponentConst.STATE] as BehaviourStateComponent; 
                EcsConst.EntityState entityState =  behaviourStateC.state;
                AnimatorStateInfo stateInfo = unityInstanceC.m_Animator.GetCurrentAnimatorStateInfo(0); 
                if(entityState == EcsConst.EntityState.Dead)
                {
                   continue;
                }else if(entityState == EcsConst.EntityState.Battle)
                {
                    if (behaviourStateC.battleState == EcsConst.EntityBattleState.Idle & !stateInfo.IsName("wait"))
                    {
                        unityInstanceC.m_Animator.Play("wait");
                    }else if (behaviourStateC.battleState == EcsConst.EntityBattleState.Idle )
                    {
                        if (propertyC.currentAttackTime <= 0)
                        {
                           behaviourStateC.battleState = EcsConst.EntityBattleState.Attack; 
                        }
                    }else if (behaviourStateC.battleState == EcsConst.EntityBattleState.Move & !stateInfo.IsName("move"))
                    {
                        unityInstanceC.m_Animator.Play("move");
                    } 
                    if (behaviourStateC.battleState == EcsConst.EntityBattleState.Attack & !stateInfo.IsName("attack1"))
                    {
                        propertyC.currentAttackTime = propertyC.attackTime;
                        AttackPAction attackP = new AttackPAction(entity);
                        Locator.action.performanceActions.Enqueue(attackP);
                        List<Entity> entities = new List<Entity>();
                        entities.Add(Locator.entityManager.playerEntity); 
                        AttackDAction attackD = new AttackDAction(entity,entities);
                        Locator.action.dataActions.Enqueue(attackD);                 
                    }  
                }            
            } 
                       
        }   
    }
}
