using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// 能力效果数据
    /// </summary>
    [System.Serializable]
    public class ABilityAction
    {
       public EcsConst.ABilityActionType actionType;
       //目标阵营
       public EcsConst.EntityCamp  camp;
    }
    /// <summary>
    /// 效果为修改生命值
    /// </summary>
    [System.Serializable]
    public class ChangeHpAction:ABilityAction
    {
        public EcsConst.ABilityAreaType AreaType;
        //生效距离
        public int availDistance;
        //生效改变的生命数值
        public int HpNum;

    }
    /// <summary>
    /// 效果为修改属性
    /// </summary>
    [System.Serializable]
    public class ChangePropertyAction:ABilityAction
    {
        //生效时间
        public float availTime;
        //生效属性类型
        public EcsConst.PropertyType changePropertyType;
        //生效改变的属性数值
        public int propertyNum;
    }
    /// <summary>
    /// 效果为修改位置
    /// </summary>
    [System.Serializable]
    public class ChangePostionAction:ABilityAction
    {
        //生效改变的距离
        public int distanceNum;
    }
namespace ECS.Component
{    
    /// <summary>
    /// 负责储存玩家被动和主动能力的组件
    /// </summary>
    public class Ability
    {
        public EcsConst.AbilityType type;
        /// <summary>
        /// 当前技能的效果
        /// </summary>
        public Dictionary<EcsConst.ABilityActionType,ABilityAction> actions;
        /// <summary>
        /// 技能ID
        /// </summary>
        public string AbilityID;
        /// <summary>
        /// 技能冷却时间
        /// </summary>
        public float CdCoolNum;
        /// <summary>
        /// 技能当前剩余冷却时间
        /// </summary>
        public float CurrentCd;
    }
    public class AbilityComponent:IComponentData
    {
        public string name => EcsConst.ComponentConst.ABILITY;
        public Dictionary<string,Ability> abilityDict = new Dictionary<string, Ability>();
    }
}
