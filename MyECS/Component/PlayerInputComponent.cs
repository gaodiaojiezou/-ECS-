using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ECS.Component
{
    /// <summary>
    /// 负责储存玩家输入的公共组件
    /// </summary>
    public class PlayerInputComponent : IComponentData
    {
       public string name => EcsConst.ComponentConst.INPUT;
       public Vector3 aimPositon;
       public Queue<string> SkillInput = new Queue<string>();

    }
}