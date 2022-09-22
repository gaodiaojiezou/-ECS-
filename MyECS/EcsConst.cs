
public static class EcsConst
{
    public static class SystemConst
    {
        public const string MOVE = "MoveSystem"; //控制Entity移动的系统
        public const string AI = "AISyetem";//控制Entity行为AI的系统
        public const string FSM = "FsmStateSystem";//控制Entity状态机的系统
        public const string TIMER = "TimerSystem";//控制Entity需要计时的变量
        public const string DEAD = "DeadSystem";//控制Entity的死亡处理
    }
    public static class ComponentConst
    {
        public const string TRANFORM = "TranformComponent";//存放Entity位置及Transform实例的组件
        public const string PROPERTY = "PropertyComponent";//存放Entity属性的组件
        public const string STATE = "BehaviourStateComponent";//存放Entity行为状态的组件
        public const string TIME = "TimeClockComponent";//存放Entity计时器的组件
        public const string INPUT = "PlayerInputComponent";//存放玩家控制信息的组件
        public const string UNITYINSTANCE = "UnityInstanceComponent";//存放unity实例的组件
        public const string ABILITY = "AbilityComponent";//存放Entity能力的组件
    }
    public enum EntityState
    {
        Init,//刚创建
        EnterPose,//入场动画
        Battle,//战斗
        Dead,//死亡
    }
    public enum EntityBattleState
    {
        Idle,//待机
        Attack,//攻击
        Skill1,//释放技能1中
        Skill2,//释放技能2中
        Skill3,//释放技能3中
        Move,//移动
    }
    /// <summary>
    /// 阵营
    /// </summary>
    public enum EntityCamp
    {
        Player,//玩家阵营
        Enemy,//敌方阵营
    }
    /// <summary>
    /// 能力类型
    /// </summary>
    public enum AbilityType
    {
       Passive,//被动
       Skill,//技能
    }

    /// <summary>
    /// 能力效果类型
    /// </summary>
    public enum ABilityActionType
    {
       ChangeHp,//改变生命,包括加血和造成伤害
       ChangeProperty,//改变属性,包括增强和削弱
       ChangePostion,//改变位置,包括敌方位移和己方
    } 
    /// <summary>
    ///  能力生效范围类型
    /// </summary>
    public enum ABilityAreaType
    {
       PlayerSelf,//玩家自身范围
       Forward,//玩家前方
       MousePostion,//鼠标位置
    } 
    //属性类型
    public enum PropertyType
    {
      Attack,//攻击力
      AttackTime,//攻击间隔
      Speed,//移动速度
    }
    public static class SkillID
    {
        public const string Skill1 = "1";
        public const string Skill2 = "2";
        public const string Skill3 = "3";

    }

}
