using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ECS.Component
{
    public class PropertyComponent : IComponentData
    {
        public string name => EcsConst.ComponentConst.PROPERTY;
        /// <summary>
        /// 最大生命值
        /// </summary>        
        public float maxHp { get; set; }
        /// <summary>
        /// 生命值
        /// </summary>
        public int hp { get; set; }
        /// <summary>
        /// 攻击力
        /// </summary>
        public int attack { get; set; }
        /// <summary>
        /// 攻击间隔
        /// </summary>
        public float attackTime { get; set; }
        /// <summary>
        /// 当前的攻击计时器
        /// </summary>
        /// <value></value>
        public float currentAttackTime{ get; set; }
        /// <summary>
        /// 攻击距离
        /// </summary>
        public float attackDisTance { get; set; }

    }
}
