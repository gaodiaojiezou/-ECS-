using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ECS.Component
{
    public class BehaviourStateComponent : IComponentData
    {
        public string name => EcsConst.ComponentConst.STATE;

        public EcsConst.EntityState state;
        public EcsConst.EntityBattleState battleState;
    }
}
