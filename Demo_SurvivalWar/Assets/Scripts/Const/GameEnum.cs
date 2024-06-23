
/// <summary>
/// 枪械武器类型枚举.
/// </summary>
public enum GunType
{
    /// <summary>
    /// 步枪
    /// </summary>
    AssaultRifle,
    HuntingRifle,
    Revolver,
    /// <summary>
    /// 霰弹枪
    /// </summary>
    Shotgun,
    /// <summary>
    /// 弓箭
    /// </summary>
    WoodenBow,
    /// <summary>
    /// 长矛
    /// </summary>
    WoodenSpear,
    /// <summary>
    /// 斧子
    /// </summary>
    StoneHatchet
}

/// <summary>
/// 模型材质类型枚举.
/// </summary>
public enum MaterialType
{
    /// <summary>
    /// 金属
    /// </summary>
    Metal,
    /// <summary>
    /// 石头
    /// </summary>
    Stone,
    /// <summary>
    /// 木头
    /// </summary>
    Wood
}


/// <summary>
/// 怪物管理器种类枚举
/// </summary>
public enum AIManagerType
{
    /// <summary>
    /// 丧尸
    /// </summary>
    CANNIBAL,
    /// <summary>
    /// 野猪
    /// </summary>
    BOAR,
    NULL
}
/// <summary>
/// 怪物种类枚举
/// </summary>
public enum AIType
{
    /// <summary>
    /// 丧尸
    /// </summary>
    CANNIBAL,
    /// <summary>
    /// 野猪
    /// </summary>
    BOAR,
    NULL
}

/// <summary>
/// AI角色的状态枚举.
/// </summary>
public enum AIState
{
    /// <summary>
    /// 常态
    /// </summary>
    IDLE,
    /// <summary>
    /// 行走
    /// </summary>
    WALK,
    /// <summary>
    /// 开始跑步
    /// </summary>
    ENTERRUN,
    /// <summary>
    /// 退出跑步
    /// </summary>
    EXITRUN,
    /// <summary>
    /// 开始攻击
    /// </summary>
    ENTERATTACK,
    /// <summary>
    /// 退出攻击
    /// </summary>
    EXITATTACK,
    /// <summary>
    /// 死亡
    /// </summary>
    DEATH
}
