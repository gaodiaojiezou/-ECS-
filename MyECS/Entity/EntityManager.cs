using System.Collections;
using System.Collections.Generic;
using System;
using ECS.Component;
using UnityEngine;

namespace ECS.EcsEntity
{
    public class EntityManager //:IEntityManager
    {
        public Entity[] entities = new Entity[10000]; //entity存储区 暂定最大Entity数为10000
        public Entity playerEntity ;//玩家操纵的实体
        public Queue<int> _FreeID = new Queue<int>();
        public event Action<Entity> EntityInit;
        public event Action<Entity> EntityCreated;

        public event Action<Entity> EntityDeleted;
        public int index ;

        public EntityManager()
        {
            index = 0;
        }
        public Entity CreateEntity() 
        {
            Entity val = new Entity();
            AddEntity(val);
            return val;
        }
        private void AddEntity(Entity entity)
        {
            if(_FreeID.Count > 0)
            {
                entity.Index = _FreeID.Dequeue();
                entities[entity.Index] = entity;
            }else
            {
                entity.Index = index;
                entities[index] = entity;
                EntityInit?.Invoke(entity);
                EntityCreated?.Invoke(entity);
                index++;
            }
        }


        public bool DeleteEntity(int id)
        {
            if (id < entities.Length)
            {
                Entity entity = entities[id];
                if(entity != null&& entity.Index == id)
                {
                    entities[id] = null;
                    _FreeID.Enqueue(id);
                    EntityDeleted?.Invoke(entity);
                    return true;
                }
            }
            return false;
        }
        public bool AddEntityComponent(Entity entity,IComponentData component)
        {
            if( entity._components.ContainsKey(component.name))
            {
                return false;
            }else
            {
                entity._components.Add(component.name, component);
                return true;
            }
        }

        public bool RemoveEntityComponent(Entity entity, IComponentData component)
        {
            if (!entity._components.ContainsKey(component.name))
            {
                return false;
            }
            else
            {
                entity._components.Remove(component.name);
                return true;
            }
        }


        public void Dispose()
        {
            EntityCreated = null;
            EntityDeleted = null;
        }
     }
}
    
