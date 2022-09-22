using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.EcsEntity;
using ECS.Component;
namespace ECS.EcsSystem
{
    public class SystemFunction 
    {
        public List<Entity> GetPlayerSkillRangeEntityList(Ability ability)
        {
            List<Entity> entities = new List<Entity>();
            Entity player = Locator.entityManager.playerEntity;
            TransformComponent playerTransformC  = player._components[EcsConst.ComponentConst.TRANFORM] as TransformComponent;
            ability.actions.TryGetValue(EcsConst.ABilityActionType.ChangeHp,out ABilityAction aBilityAction);
            ChangeHpAction changeHpAction = aBilityAction as ChangeHpAction;
            EcsConst.ABilityAreaType AreaType = changeHpAction.AreaType;
            int availDistance = changeHpAction.availDistance;
            for (int i = 0; i < Locator.entityManager.entities.Length; i++)
            {
                Entity entity = Locator.entityManager.entities[i];
                if (entity == null )
                {
                    continue;
                }
                TransformComponent transformC  = entity._components[EcsConst.ComponentConst.TRANFORM] as TransformComponent;
                BehaviourStateComponent behaviourStateC = entity._components[EcsConst.ComponentConst.STATE] as BehaviourStateComponent;
                if (behaviourStateC.state == EcsConst.EntityState.Dead)
                {
                   continue; 
                }
                float distance = Vector3.Distance(playerTransformC.position, transformC.position);
                if (AreaType == EcsConst.ABilityAreaType.PlayerSelf)
                {
                     if (distance <= availDistance)
                     {
                        entities.Add(entity);
                     }
                }else if(AreaType == EcsConst.ABilityAreaType.Forward)
                {
                    Vector3 dir = new Vector3();
                    dir.x = (transformC.position - playerTransformC.position).normalized.x;
                    dir.z = (transformC.position - playerTransformC.position).normalized.z;
                    dir.y = (transformC.position - playerTransformC.position).normalized.y;
                    if(Vector3.Dot(dir,playerTransformC.rotation) > 0 & distance <= availDistance) 
                    {
                      entities.Add(entity);
                    }
                }

            }
            return entities;
        }
    }
}