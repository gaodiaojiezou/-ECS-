using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ECS.Component
{   
    /// <summary>
    /// 负责保存Entity的位置信息的Ecs组件
    /// </summary>    
    public class TransformComponent : IComponentData
    {
        public string name => EcsConst.ComponentConst.TRANFORM;
        /// <summary>
        /// 位置
        /// </summary>
        public Vector3 position ;
        /// <summary>
        /// 朝向
        /// </summary>
        public Vector3 rotation = new Vector3(1,0,0);
        /// <summary>
        /// 比例
        /// </summary>
        public Vector3 scale;
        /// <summary>
        /// 移动速度
        /// </summary>
        public float speed;

    }
}
