#region SceneType 场景类型
/// <summary>
/// 场景类型
/// </summary>
public enum SceneType
{
    /// <summary>
    /// 空
    /// </summary>
    None,

    /// <summary>
    /// 登录
    /// </summary>
    Login,

    /// <summary>
    /// 菜单面板
    /// </summary>
    Menu,

    /// <summary>
    /// 教学面板
    /// </summary>
    Instruct,

    /// <summary>
    /// 游戏面板
    /// </summary>
    Game
}
#endregion

#region GameStateType 游戏状态类型
/// <summary>
/// 游戏状态类型
/// </summary>
public enum GameStateType
{
    /// <summary>
    /// 空
    /// </summary>
    None,

    /// <summary>
    /// 游戏开始状态
    /// </summary>
    Start,

    /// <summary>
    /// 游戏中状态
    /// </summary>
    Gaming,

    /// <summary>
    /// 教学中状态
    /// </summary>
    Teaching,

    /// <summary>
    /// 游戏胜利状态
    /// </summary>
    Victory,

    /// <summary>
    /// 游戏失败状态
    /// </summary>
    Defeat
}
#endregion

#region RoleState 角色状态类型
/// <summary>
/// 角色状态类型
/// </summary>
public enum RoleState
{
    /// <summary>
    /// 空
    /// </summary>
    None,

    /// <summary>
    /// 等待状态
    /// </summary>
    Idle,

    /// <summary>
    /// 跑步状态
    /// </summary>
    Run,

    /// <summary>
    /// 死亡状态
    /// </summary>
    Die,

    /// <summary>
    /// 攻击状态
    /// </summary>
    Attack,

    /// <summary>
    /// 受伤状态
    /// </summary>
    Damage
}
#endregion

#region ToAnimatorConditon 角色动画控制变量名称
/// <summary>
/// 角色动画控制变量名称
/// </summary>
public enum ToAnimatorConditon
{
    /// <summary>
    /// 转换到等待的控制变量名称
    /// </summary>
    ToIdle,

    /// <summary>
    /// 转换到跑步的控制变量名称
    /// </summary>
    ToRun,

    /// <summary>
    /// 转换到死亡的控制变量名称
    /// </summary>
    ToDie,

    /// <summary>
    /// 转换到攻击的控制变量名称
    /// </summary>
    ToAttack,

    /// <summary>
    /// 转换到受伤的控制变量名称
    /// </summary>
    ToDamage,

    /// <summary>
    /// 打断的控制变量名称
    /// </summary>
    ToIterrupt,

    /// <summary>
    /// 当前状态的控制变量名称
    /// </summary>
    CurrState
}
#endregion

#region RoleAnimatorName 角色动画状态名称
/// <summary>
/// 角色动画状态名称
/// </summary>
public enum RoleAnimatorName
{
    /// <summary>
    /// 等待
    /// </summary>
    Idle,

    /// <summary>
    /// 跑步
    /// </summary>
    Run,

    /// <summary>
    /// 死亡
    /// </summary>
    Die,

    /// <summary>
    /// 攻击
    /// </summary>
    Attack,

    /// <summary>
    /// 受伤
    /// </summary>
    Damage,

    /// <summary>
    /// 瞄准
    /// </summary>
    Aiming,

    /// <summary>
    /// 被打断
    /// </summary>
    Iterrupt
}
#endregion

#region RoleType 角色类型
/// <summary>
/// 角色类型
/// </summary>
public enum RoleType
{
    /// <summary>
    /// 未设置
    /// </summary>
    None = 0,

    /// <summary>
    /// 当前玩家
    /// </summary>
    MainPlayer = 1,

    /// <summary>
    /// 怪
    /// </summary>
    Monster = 2
}
#endregion

#region ActionCode 动作类型
/// <summary>
/// 动作类型
/// </summary>
public enum ActionCode
{
    /// <summary>
    /// 空
    /// </summary>
    None,

    /// <summary>
    /// 登录
    /// </summary>
    Login,

    /// <summary>
    /// 获取排名
    /// </summary>
    GetRank,

    /// <summary>
    /// 提交结果
    /// </summary>
    Submit
}
#endregion

#region ReturnCode 返回类型
/// <summary>
/// 返回类型
/// </summary>
public enum ReturnCode
{
    /// <summary>
    /// 成功
    /// </summary>
    Success,

    /// <summary>
    /// 失败
    /// </summary>
    Fail,

    /// <summary>
    /// 没有找到
    /// </summary>
    NotFound
}
#endregion