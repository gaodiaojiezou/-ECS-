using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECS.EcsEntity;
using ECS.EcsSystem;
using ECS.Component;
using System;
    public class World:IDisposable
    {
        public EntityManager entityManager;
        /// <summary>
        /// 存放所有的Ecs系统
        /// </summary>
        private Dictionary<string, ISystem> systemDict;
        /// <summary>
        /// 存放大部分Entity都会使用的公共Component实例
        /// </summary>
        private Dictionary<string,IComponentData> ComponentDict;  
        
        public void initialize()
        {
            entityManager = new EntityManager();
            Locator.entityManager = entityManager;
            systemDict = new Dictionary<string, ISystem>();
            ComponentDict = new Dictionary<string, IComponentData>();
            PlayerInputComponent inputComponent = new PlayerInputComponent();
            ComponentDict.Add(EcsConst.ComponentConst.INPUT,inputComponent);
        }
        public void AddSystem(ISystem system)
        {
            if(!systemDict.ContainsKey(system.name))
            {
                systemDict.Add(system.name,system);
            }
        }
        public void RemoveSystem(ISystem system)
        {
            if(!systemDict.ContainsKey(system.name))
            {
                systemDict.Remove(system.name);
            }
        }
        public IComponentData GetCommonComponent(string component)
        {
           ComponentDict.TryGetValue(component,out IComponentData componentInstance);
           return componentInstance;
        }
        public void Update(float deltaTime)
        {
        foreach (ISystem each in systemDict.Values)
            {
                each.Update(deltaTime);
            }
         }
        public void LateUpdate(float deltaTime)
        {
            foreach (ISystem each in systemDict.Values)
            {
                each.LateUpdate(deltaTime);
            }            
        }



    public void Dispose()
        {
            entityManager = null;
            systemDict.Clear();
        }


    }

