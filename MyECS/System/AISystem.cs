using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.Component;
using ECS.EcsEntity;
namespace ECS.EcsSystem
{

    public class AISystem : ISystem
    {
        public string name => EcsConst.SystemConst.AI;

        private bool _enabled = false;

        public bool enabled
        {
            get { return _enabled; }
        }
        public void Update(float deltaTime)
        {
            for (int i = 0; i < Locator.entityManager.entities.Length; i++)
            {
                Entity entity = Locator.entityManager.entities[i];
                if (entity == null)
                {
                    continue;
                }
                AttackAIController(entity, deltaTime);
            }
        }
        public void LateUpdate(float deltaTime)
        {

        }
        /// <summary>
        /// 攻击逻辑控制器
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="deltaTime"></param>
        public void AttackAIController(Entity entity,float deltaTime)
        {

        }
    }
}
