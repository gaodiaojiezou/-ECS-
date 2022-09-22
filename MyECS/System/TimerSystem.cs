using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.Component;
using ECS.EcsEntity;
namespace ECS.EcsSystem
{
    public class TimerSystem : ISystem
    {
        public string name => EcsConst.SystemConst.TIMER;

        private bool _enabled = false;

        public bool enabled
        {
            get { return _enabled; }
        }

        public void Update(float deltaTime)
        {
            UpdatePlayerEntity(deltaTime);
            UpdateEnemyEntity(deltaTime);
        }
        public void LateUpdate(float deltaTime)
        {

        } 
        private void UpdatePlayerEntity(float deltaTime)
        {
            Entity entity = Locator.entityManager.playerEntity;
            AbilityComponent abilityComponent = entity._components[EcsConst.ComponentConst.ABILITY] as AbilityComponent;
            BehaviourStateComponent behaviourStateC = entity._components[EcsConst.ComponentConst.STATE] as BehaviourStateComponent;
            if (behaviourStateC.state == EcsConst.EntityState.Dead)
            {
                return;
            }
            Ability Skill1 = abilityComponent.abilityDict[EcsConst.SkillID.Skill1];
            Ability Skill2 = abilityComponent.abilityDict[EcsConst.SkillID.Skill2];
            Ability Skill3 = abilityComponent.abilityDict[EcsConst.SkillID.Skill3];
            Skill1.CurrentCd  -=  Skill1.CurrentCd >0 ? deltaTime:0;
            Skill2.CurrentCd  -=  Skill2.CurrentCd >0 ? deltaTime:0;
            Skill3.CurrentCd  -=  Skill3.CurrentCd >0 ? deltaTime:0;

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
                PropertyComponent propertyC = entity._components[EcsConst.ComponentConst.PROPERTY] as PropertyComponent;
                BehaviourStateComponent behaviourStateC = entity._components[EcsConst.ComponentConst.STATE] as BehaviourStateComponent;  
                if (behaviourStateC.state == EcsConst.EntityState.Dead)
                {
                    return;
                }   
                propertyC.currentAttackTime -= propertyC.currentAttackTime > 0 ? deltaTime:0;                           
            }
        }                      
    }
}
