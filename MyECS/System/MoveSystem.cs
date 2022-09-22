using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.Component;
using ECS.EcsEntity;
namespace ECS.EcsSystem
{
    /// <summary>
    /// 用于控制所有Entity移动数据的系统
    /// </summary>
    public class MoveSystem : ISystem
    {
        public string name => EcsConst.SystemConst.MOVE;

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
            TransformComponent transformC  = entity._components[EcsConst.ComponentConst.TRANFORM] as TransformComponent;
            UnityInstanceComponent unityInstanceC  = entity._components[EcsConst.ComponentConst.UNITYINSTANCE] as UnityInstanceComponent;
            PlayerInputComponent playerInputC = Locator.world.GetCommonComponent(EcsConst.ComponentConst.INPUT) as PlayerInputComponent;
            BehaviourStateComponent behaviourStateC = entity._components[EcsConst.ComponentConst.STATE] as BehaviourStateComponent;
            //只有在战斗状态下,才能进行移动
            if (behaviourStateC.state != EcsConst.EntityState.Battle)
                return;
            //更新TRANFORM组件
            transformC.position = unityInstanceC.m_Trans.position;                
            //更新TRANFORM组件
            float distance = Vector3.Distance(playerInputC.aimPositon, transformC.position);
            //如果距离目标点小于1,则不进行处理
            if(distance <= 1f)
            { 
                if (behaviourStateC.battleState == EcsConst.EntityBattleState.Move)
                {
                   behaviourStateC.battleState = EcsConst.EntityBattleState.Idle; 
                }
            return;
            }
            if (behaviourStateC.battleState != EcsConst.EntityBattleState.Idle & behaviourStateC.battleState != EcsConst.EntityBattleState.Move)           
            return;
            behaviourStateC.battleState = EcsConst.EntityBattleState.Move;
            Vector3 dir = new Vector3();
            Vector3 aimPosition = new Vector3();
            dir.x = (playerInputC.aimPositon - transformC.position).normalized.x;
            dir.z = (playerInputC.aimPositon - transformC.position).normalized.z;
            dir.y = (playerInputC.aimPositon - transformC.position).normalized.y;
            aimPosition = transformC.position + dir * transformC.speed*deltaTime;
            //更新TRANFORM组件数据到表现层
            unityInstanceC.m_Rigidbody2D.MovePosition(aimPosition);  
            //计算朝向
            if(Vector3.Dot(dir,transformC.rotation) < 0) 
            {
               unityInstanceC.m_Trans.Rotate(new Vector3(0,180,0));
               transformC.rotation = new Vector3(-transformC.rotation.x,transformC.rotation.y,transformC.rotation.z);
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
            TransformComponent transformC  = entity._components[EcsConst.ComponentConst.TRANFORM] as TransformComponent;
            UnityInstanceComponent unityInstanceC  = entity._components[EcsConst.ComponentConst.UNITYINSTANCE] as UnityInstanceComponent;
            BehaviourStateComponent behaviourStateC = entity._components[EcsConst.ComponentConst.STATE] as BehaviourStateComponent;
            unityInstanceC.m_Rigidbody2D.velocity = Vector2.zero;
            //只有在战斗状态下,才能进行移动
            if (behaviourStateC.state == EcsConst.EntityState.Dead)
                continue;
            //更新TRANFORM组件
            transformC.position = unityInstanceC.m_Trans.position;
            float distance = Vector3.Distance(unityInstanceC.aim_Trans.position, transformC.position);
            //如果距离目标小于4,则开始进入待机状态
            if(distance <= 3)
            {               
                if (behaviourStateC.battleState == EcsConst.EntityBattleState.Move)
                {
                   behaviourStateC.battleState = EcsConst.EntityBattleState.Idle; 
                }
                continue;
            } 
            if (behaviourStateC.battleState != EcsConst.EntityBattleState.Move)
            {
                if(distance >= 5)
                behaviourStateC.battleState = EcsConst.EntityBattleState.Move; 
                continue;
            }   
            Vector3 dir = new Vector3();
            Vector3 aimPosition = new Vector3();
            dir.x = (unityInstanceC.aim_Trans.position - transformC.position).normalized.x;
            dir.z = (unityInstanceC.aim_Trans.position - transformC.position).normalized.z;
            dir.y = (unityInstanceC.aim_Trans.position - transformC.position).normalized.y;
            aimPosition = transformC.position + dir * transformC.speed*deltaTime;
            //更新TRANFORM组件数据到表现层
            unityInstanceC.m_Rigidbody2D.MovePosition(aimPosition);
            //计算朝向
            if(Vector3.Dot(dir,transformC.rotation) < 0) 
            {
               unityInstanceC.m_Trans.Rotate(new Vector3(0,180,0));
               transformC.rotation = new Vector3(-transformC.rotation.x,transformC.rotation.y,transformC.rotation.z);
            }            
            }
        }

    }
}
