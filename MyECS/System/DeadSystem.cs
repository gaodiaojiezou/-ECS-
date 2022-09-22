using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.EcsEntity;
using ECS.Component;
namespace ECS.EcsSystem
{
    public class DeadSystem : ISystem
    {
        public string name => EcsConst.SystemConst.DEAD;

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
        public void UpdatePlayerEntity(float deltaTime)
        {
           Entity entity = Locator.entityManager.playerEntity;
           BehaviourStateComponent behaviourStateC = entity._components[EcsConst.ComponentConst.STATE] as BehaviourStateComponent;
           PropertyComponent propertyC = entity._components[EcsConst.ComponentConst.PROPERTY] as PropertyComponent;
            if(behaviourStateC.state == EcsConst.EntityState.Dead)
            {
              return;
            }
            if (propertyC.hp <= 0)
            {
                behaviourStateC.state = EcsConst.EntityState.Dead;
                DeadPAction deadPAction =new DeadPAction(entity);
                Locator.action.performanceActions.Enqueue(deadPAction);
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
                BehaviourStateComponent behaviourStateC = entity._components[EcsConst.ComponentConst.STATE] as BehaviourStateComponent;
                PropertyComponent propertyC = entity._components[EcsConst.ComponentConst.PROPERTY] as PropertyComponent;
                if (propertyC.hp <= 0)
                {
                    behaviourStateC.state = EcsConst.EntityState.Dead;
                    DeadPAction deadPAction =new DeadPAction(entity);
                    Locator.action.performanceActions.Enqueue(deadPAction);
                    Locator.entityManager.DeleteEntity(entity.Index);
                }                
            }        
        } 
                                     
    }
}
