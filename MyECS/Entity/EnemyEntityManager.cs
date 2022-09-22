using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.Component;
namespace ECS.EcsEntity
{
    public class EnemyEntityManager 
    {
       public Dictionary<string,EnemyPropertyItem> enemyDataDict;
       int PosID = 0;
       int MaxAliveEnemy = 8;
       public void Initialize()
       {
            EnemyPropertyData enemyPropertyData = Locator.ResLoader.LoadResource<EnemyPropertyData>("Assets/ScriptableData/enemyPropertyData.asset");
            enemyDataDict = new Dictionary<string, EnemyPropertyItem>();
            for (int i = 0; i < enemyPropertyData.enemyItems.Length; i++)
            {
               enemyDataDict.Add(enemyPropertyData.enemyItems[i].name,enemyPropertyData.enemyItems[i]);
            }    
       }
       public void Update(float deltaTime)
       {
            int enemyNum = 0;
            for (int i = 0; i < Locator.entityManager.entities.Length; i++)
            {
                Entity entity = Locator.entityManager.entities[i];
                if (entity == null )
                {
                    continue;
                }
                enemyNum++;
            }
            if (enemyNum <= MaxAliveEnemy)
            {
               CreateOneEnemy(); 
            }
       }
       /// <summary>
       /// 创建一个地方单位 位置随机 模型随机
       /// </summary>
       public void CreateOneEnemy()
       {
            int enemyID =  Random.Range(0,enemyDataDict.Count);
            EnemyPropertyItem item =  enemyDataDict["Enemy"+enemyID];
            GameObject enemyObj = Locator.pool.GetEnemyObj("Enemy"+enemyID);
            Entity entity = Locator.entityManager.CreateEntity();
            if (PosID >= Locator.battle.entityCreator.Length)
            {
                PosID = 0;
            }
            enemyObj.transform.position = Locator.battle.entityCreator[PosID].position;
            PosID++;
            //添加Transform组件
            TransformComponent transformC = new TransformComponent()
            {
            position = enemyObj.transform.position,
            speed = item.moveSpeed,
            };
            Locator.entityManager.AddEntityComponent(entity, transformC);
            //添加Property组件
            PropertyComponent PropertyC = new PropertyComponent()
            {
                hp         = item.hp,
                maxHp      = item.hp,
                attack     = item.attack,
                attackTime = item.attackTime,
                currentAttackTime = item.attackTime,
            };
            Locator.entityManager.AddEntityComponent(entity, PropertyC);  
            //添加UnityInstance组件
            Entity player = Locator.entityManager.playerEntity;
            UnityInstanceComponent playerUnityInstanceC  = player._components[EcsConst.ComponentConst.UNITYINSTANCE] as UnityInstanceComponent;            
            UnityInstanceComponent unityInstanceC = new UnityInstanceComponent()
            {
                m_Trans = enemyObj.transform,
                aim_Trans = playerUnityInstanceC.m_Trans,
            };
            unityInstanceC.Initialize();
            Locator.entityManager.AddEntityComponent(entity, unityInstanceC); 
            //添加behaviourStateC组件
            BehaviourStateComponent behaviourStateC = new BehaviourStateComponent()
            {
                 state = EcsConst.EntityState.Battle,
                 battleState = EcsConst.EntityBattleState.Move,   
            };
            Locator.entityManager.AddEntityComponent(entity,behaviourStateC);                       
       }

    }
}
